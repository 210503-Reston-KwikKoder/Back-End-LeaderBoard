using System;
using System.Collections.Generic;
using BELBModels;
using System.Threading.Tasks;

namespace BELBDL
{
    public interface IRepo
    {
        
        Task<Category> AddCategory(Category cat);
        /// <summary>
        /// Retrieves all categories currently in the database
        /// </summary>
        /// <returns>List of categories found</returns>
        Task<List<Category>> GetAllCategories();
        /// <summary>
        /// Gets a category by it's Octokit.Language int name
        /// </summary>
        /// <param name="name">name of category requested</param>
        /// <returns>Category requested</returns>
        Task<Category> GetCategoryByName(int name);
        /// <summary>
        /// Versatile method to update a user's stats for a given category
        /// </summary>
        /// <param name="categoryid">category user participated in</param>
        /// <param name="userid">user id related to user</param>
        /// <returns>userstat updated</returns>


        //Leaderboard section
        Task<LeaderBoard> AddLeaderboardAsync(LeaderBoard leaderBoard);
        Task<LeaderBoard> DeleteLeaderboardAsync(LeaderBoard leaderBoard);
        Task<LeaderBoard> UpdateLeaderboardAsync(LeaderBoard leaderBoard);
        Task<List<LeaderBoard>> GetAllLeaderboards();
        Task<LeaderBoard> GetLeaderboardById(int id);

    }
}
