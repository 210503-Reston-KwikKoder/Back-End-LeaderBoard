using BELBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBDL;

namespace BELBBL
{
    public class LeaderboardBusinessLayer : ILeaderboardBusinessLayer
    {
        private readonly Repo _repo;
        public LeaderboardBusinessLayer(LeaderboardDbContext context)
        {
            _repo = new Repo(context);
        }

        /// </summary>
        /// <param name="leaderBoard"></param>
        /// <returns>the added leaderboard</returns>
        public async Task<LeaderBoard> AddLeaderboard(LeaderBoard leaderBoard)
        {
            return await _repo.AddLeaderboardAsync(leaderBoard);
        }

        /// </summary>
        /// <param name="id"></param>
        /// <param name="cID"></param>
        /// <returns>the removed leaderboard</returns>
        public async Task<string> DeleteLeaderboard(string id, int cID)
        {
            return await _repo.DeleteLeaderboardAsync(id, cID);
        }

        /// </summary>
        /// <returns>All of the leaderboards</returns>
        public async Task<List<LeaderBoard>> GetAllLeaderboards()
        {
            return await _repo.GetAllLeaderboards();
        }

         /// </summary>
        /// <param name="id"></param>
        /// <returns>All of the leaderboards with the inserted catagory ID</returns>
        public async Task<List<LeaderBoard>> GetLeaderboardByCatId(int id)
        {
            return await _repo.GetLeaderboardByCatId(id);
        }

          /// </summary>
        /// <param name="leaderdbrs"></param>
        /// <returns>The list of updated/added leaderboards</returns>
        public Task<List<LeaderBoard>> Updatedleaderboard(List<LeaderBoard> leaderdbrs)
        {
            return  _repo.Updatedleaderboard(leaderdbrs);
        }
    }
}
