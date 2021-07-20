using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardModels
{
    public class LeaderBoard
    {
        public LeaderBoard() { }
        [ForeignKey("User")]
        public string AuthId { get; set; }
        
        public double AverageWPM { get; set; }
        public double AverageAcc { get; set; }
       
        public int CatID { get; set; }
    }
}
