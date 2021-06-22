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
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>()
                .Property(cat => cat.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<LeaderBoard>()
                .Property(tT => tT.Id)
                      .ValueGeneratedOnAdd();

        }
    }
}
