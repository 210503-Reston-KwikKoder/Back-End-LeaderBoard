using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BELBDL
{
    public class LeaderboardDbContext:DbContext
    {
        public LeaderboardDbContext(DbContextOptions options): base(options) { }
        protected LeaderboardDbContext() { }
        public DbSet<LeaderBoard> LeaderBoards { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<LeaderBoard>()
                .HasKey(Lb => new { Lb.AuthId, Lb.CatID });
            
        }
    }
}
