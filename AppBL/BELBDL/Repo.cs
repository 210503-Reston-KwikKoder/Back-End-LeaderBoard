using LeaderboardModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardDataLayer
{
    public class Repo : IRepo
    {
        private readonly LeaderboardDBContext _context;
        public Repo(LeaderboardDBContext context)
        {
            _context = context;
            Log.Debug("Repo instantiated");
        }
                
        /// </summary>
        /// <param name="leaderBoard"></param>
        /// <returns>an individual leaderboard</returns>

        public async Task<LeaderBoard> AddLeaderboardAsync(LeaderBoard leaderBoard)
        {
            try
            {
                await _context.LeaderBoards.AddAsync(
                leaderBoard
                );
                await _context.SaveChangesAsync();
                return leaderBoard;
            } catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Failed to add Leader Board with catID: " + leaderBoard.CatID + " and AuthID: " + leaderBoard.AuthId);
                return null;
            }
        
        }

        
        /// </summary>
        /// <param name="leaderdbrs"></param>
        /// <returns>list of leaderboards added/udpated</returns>
        public async Task<List<LeaderBoard>> Updatedleaderboard(List<LeaderBoard> leaderdbrs)
        {
            
            
            foreach(LeaderBoard user in leaderdbrs )
            {
                try{
                    _context.LeaderBoards.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch(Exception e){
                    await  _context.LeaderBoards.AddAsync(user);
                    Log.Information(e.ToString());

                }
            }
            await _context.SaveChangesAsync();
            return leaderdbrs;
        }
        /// <summary>
        /// Function that deletes a leaderboard given the authID and the catagory ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cID"></param>
        public async Task<string> DeleteLeaderboardAsync(string id, int cID)
        {
            LeaderBoard toBeDeleted = await _context.LeaderBoards.AsNoTracking().FirstAsync(ldr => ldr.AuthId == id  && ldr.CatID == cID);
            _context.LeaderBoards.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            return id;
        }
        
        
        /// </summary>
        /// <returns>gets all leaderboards</returns>
        public async Task<List<LeaderBoard>> GetAllLeaderboards()
        {
            return await _context.LeaderBoards.Select(c => c)
                .ToListAsync();
        }
         
        ///</summary>
        /// <param name="id"></param>
        /// <returns>all Leaderboards that have the same catagory ID</returns>
        public async Task<List<LeaderBoard>> GetLeaderboardByCatId(int id)
        {
            try
            {
                return await (from c in _context.LeaderBoards
                                where c.CatID == id
                                orderby c.AverageWPM descending
                                select c).ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Error finding LeaderBoard returning null");
                return null;
            }
        }
            /// </summary>
        /// <param name="u">User u to add to database</param>
        /// <returns>Id string of user</returns>
        public async Task<string> AddUser(User u)
        {
            try
            {
                await _context.Users.AddAsync(u);
                await _context.SaveChangesAsync();
                return u.AuthId;
            }
            catch(Exception e)
            {
                Log.Error(e.StackTrace);
                return null;
            }
        }
        
       /// </summary>
        /// <param name="Id">AuthId of user</param>
        /// <returns>User with associated Id</returns>
        public async Task<User> GetUser(string Id)
        {
            try {
                return await (from u in _context.Users
                              where u.AuthId == Id
                              select u).SingleAsync();
            }
            catch(Exception e)
            {
                Log.Information(e.Message);
                Log.Information("User not found");
                return null;
            }
        }
    }
}
