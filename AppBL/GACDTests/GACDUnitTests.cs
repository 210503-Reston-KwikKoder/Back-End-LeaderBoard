using System;
using Xunit;
using GACDModels;
using GACDBL;
using GACDDL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Serilog;
namespace GACDTests
{
    public class GACDUnitTests
    {
        private readonly DbContextOptions<GACDDBContext> options;
        public GACDUnitTests()
        {
            options = new DbContextOptionsBuilder<GACDDBContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }

        /// <summary>
        /// Makes sure that Categories can be added
        /// </summary>
        /// <returns>True if successful/False on fail</returns>
        [Fact]
        public async Task AddCatShouldAddCatAsync()
        {
            using(var context = new GACDDBContext(options))
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
        /// Makes sure adding two of the same category returns null
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task AddingCategoryTwiceShouldBeNull()
        {
            using( var context  = new GACDDBContext(options))
            {
                ICategoryBL categoryBL = new CategoryBL(context);
                Category category = new Category();
                category.Name = 1;
                await categoryBL.AddCategory(category);
                Assert.Null(await categoryBL.AddCategory(category));
            }        
        }
        /// <summary>
        /// Makes sure that we are able to get category by id
        /// </summary>
        /// <returns>True on success</returns>
        [Fact]
        public async Task GetCategoryByIdShouldWork()
        {
            using (var context = new GACDDBContext(options))
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
        private void Seed()
        {
            using(var context = new GACDDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
