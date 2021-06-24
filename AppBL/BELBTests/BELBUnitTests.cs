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
        /// <summary>
        /// Makes sure UserStats updates and doesn't fail
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
       
        /// <summary>
        /// Makes sure there aren't any critical failures in Category Leaderboard / Stat methods
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
        [Fact]
        public async Task CategoryLeaderBoardShouldReturnAnyNumberofUsers()
        {
            using (var context = new BELBDBContext(options))
            {
                IUserBL userBL = new UserBL(context);
                ICategoryBL categoryBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);

                User user = new User();
                user.Auth0Id = "test";
                await userBL.AddUser(user);
                User user1 = new User();
                user1.Auth0Id = "test1";
                await userBL.AddUser(user1);
                User user2 = new User();
                user2.Auth0Id = "test2";
                await userBL.AddUser(user2);
                TypeTest testToBeInserted = await userStatBL.SaveTypeTest(1, 50, 100, 100, DateTime.Now);
                await userStatBL.AddTestUpdateStat(1, 1, testToBeInserted);
                TypeTest testToBeInserted1 = await userStatBL.SaveTypeTest(2, 50, 100, 100, DateTime.Now);
                await userStatBL.AddTestUpdateStat(2, 1, testToBeInserted1);
                TypeTest testToBeInserted2 = await userStatBL.SaveTypeTest(3, 50, 100, 100,  DateTime.Now);
                await userStatBL.AddTestUpdateStat(3, 1, testToBeInserted2);
                bool expected = true;
                bool actual = (await userStatBL.GetBestUsersForCategory(1)).Count > 0;
                Assert.Equal(expected, actual);
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
        /// Makes sure competition will show that count is one when we add a competition
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetCompetitionsShouldGetAComp()
        {
            using (var context = new BELBDBContext(options))
            {
                Competition c = new Competition();
                User user = new User();
                user.Auth0Id = "test";
                IUserBL userBL = new UserBL(context);
                ICategoryBL categoryBL = new CategoryBL(context);
                IUserStatBL userStatBL = new UserStatBL(context);
                ICompBL compBL = new CompBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                await userBL.AddUser(user);
                Category category1 = await categoryBL.GetCategory(1);
                string testForComp = "Console.WriteLine('Hello World');";
                await compBL.AddCompetition(DateTime.Now, DateTime.Now, category1.Id, "name", 1, testForComp, "testauthor");
                int expected = 1;
                int actual = (await compBL.GetAllCompetitions()).Count;
                Assert.Equal(expected, actual);
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
