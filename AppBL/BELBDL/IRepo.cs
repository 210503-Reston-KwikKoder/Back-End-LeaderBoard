using System;
using System.Collections.Generic;
using BELBModels;
using System.Threading.Tasks;

namespace BELBDL
{
    public interface IRepo
    {
        //Leaderboard section
        Task<LeaderBoard> AddLeaderboardAsync(LeaderBoard leaderBoard);
        Task<string> DeleteLeaderboardAsync(string id, int cID);
        Task<List<LeaderBoard>> GetAllLeaderboards();
        Task<List<LeaderBoard>> GetLeaderboardByCatId(int id);
    }
}
