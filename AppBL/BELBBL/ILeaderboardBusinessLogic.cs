using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderboardModels;

namespace LeaderboardBusinessLayer
{
    public interface ILeaderboardBusinessLogic
    {
        /// </summary>
        /// <param name="leaderBoard"></param>
        /// <returns>the added leaderboard</returns>
        Task<LeaderBoard> AddLeaderboard(LeaderBoard leaderBoard);

        /// </summary>
        /// <param name="id"></param>
        /// <param name="cID"></param>
        /// <returns>the removed leaderboard</returns>
        Task<string> DeleteLeaderboard(string id, int cID);
     
        /// </summary>
        /// <returns>All of the leaderboards</returns>
        Task<List<LeaderBoard>> GetAllLeaderboards();
    
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All of the leaderboards with the inserted catagory ID</returns>
        Task<List<LeaderBoard>> GetLeaderboardByCatId(int id);
    
        /// </summary>
        /// <param name="leaderdbrs"></param>
        /// <returns>The list of updated/added leaderboards</returns>
        Task<List<LeaderBoard>> Updatedleaderboard(List<LeaderBoard> leaderdbrs);
    }
}
