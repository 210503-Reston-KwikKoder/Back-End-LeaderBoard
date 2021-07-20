using LeaderboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardRest.DTO
{
    public class LeaderboardModel : LeaderBoard
    {
        public LeaderboardModel() { }
        public LeaderboardModel (LeaderBoard leaderBoard)
        {
            this.AuthId = leaderBoard.AuthId;
            this.AverageWPM = leaderBoard.AverageWPM;
            this.AverageAcc = leaderBoard.AverageAcc;
            this.CatID = leaderBoard.CatID;
        }
        public string Name { get; set; }
        public string UserName { get; set; }
    }    
}
