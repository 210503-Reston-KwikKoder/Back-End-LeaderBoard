﻿using BELBModels;
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
            try
            {
                await _context.LeaderBoards.AddAsync(
                leaderBoard
                );
                await _context.SaveChangesAsync();
                return leaderBoard;
            } catch(Exception e)
            {
                Log.Error("Failed to add Leader Board with catID: " + leaderBoard.CatID + " and AuthID: " + leaderBoard.AuthId);
                return null;
            }
        
        }
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
        public async Task<string> DeleteLeaderboardAsync(string id, int cID)
        {
            LeaderBoard toBeDeleted = await _context.LeaderBoards.AsNoTracking().FirstAsync(ldr => ldr.AuthId == id  && ldr.CatID == cID);
            _context.LeaderBoards.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            return id;
        }
     
        public async Task<List<LeaderBoard>> GetAllLeaderboards()
        {
            return await _context.LeaderBoards.Select(c => c)
                .ToListAsync();
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
        
    }
}
