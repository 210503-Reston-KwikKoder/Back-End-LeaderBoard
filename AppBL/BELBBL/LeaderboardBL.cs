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
        public async Task<int> DeleteLeaderboard(int id)
        {
            return await _repo.DeleteLeaderboardAsync(id);
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
        public async Task<List<LeaderBoard>> GetLeaderboardByCatId(int id)
        {
            return await _repo.GetLeaderboardByCatId(id);
        }
        /*public async Task<List<Leaderboard>> Top100(int id)
         {
        
            return await _repo.Top100(id);
                
          }*/
    }
}
