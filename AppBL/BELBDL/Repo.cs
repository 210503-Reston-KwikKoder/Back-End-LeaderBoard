using BELBModels;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELBDL
{
    public class Repo : IRepo
    {
        private readonly BELBDBContext _context;
        public Repo(BELBDBContext context)
        {
            _context = context;
            Log.Debug("Repo instantiated");
        }
        public async Task<Category> AddCategory(Category cat)
        {
            try
            {
                //Make sure category doesn't already exists
                await (from c in _context.Categories
                       where c.Name == cat.Name
                       select c).SingleAsync();
                return null;
            }catch (Exception)
            {
                await _context.Categories.AddAsync(cat);
                await _context.SaveChangesAsync();
                Log.Information("New category created.");
                return cat;
            }
        }     
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await (from c in _context.Categories
                        select c).ToListAsync();
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }
        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                return await(from c in _context.Categories
                             where c.CId == id
                             select c).SingleAsync();
            }catch(Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Error finding category returning null");
                return null;
            }
        }
        public async Task<Category> GetCategoryByName(int name)
        {
            try
            {
                return await (from cat in _context.Categories
                             where cat.Name == name
                             select cat).SingleAsync();
            } catch(Exception e)
            {
                Log.Information(e.StackTrace);
                Log.Information("No such category found");
                return null;

            }
        }

        //Leaderboard Section

        public async Task<LeaderBoard> AddLeaderboardAsync(LeaderBoard leaderBoard)
        {
                
                await _context.LeaderBoards.AddAsync(
                    leaderBoard
                    );
                await _context.SaveChangesAsync();

            try
            {
                await _context.LeaderBoards.AsNoTracking().FirstAsync(c => c.AuthId == leaderBoard.AuthId && c.CatID == -2);


                // Averages for the updated user, all their AverageWPM by category into an overall and stored into CatID= -2
                _context.LeaderBoards.Update(_context.LeaderBoards.Select(c => c)
                .Where(u => u.AuthId == leaderBoard.AuthId)
                .GroupBy(g => new { uid = g.AuthId, Un = g.UserName, n = g.Name })
                .Select(lb => new LeaderBoard()
                {
                    AuthId = lb.Key.uid,
                    UserName = lb.Key.Un,
                    Name = lb.Key.n,
                    AverageWPM = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageWPM),
                    AverageAcc = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageAcc),
                    CatID = -2
                })
                    .OrderBy(c => c.AverageWPM)
                    .SingleAsync().Result);
                
            } catch(Exception e)
            {
                // Averages for the updated user, all their AverageWPM by category into an overall and stored into CatID= -2
                await _context.LeaderBoards.AddAsync(_context.LeaderBoards.Select(c => c)
                .Where(u => u.AuthId == leaderBoard.AuthId)
                .GroupBy(g => new { uid = g.AuthId, Un = g.UserName, n = g.Name })
                .Select(lb => new LeaderBoard()
                {
                    AuthId = lb.Key.uid,
                    UserName = lb.Key.Un,
                    Name = lb.Key.n,
                    AverageWPM = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageWPM),
                    AverageAcc = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageAcc),
                    CatID = -2
                })
                    .OrderBy(c => c.AverageWPM)
                    .SingleAsync().Result);
            }
            await _context.SaveChangesAsync();
            return leaderBoard;
        }
        public async Task<string> DeleteLeaderboardAsync(string id, int cID)
        {
            LeaderBoard toBeDeleted = await _context.LeaderBoards.AsNoTracking().FirstAsync(ldr => ldr.AuthId == id  && ldr.CatID == cID);
            _context.LeaderBoards.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            return id;
        }
        public async Task<LeaderBoard> UpdateLeaderboardAsync(LeaderBoard leaderBoard)
        {
            _context.LeaderBoards.Update(leaderBoard);
            await _context.SaveChangesAsync();
            return leaderBoard;
        }
        public async Task<List<LeaderBoard>> GetAllLeaderboards()
        {
            return await _context.LeaderBoards.Select(c => c)
                .GroupBy(g => new { uid = g.AuthId, Un = g.UserName, n = g.Name })
                .Select(lb => new LeaderBoard()
                {
                    AuthId = lb.Key.uid,
                    UserName = lb.Key.Un,
                    Name = lb.Key.n,
                    AverageWPM = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageWPM),
                    AverageAcc = lb.Where(x => lb.Key.uid == x.AuthId).Average(x => x.AverageAcc),
                    CatID = -2
                })
                    .OrderBy(c=>c.AverageWPM)
                    .ToListAsync();
        }
        public async Task<LeaderBoard> GetLeaderboardById(int cID)
        {
            try
            {
                return await (from c in _context.LeaderBoards
                              where c.CatID == cID
                              select c).SingleAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Error finding LeaderBoard returning null");
                return null;
            }
        }

        public async Task<List<LeaderBoard>> GetLeaderboardByCatId(int id)
        {
            try
            {
                return await (from c in _context.LeaderBoards
                              where c.CatID == id
                              select c).ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                Log.Error("Error finding LeaderBoard returning null");
                return null;
            }
        }
        /*
        pubic async Task<List<Leaderboard>> Top100(int id)
        {
            
            
            
            
        }
         
         
         */
    }
}
