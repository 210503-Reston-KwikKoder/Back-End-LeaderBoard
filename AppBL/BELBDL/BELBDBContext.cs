using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBModels;

namespace BELBDL
{
    public class BELBDBContext:DbContext
    {
        public BELBDBContext(DbContextOptions options): base(options) { }
        protected BELBDBContext() { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<LeaderBoard> LeaderBoards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaderBoard>().Property(Lb => Lb.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(cat => cat.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<LeaderBoard>()
                .HasKey(Lb => Lb.Id);
            modelBuilder.Entity<Category>()
                .HasKey(cat => cat.Id);

        }
    }
}
