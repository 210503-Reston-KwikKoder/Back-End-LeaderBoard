using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using LeaderboardBusinessLayer;
using LeaderboardModels;
using LeaderboardRest.Controllers;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LeaderboardDataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LeaderboardRest.DTO;

namespace BELBTests
{
    public class ControllerTest
    {
        private readonly DbContextOptions<LeaderboardDBContext> options;
        public ControllerTest()
        {
            options = new DbContextOptionsBuilder<LeaderboardDBContext>().UseSqlite("Filename=Test.db;foreign keys=false").Options;
        }

        [Fact]
        public async Task GetAllLeaderboardsShouldReturnList()
        {
            var mockBL = new Mock<ILeaderboardBusinessLogic>();
            mockBL.Setup(x => x.GetAllLeaderboards()).ReturnsAsync(
                new List<LeaderBoard>
                {
                    new LeaderBoard(){
                        AuthId = "CM",
                        AverageWPM = 25,
                        AverageAcc = 5,
                        CatID = 1
                    },
                    new LeaderBoard(){
                        AuthId = "CM2",
                        AverageWPM = 30,
                        AverageAcc = 6,
                        CatID = 1
                    }
                }
                );
            var mockUserBL = new Mock<IUserBusinessLogic>();
            mockUserBL.Setup(x => x.GetUser("CM")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM",
                        Name = "Jane",
                        UserName = "JaneDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM2")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM2",
                        Name = "John",
                        UserName = "JohnDoe"
                    }
                );
            var s = Options.Create(new ApiSettings());

            var controller = new LeaderboardController(mockBL.Object, s, mockUserBL.Object);
            var result = controller.GetAllLeaderboards();
            var okResult = await result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<LeaderboardModel>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetLeaderboardByCatIDShouldReturnLeaderboardList()
        {
            var mockBL = new Mock<ILeaderboardBusinessLogic>();
            mockBL.Setup(x => x.GetLeaderboardByCatId(1)).ReturnsAsync(
                new List<LeaderBoard>
                {
                    new LeaderBoard(){
                        AuthId = "CM",
                        AverageWPM = 25,
                        AverageAcc = 5,
                        CatID = 1
                    },
                    new LeaderBoard(){
                        AuthId = "CM2",
                        AverageWPM = 30,
                        AverageAcc = 6,
                        CatID = 1
                    }
                }
                );
            var mockUserBL = new Mock<IUserBusinessLogic>();
            mockUserBL.Setup(x => x.GetUser("CM")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM",
                        Name = "Jane",
                        UserName = "JaneDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM2")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM2",
                        Name = "John",
                        UserName = "JohnDoe"
                    }
                );
            var s = Options.Create(new ApiSettings());

            var controller = new LeaderboardController(mockBL.Object, s, mockUserBL.Object);
            var result = controller.GetLeaderboardByCatID(1);
            var okResult = await result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<LeaderboardModel>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        /*[Fact]
        Post Is not working anymore
        public async Task AddLeaderboardShouldReturnLeaderboard()
        {
            var mockBL = new Mock<ILeaderboardBL>();
            mockBL.Setup(x => x.AddLeaderboard(It.IsAny<LeaderBoard>())).ReturnsAsync(
                    new LeaderBoard(){
                        AuthId = "CM",
                        UserName = "Cesar123",
                        Name = "Cesar",
                        AverageWPM = 25,
                        AverageAcc = 5,
                        CatID = 1
                    }
                );


            var s = Options.Create(new ApiSettings());

            var controller = new LBController(mockBL.Object, s);
            var result = controller.AddLeaderboard(new LeaderBoard());
            var okResult = await result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<LeaderBoard>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }*/

        [Fact]
        public async Task UpdateLeaderboardShouldReturnLeaderboardList()
        {
            var mockBL = new Mock<ILeaderboardBusinessLogic>();
            mockBL.Setup(x => x.Updatedleaderboard(It.IsAny<List<LeaderBoard>>())).ReturnsAsync(
                new List<LeaderBoard>
                {
                    new LeaderBoard(){
                        AuthId = "CM",
                        AverageWPM = 25,
                        AverageAcc = 5,
                        CatID = 1
                    },
                    new LeaderBoard(){
                        AuthId = "CM2",

                        AverageWPM = 30,
                        AverageAcc = 6,
                        CatID = 1
                    }
                }
                );

            var s = Options.Create(new ApiSettings());

            var mockUserBL = new Mock<IUserBusinessLogic>();
            mockUserBL.Setup(x => x.GetUser("CM")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM",
                        Name = "Jane",
                        UserName = "JaneDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM2")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM2",
                        Name = "John",
                        UserName = "JohnDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM3")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM3",
                        Name = "Doe",
                        UserName = "DoeJohn"
                    }
                );
            

            var controller = new LeaderboardController(mockBL.Object, s, mockUserBL.Object);
            var result = controller.UpdateLeaderboard(new List<LeaderboardModel>()
            {
                new LeaderboardModel(new LeaderBoard(){
                        AuthId = "BZ",
                        AverageWPM = 60,
                        AverageAcc = 3,
                        CatID = 1
                    })
                {
                    Name = "Name",
                    UserName = "UserName"
                }
            }
                ); //Changed the function
            var okResult = await result as NoContentResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is NoContentResult);
            //Assert.IsType<List<LeaderBoard>>(okResult.Value);
            Assert.Equal(StatusCodes.Status204NoContent, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteLeaderboardShouldReturnsNoContent()
        {
            var mockBL = new Mock<ILeaderboardBusinessLogic>();
            mockBL.Setup(x => x.DeleteLeaderboard(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync("ok");


            var s = Options.Create(new ApiSettings());

            var mockUserBL = new Mock<IUserBusinessLogic>();
            mockUserBL.Setup(x => x.GetUser("CM")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM",
                        Name = "Jane",
                        UserName = "JaneDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM2")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM2",
                        Name = "John",
                        UserName = "JohnDoe"
                    }
                );

            var controller = new LeaderboardController(mockBL.Object, s, mockUserBL.Object);
            var result = controller.DeleteLeaderboard("test", 1);
            var okResult = await result as OkObjectResult;
            Assert.Null(okResult);
        }

        [Fact]
        public async Task DeleteLeaderboardShouldReturnsExceptions()
        {
            var mockBL = new Mock<ILeaderboardBusinessLogic>();
            mockBL.Setup(x => x.DeleteLeaderboard(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception("exception test"));


            var s = Options.Create(new ApiSettings());

            var mockUserBL = new Mock<IUserBusinessLogic>();
            mockUserBL.Setup(x => x.GetUser("CM")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM",
                        Name = "Jane",
                        UserName = "JaneDoe"
                    }
                );
            mockUserBL.Setup(x => x.GetUser("CM2")).ReturnsAsync(
                    new User()
                    {
                        AuthId = "CM2",
                        Name = "John",
                        UserName = "JohnDoe"
                    }
                );
            

            var controller = new LeaderboardController(mockBL.Object, s, mockUserBL.Object);
            var result = controller.DeleteLeaderboard("test", 1);
            var okResult = await result as OkObjectResult;
            Assert.Null(okResult);
        }
    }
}
