using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardModels
{
    public class User
    {
        public User() { }
        [Key]
        public string AuthId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
