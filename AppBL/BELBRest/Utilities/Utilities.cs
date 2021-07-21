using LeaderboardBusinessLayer;
using LeaderboardModels;
using LeaderboardRest.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LeaderboardRest.Utilities
{
    public class Utilities
    {

        public static async Task<List<LeaderboardModel>> AddUserNamesToLeaderBoards(List<LeaderBoard> leaderboards, IUserBusinessLogic userBL)
        {
            List<LeaderboardModel> lBModels = new List<LeaderboardModel>();
             
            foreach (LeaderBoard lb in leaderboards)
            {
                // can abstract this to a method
                LeaderboardModel lBModel = new LeaderboardModel(lb);
                LeaderboardModels.User user = await userBL.GetUser(lBModel.AuthId);
                if (user.Name != null) lBModel.Name = user.Name;
                if (user.UserName != null) lBModel.UserName = user.UserName;
                lBModels.Add(lBModel);
            }
            return lBModels;
        }

    }
}
