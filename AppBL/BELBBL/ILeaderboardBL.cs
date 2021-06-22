using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BELBModels;

namespace BELBBL
{
    public interface ILeaderboardBL
    {
        Task<List<LeaderBoard>> GetAllCategories();
        Task<LeaderBoard> GetLeaderboardById(int id);
    }
}
