using System;
using Xunit;
using BELBModels;
using BELBBL;
using BELBDL;
using BELBRest;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Serilog;
using System.Collections.Generic;

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
        /// Leaderboard tests
        /// </summary>


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
                await leaderboardBL.AddLeaderboard(leaderboard);
                Assert.Null(await leaderboardBL.AddLeaderboard(leaderboard));
            }
        }

        [Fact]
        public async Task AddingLeaderBoardShouldCreateOrUpdateAnAverageLeaderBoard()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
                    UserName = "Cesar123",
                    Name = "Cesar",
                    AverageWPM = 25,
                    AverageAcc = 5,
                    CatID = 1
                };
                await leaderboardBL.AddLeaderboard(leaderboard);
                List<LeaderBoard> Result = await leaderboardBL.GetLeaderboardByCatId(-2);

                // This should create an average leaderboard under CatID -2
                Assert.NotNull(Result);
            }
        }

        [Fact]
        public async Task deleteLeaderBoardShouldRemoveLeaderBoard()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
                    UserName = "Cesar123",
                    Name = "Cesar",
                    AverageWPM = 25,
                    AverageAcc = 5,
                    CatID = 1
                };
                await leaderboardBL.AddLeaderboard(leaderboard);
                leaderboardBL.DeleteLeaderboard(leaderboard.AuthId, leaderboard.CatID);
                List<LeaderBoard> Result = await leaderboardBL.GetLeaderboardByCatId(1);

                int expected = 1;
                // This should create an average leaderboard under CatID -2
                Assert.Equal(Result.Count, expected);
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

        [Fact]
        public async Task GetAllLeaderBoardsShouldWork()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
                    UserName = "Cesar123",
                    Name = "Cesar",
                    AverageWPM = 25,
                    AverageAcc = 5,
                    CatID = 1
                };
                await leaderboardBL.AddLeaderboard(leaderboard);
                
                List<LeaderBoard> Result = await leaderboardBL.GetAllLeaderboards();

                int expected = 1;
                // This should create an average leaderboard under CatID -2
                Assert.Equal(Result.Count, expected);
            }
        }

        [Fact]
        public async Task UpdateLeaderboardShouldWork()
        {
            using (var context = new BELBDBContext(options))
            {
                ILeaderboardBL leaderboardBL = new LeaderboardBL(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
                    UserName = "Cesar123",
                    Name = "Cesar",
                    AverageWPM = 25,
                    AverageAcc = 5,
                    CatID = 1
                };
                await leaderboardBL.AddLeaderboard(leaderboard);

                List<LeaderBoard> tobeUpdated = await leaderboardBL.GetAllLeaderboards();
                tobeUpdated[0].AverageAcc = 6;

                await leaderboardBL.Updatedleaderboard(tobeUpdated);

                List<LeaderBoard> Result = await leaderboardBL.GetAllLeaderboards();


                int expected = 6;
                // This should create an average leaderboard under CatID -2
                Assert.Equal(Result[0].AverageAcc, expected);
            }
        }

        [Fact]
        public void ApiSettingsShouldWork()
        {
            ApiSettings apiSettings = new ApiSettings();
            string gitKey = "testGitKey";
            string AuthKey = "testAuthKey";

            apiSettings.githubApiKey = gitKey;
            apiSettings.authString = AuthKey;

            Assert.Equal(gitKey, apiSettings.githubApiKey);
            Assert.Equal(AuthKey, apiSettings.authString);
        }

        [Fact]
        public void CategoryShouldWork()
        {
            Category cat = new Category();
            int expected = 1;

            cat.CId = 1;
            cat.Name = 1;

            Assert.Equal(expected, cat.CId);
            Assert.Equal(expected, cat.Name);
        }

        [Fact]
        public void CheckScopeAuthShouldWork()
        {
            Assert.Throws<ArgumentNullException>(() => new CheckScopeAuth(null, null));
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
