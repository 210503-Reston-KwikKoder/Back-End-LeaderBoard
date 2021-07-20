using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaderboardModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeaderboardDataLayer
{
    public class LeaderboardDBContext:DbContext
    {
        public LeaderboardDBContext(DbContextOptions options): base(options) { }
        protected LeaderboardDBContext() { }
        public DbSet<LeaderBoard> LeaderBoards { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<LeaderBoard>()
                .HasKey(Lb => new { Lb.AuthId, Lb.CatID });
            
        }
    }
}
