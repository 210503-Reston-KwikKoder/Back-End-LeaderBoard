using BELBBL;
using BELBModels;
using BELBRest.DTO;
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
    public class LeaderboardController : ControllerBase
    {
        private readonly ApiSettings _ApiSettings;
        private readonly ILeaderboardBusinessLayer _leaderboardBL;
        private readonly IUserBusinessLayer _userBL;
        public LeaderboardController(ILeaderboardBusinessLayer  leaderboardBL, IOptions<ApiSettings> settings, IUserBusinessLayer userBL)
        {
            _leaderboardBL = leaderboardBL;
            _ApiSettings = settings.Value;
            _userBL = userBL;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllLeaderboards()
        {
            List<LeaderBoard> leaderBoards = await _leaderboardBL.GetAllLeaderboards();
            List<LeaderboardModel> lBModels = new List<LeaderboardModel>();
            foreach( LeaderBoard lb in leaderBoards)
            {
                LeaderboardModel lBModel = new LeaderboardModel(lb);
                BELBModels.User user = await _userBL.GetUser(lBModel.AuthId);
                if (user.Name != null) lBModel.Name = user.Name;
                if (user.UserName != null) lBModel.UserName = user.UserName;
                lBModels.Add(lBModel);
            }
            return Ok(lBModels);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderboardByCatID(int id)
        {
            List<LeaderBoard> leaderBoards = await _leaderboardBL.GetLeaderboardByCatId(id);
            List<LeaderboardModel> lBModels = new List<LeaderboardModel>();
            foreach (LeaderBoard lb in leaderBoards)
            {
                LeaderboardModel lBModel = new LeaderboardModel(lb);
                BELBModels.User user = await _userBL.GetUser(lBModel.AuthId);
                if (user.Name != null) lBModel.Name = user.Name;
                if (user.UserName != null) lBModel.UserName = user.UserName;
                lBModels.Add(lBModel);
            }
            return Ok(lBModels); // Just have this to prevent errors for now...
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaderboard(List<LeaderboardModel> lBModels)
        {
            List<LeaderBoard> leaderBoards = new List<LeaderBoard>();
            foreach(LeaderboardModel l in lBModels)
            {
                if((await _userBL.GetUser(l.AuthId)) == null)
                {
                    User user = new User();
                    if (l.Name != null) user.Name = l.Name;
                    if (l.UserName != null) user.UserName = l.UserName;
                    user.AuthId = l.AuthId;
                    await _userBL.AddUser(user);
                }
                LeaderBoard leaderBoard = l;
                leaderBoards.Add(leaderBoard);
            }
            await _leaderboardBL.Updatedleaderboard(leaderBoards);
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
