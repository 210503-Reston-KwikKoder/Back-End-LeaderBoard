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
    public class BELBDBContext:DbContext
    {
        public BELBDBContext(DbContextOptions options): base(options) { }
        protected BELBDBContext() { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<LeaderBoard> LeaderBoards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(cat => cat.CId).ValueGeneratedOnAdd().HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity<LeaderBoard>()
                .HasKey(Lb => new { Lb.AuthId, Lb.CatID });
            modelBuilder.Entity<Category>()
                .HasKey(cat => cat.CId);

        }
    }
}
