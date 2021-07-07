using System;
using System.Collections.Generic;
using BELBModels;
using System.Threading.Tasks;

namespace BELBDL
{
    public interface IRepo
    {
      
        /// </summary>
        /// <param name="leaderBoard"></param>
        /// <returns>an individual leaderboard</returns>
        Task<LeaderBoard> AddLeaderboardAsync(LeaderBoard leaderBoard);
        /// <summary>
        /// Function that deletes a leaderboard given the authID and the catagory ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cID"></param>
        Task<string> DeleteLeaderboardAsync(string id, int cID);
        /// </summary>
        /// <returns>gets all leaderboards</returns>
        Task<List<LeaderBoard>> GetAllLeaderboards();
        ///</summary>
        /// <param name="id"></param>
        /// <returns>all Leaderboards that have the same catagory ID</returns>
        Task<List<LeaderBoard>> GetLeaderboardByCatId(int id);
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="u">User u to add to database</param>
        /// <returns>Id string of user</returns>
        Task<string> AddUser(User u);
        /// <summary>
        /// Given a user's authId, gets their entry in the db
        /// </summary>
        /// <param name="Id">AuthId of user</param>
        /// <returns>User with associated Id</returns>
        Task<User> GetUser(string Id);
        
    }
}
