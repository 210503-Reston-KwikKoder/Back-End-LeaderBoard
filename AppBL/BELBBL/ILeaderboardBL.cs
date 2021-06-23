using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBModels;

namespace BELBBL
{
    public interface ILeaderboardBL
    {
        Task<LeaderBoard> AddLeaderboard(LeaderBoard leaderBoard);
        Task<int> DeleteLeaderboard(int id);
        //Task<LeaderBoard> UpdateLeaderboard((LeaderBoard leaderBoard);
        Task<List<LeaderBoard>> GetAllLeaderboards();
        Task<LeaderBoard> GetLeaderboardById(int id);
        //Task<LeaderBoard> Top100((int id);
        Task<List<LeaderBoard>> GetLeaderboardByCatId(int id);

    }
}
