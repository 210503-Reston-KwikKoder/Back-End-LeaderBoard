using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GACDModels;

namespace GACDDL
{
    public class GACDDBContext:DbContext
    {
        public GACDDBContext(DbContextOptions options): base(options) { }
        protected GACDDBContext() { }
        
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
