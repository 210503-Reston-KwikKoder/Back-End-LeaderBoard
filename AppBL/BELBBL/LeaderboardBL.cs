using BELBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBDL;

namespace BELBBL
{
    public class LeaderboardBL : ILeaderboardBL
    {
        private readonly Repo _repo;
        public LeaderboardBL(BELBDBContext context)
        {
            _repo = new Repo(context);
        }
        public async Task<LeaderBoard> AddLeaderboard(LeaderBoard leaderBoard)
        {
            return await _repo.AddLeaderboardAsync(leaderBoard);
        }
        public async Task<LeaderBoard> DeleteLeaderboard(LeaderBoard leaderBoard)
        {
            return await _repo.DeleteLeaderboardAsync(leaderBoard);
        }
        public async Task<LeaderBoard> UpdateLeaderboard(LeaderBoard leaderBoard)
        {
            return await _repo.UpdateLeaderboardAsync(leaderBoard);
        }
        public async Task<List<LeaderBoard>> GetAllLeaderboards()
        {
            return await _repo.GetAllLeaderboards();
        }
        public async Task<LeaderBoard> GetLeaderboardById(int id)
        {
            return await _repo.GetLeaderboardById(id);
        }
    }
}
