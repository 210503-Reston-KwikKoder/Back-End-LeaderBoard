using System;
using Xunit;
using LeaderboardModels;
using LeaderboardBusinessLayer;
using LeaderboardDataLayer;
using LeaderboardRest;
//using LeaderboardRest.DTO; Dont need DTO anymore
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BELBTests
{
    public class LeaderboardUnitTests
    {
        private readonly DbContextOptions<LeaderboardDBContext> options;
        public LeaderboardUnitTests()
        {
            options = new DbContextOptionsBuilder<LeaderboardDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }
        

        /// <summary>
        /// Leaderboard tests
        /// </summary>


        [Fact]
        public async Task AddingLeaderboardTwiceShouldBeNull()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard = new LeaderBoard();
                leaderboard.AuthId = "CM";
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
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
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
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
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
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard1 = new LeaderBoard();
                leaderboard1.AuthId = "CM";
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
        public async Task GetLeaderboardByCatShouldNotWork()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard1 = new LeaderBoard();
                leaderboard1.AuthId = "CM";
                leaderboard1.AverageWPM = 65;
                leaderboard1.AverageAcc = 50;
                leaderboard1.CatID = 3;
                await leaderboardBL.AddLeaderboard(leaderboard1);
                int lbCount = (await leaderboardBL.GetLeaderboardByCatId(4)).Count;
                int expected = 0;
                Assert.Equal(expected, lbCount);
            }
        }
        [Fact]
        public async Task GetAllLeaderBoardsShouldWork()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                LeaderBoard leaderboard = new LeaderBoard()
                {
                    AuthId = "CM",
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
            using (var context = new LeaderboardDBContext(options))
            {
                ILeaderboardBusinessLogic leaderboardBL = new LeaderboardBusinessLayer.LeaderboardBusinessLogic(context);
                List<LeaderBoard> lst = new List<LeaderBoard>()
                {
                        new LeaderBoard{
                                AuthId = "CM",
                                AverageWPM = 64,
                                AverageAcc = 55,
                                CatID = 1
                        },
                        new LeaderBoard{
                                AuthId = "RM",
                                AverageWPM = 44,
                                AverageAcc = 11,
                                CatID = 2
                        },

                };

                
                await leaderboardBL.Updatedleaderboard(lst);

                List<LeaderBoard> tobeUpdated = await leaderboardBL.GetAllLeaderboards();
                

                List<LeaderBoard> Result = await leaderboardBL.GetAllLeaderboards();


                int expected = 11;
                // This should create an average leaderboard under CatID -2
                Assert.Equal(Result[1].AverageAcc, expected);
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
        public void CheckScopeAuthShouldThrowAnexception()
        {
            Assert.Throws<ArgumentNullException>(() => new CheckScopeAuth(null, null));
        }

        [Fact]
        public void CheckScopeAuthShouldWork()
        {
            CheckScopeAuth newAuth = new CheckScopeAuth("testScope", "testIssuer");
            string expectedScope = "testScope";
            string expectedIssue = "testIssuer";
            Assert.Equal(newAuth.Scope, expectedScope);
            Assert.Equal(newAuth.Issuer, expectedIssue);

        }

        [Fact]
        public async Task AddUserShouldAddUserAsync()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                IUserBusinessLogic userBL = new UserBusinessLogic(context);
                User user = new User()
                {
                    AuthId = "test",
                    Name = "Jane",
                    UserName = "JaneDoe"
                };
                await userBL.AddUser(user);
                string expected = "JaneDoe";
                string actual = (await userBL.GetUser("test")).UserName;
                // This should create an average leaderboard under CatID -2
                Assert.Equal(actual, expected);
            }

        }
        [Fact] 
        public async Task UserShouldNotBeAddedTwice()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                IUserBusinessLogic userBL = new UserBusinessLogic(context);
                User user = new User()
                {
                    AuthId = "test",
                    Name = "Jane",
                    UserName = "JaneDoe"
                };
                await userBL.AddUser(user);
                Assert.Null(await userBL.AddUser(user));
                
            }
        }
        [Fact]
        public async Task GettingUserNotInDBShouldBeNull()
        {
            using (var context = new LeaderboardDBContext(options))
            {
                IUserBusinessLogic userBL = new UserBusinessLogic(context);
                Assert.Null(await userBL.GetUser("user"));

            }
        }
        private void Seed()
        {
            using(var context = new LeaderboardDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
