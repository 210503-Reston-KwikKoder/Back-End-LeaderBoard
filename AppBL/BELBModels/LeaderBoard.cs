using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELBModels
{
    public class LeaderBoard
    {
        public LeaderBoard() { }
        public string AuthId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public double AverageWPM { get; set; }
        public double AverageAcc { get; set; }
        public int CatID { get; set; }
    }
}
