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
        public LBController(ILeaderboardBL  leaderboardBL, IOptions<ApiSettings> settings)
        {
            _leaderboardBL = leaderboardBL;
            _ApiSettings = settings.Value;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllLeaderboards()
        {
            
            return Ok(await _leaderboardBL.GetAllLeaderboards());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderboardByCatID(int id)
        {
            return Ok(await _leaderboardBL.GetLeaderboardByCatId(id)); // Just have this to prevent errors for now...
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaderboard(List<LeaderBoard> leaderBoard)
        {
            await _leaderboardBL.Updatedleaderboard(leaderBoard);
            return NoContent();
        }
        // Dont need delete, just need update. Data here will probably never be removed.
        [HttpDelete("{cID}")]
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
        
    }
}
