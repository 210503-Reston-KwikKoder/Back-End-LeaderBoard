using BELBBL;
using BELBModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BELBRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LBController : ControllerBase
    {
        private readonly ApiSettings _ApiSettings;
        private readonly ILeaderboardBL _leaderboardBL;
        private readonly ICategoryBL _categoryBL;
        public LBController(ILeaderboardBL  leaderboardBL, IOptions<ApiSettings> settings, ICategoryBL categoryBL)
        {
            _leaderboardBL = leaderboardBL;
            _ApiSettings = settings.Value;
            _categoryBL = categoryBL;
        }
        /// <summary>
        /// GET /api/LB
        /// General leaderboard, gets the best users in general to send to client
        /// </summary>
        /// <returns>List of best users in the database sorted by WPM or 404 if it cannot be found</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLeaderboards()
        {
            /*Get 
            Get List users from data base
            foreach UserName AVG(AverageWPM / AverageAcc) < --Could Cause high runtimes, solution? storing as independent Table

            Rank(results)
            */
            return Ok(await _leaderboardBL.GetAllLeaderboards());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderboardByCatID(int id)
        {
            /*Get { CatID}
            -Get data for users whose rank < 101 && this.CatID == CatID
            */
            return Ok(await _leaderboardBL.GetLeaderboardByCatId(id)); // Just have this to prevent errors for now...
        }

        [HttpPost]
        public async Task<IActionResult> AddLeaderboard( LeaderBoard leaderBoard)
        {
            return Ok(await _leaderboardBL.AddLeaderboard(leaderBoard));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaderboard(List<LeaderBoard> leaderBoard, int id)
        {
            return Ok(await _leaderboardBL.Updatedleaderboard(leaderBoard));
        }
        // Dont need delete, just need update. Data here will probably never be removed.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaderboard(string id, int cID)
        {
            try
            {
                await _leaderboardBL.DeleteLeaderboard(id, cID);
                return NoContent();
            }
            catch (Exception e)
            {
                Log.Error("Failed to remove Leaderboard with ListID " + id + " in LeaderboardController", e.Message);
                return BadRequest();
            }
        }
        /// <summary>
        /// Private method to get application token for auth0 management 
        /// </summary>
        /// <returns>dynamic object with token for Auth0 call</returns>
        private dynamic GetApplicationToken()
        {
            var client = new RestClient("https://kwikkoder.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", _ApiSettings.authString, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Log.Information("Response: {0}",response.Content);
            return JsonConvert.DeserializeObject(response.Content);
        }
    }
}
