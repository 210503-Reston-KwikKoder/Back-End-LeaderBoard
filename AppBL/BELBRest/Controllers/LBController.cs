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
            return Ok(await _leaderboardBL.GetAllLeaderboards());

        }
        [HttpPost]
        public async Task<IActionResult> AddLeaderboard(LeaderBoard leaderBoard)
        {
            return Ok(await _leaderboardBL.AddLeaderboard(leaderBoard));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDogList(int id)
        {
            try
            {
                await _leaderboardBL.DeleteLeaderboard(id);
                return NoContent();
            }
            catch (Exception e)
            {
                Log.Error("Failed to remove DogList with ListID " + id + " in DogListController", e.Message);
                return BadRequest();
            }
        }
        /*COMMENTED OUT FOR NOW TO MAKE SURE DB IS PERSISTANT AND WORKING
         * 
         * public async Task<ActionResult<IEnumerable<LBUserModel>>> GetAsync()
        { //To create a new one... 
            try
            {
                List<Tuple<User, double, double, int>> statTuples = await _userStatBL.GetOverallBestUsers();
                List<LBUserModel> lBUserModels = new List<LBUserModel>();
                foreach (Tuple<User, double, double, int> tuple in statTuples)
                {
                    LBUserModel lBUserModel = new LBUserModel();
                    lBUserModel.AverageWPM = tuple.Item2;
                    lBUserModel.AverageAcc = tuple.Item3;
                    lBUserModel.Ranking = tuple.Item4;
                    try
                    { //All is created to get the username
                        //Call to get the username
                        dynamic AppBearerToken = GetApplicationToken();
                        var client = new RestClient($"https://kwikkoder.us.auth0.com/api/v2/users/{tuple.Item1.Auth0Id}");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("authorization", "Bearer " + AppBearerToken.access_token);
                        IRestResponse restResponse = await client.ExecuteAsync(request);
                        dynamic deResponse = JsonConvert.DeserializeObject(restResponse.Content);
                        lBUserModel.UserName = deResponse.username;
                        lBUserModel.Name = deResponse.name;
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                        Log.Error("Unexpected error occured in LBController");
                    }
                    
                    if ((!Double.IsNaN(lBUserModel.AverageWPM)) && (!Double.IsNaN(lBUserModel.AverageAcc)) && (!Double.IsInfinity(lBUserModel.AverageWPM)) && (!Double.IsInfinity(lBUserModel.AverageAcc)))
                        lBUserModels.Add(lBUserModel);
                }
                return lBUserModels;
            }catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error("Error found returning 404");
                return NotFound();

            }
        }
        /// <summary>
        /// Gets the best Users for a given category
        /// </summary>
        /// <param name="id">category of Id to search</param>
        /// <returns>List of best users in the database sorted by WPM in that category or 404 if it cannot be found</returns>
        // GET api/<LBController>/5
        [HttpGet("{id}")] //Catagory ID
        public async Task<IEnumerable<LBUserModel>> GetAsync(int id)
        {// Can be replaced by search of category by id 
            Category category;
            try { category = await _categoryBL.GetCategory(id); }
            catch (Exception) {
                Log.Error("Category not found returning empty");
                return new List<LBUserModel>();  }
            List<Tuple<User, double, double, int>> statTuples;
            try { statTuples = await _userStatBL.GetBestUsersForCategory(category.Id); }
            catch(Exception e ) {
                Log.Error(e.StackTrace);
                Log.Error("Category not found returning empty");
                return new List<LBUserModel>();
            }
            List<LBUserModel> lBUserModels = new List<LBUserModel>();
            foreach (Tuple<User, double, double,int> tuple in statTuples)
            {
                LBUserModel lBUserModel = new LBUserModel();
                try
                {
                    dynamic AppBearerToken = GetApplicationToken();
                    var client = new RestClient($"https://kwikkoder.us.auth0.com/api/v2/users/{tuple.Item1.Auth0Id}");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + AppBearerToken.access_token);
                    IRestResponse restResponse = await client.ExecuteAsync(request);
                    dynamic deResponse = JsonConvert.DeserializeObject(restResponse.Content);
                    lBUserModel.Name = deResponse.name;
                    lBUserModel.UserName = deResponse.username;
                }
                catch(Exception e)
                {
                    Log.Error(e.Message);
                    Log.Error("Unexpected error occured in LBController");
                }
                lBUserModel.AverageWPM = tuple.Item2;
                lBUserModel.AverageAcc = tuple.Item3;
                lBUserModel.Ranking = tuple.Item4;
                lBUserModels.Add(lBUserModel);
            }
            return lBUserModels;
        }*/
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
