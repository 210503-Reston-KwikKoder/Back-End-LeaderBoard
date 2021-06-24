using System;
using Xunit;
using BELBModels;
using BELBBL;
using BELBDL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Serilog;
namespace BELBTests
{
    public class BELBUnitTests
    {
        private readonly DbContextOptions<BELBDBContext> options;
        public BELBUnitTests()
        {
            options = new DbContextOptionsBuilder<BELBDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        /// <summary>
        /// Method to make sure AddUser adds a user to the db
        /// </summary>
        /// <returns></returns>
        
        [Fact]
        public async Task AddCatShouldAddCatAsync()
        {
            using(var context = new BELBDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                Category category1 = new Category();
                category1.Name = 2;
                await categoryBL.AddCategory(category1);
                Category category2 = new Category();
                category2.Name = 3;
                await categoryBL.AddCategory(category2);
                int catCount = (await categoryBL.GetAllCategories()).Count;
                int expected = 3;
                Assert.Equal(expected, catCount);
            }
        }
   
        [Fact]
        public async Task AddingCategoryTwiceShouldBeNull()
        {
            using( var context  = new BELBDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                Assert.Null(await categoryBL.AddCategory(category));
            }        
        }
        
        [Fact]
        public async Task GetCategoryByIdShouldWork()
        {
            using (var context = new BELBDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 3;
                await categoryBL.AddCategory(category);
                Category category1 = await categoryBL.GetCategoryById(1);
                int expected = 3;
                int actual = category1.Name;
                Assert.Equal(expected, actual);
            }
        }

        /// <summary>
        /// Leaderboard tests
        /// </summary>

        /*[Fact]
        public async Task AddLeaderboardShouldAddLeaderboardAsync()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard();
                leaderboard.AuthId = "CM";
                leaderboard.UserName = "Cesar123";
                leaderboard.Name = "Cesar";
                leaderboard.AverageWPM = 25;
                leaderboard.AverageAcc = 5;
                leaderboard.CatID = 1;
                //category.Name = 1;
                await leaderboardBL.AddLeaderboard(leaderboard);
                LeaderBoard leaderboard1 = new LeaderBoard();
                leaderboard1.AuthId = "CM";
                leaderboard1.UserName = "Cesar123";
                leaderboard1.Name = "Cesar";
                leaderboard1.AverageWPM = 65;
                leaderboard1.AverageAcc = 50;
                leaderboard1.CatID = 3;
                //category1.Name = 2;
                await leaderboardBL.AddLeaderboard(leaderboard1);
                LeaderBoard leaderboard2 = new LeaderBoard();
                leaderboard2.AuthId = "RF";
                leaderboard2.UserName = "Robbie789";
                leaderboard2.Name = "Robbie";
                leaderboard2.AverageWPM = 85;
                leaderboard2.AverageAcc = 96;
                leaderboard2.CatID = 1;
                //category2.Name = 3;
                await leaderboardBL.AddLeaderboard(leaderboard2);
                int catCount = (await leaderboardBL.GetAllLeaderboards()).Count;
                int expected = 5;
                Assert.Equal(expected, catCount);
            }
        }*/

        [Fact]
        public async Task AddingLeaderboardTwiceShouldBeNull()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard();
                leaderboard.AuthId = "CM";
                leaderboard.UserName = "Cesar123";
                leaderboard.Name = "Cesar";
                leaderboard.AverageWPM = 25;
                leaderboard.AverageAcc = 5;
                leaderboard.CatID = 1;
                leaderboardBL.AddLeaderboard(leaderboard);
                Assert.Null(await leaderboardBL.AddLeaderboard(leaderboard));
            }
        }

        [Fact]
        public async Task GetLeaderboardByCatShouldWork()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard1 = new LeaderBoard();
                leaderboard1.AuthId = "CM";
                leaderboard1.UserName = "Cesar123";
                leaderboard1.Name = "Cesar";
                leaderboard1.AverageWPM = 65;
                leaderboard1.AverageAcc = 50;
                leaderboard1.CatID = 3;
                await leaderboardBL.AddLeaderboard(leaderboard1);
                int lbCount = (await leaderboardBL.GetLeaderboardByCatId(3)).Count;
                int expected = 1;
                Assert.Equal(expected, lbCount);
            }
        }

        private void Seed()
        {
            using(var context = new BELBDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
