using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BELBRest.DTO
{
    public class userstats
    {
        public userstats() { }
        public string UserName { get; set; }
        public string Name { get; set; }
        public double AverageWPM { get; set; }
        public double AverageAcc { get; set; }
        public double Average { get; set; }

    }
}
