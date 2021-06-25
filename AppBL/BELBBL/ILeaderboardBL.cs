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
        Task<string> DeleteLeaderboard(string id, int cID);
        Task<List<LeaderBoard>> GetAllLeaderboards();
        Task<List<LeaderBoard>> GetLeaderboardByCatId(int id);
        Task<List<LeaderBoard>> Updatedleaderboard(List<LeaderBoard> leaderdbrs);
    }
}
