using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Meal Meal { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
