using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
       // public virtual ICollection<Meal> Meals { get; set; }
        public virtual Meal[] Meals { get; set; }

    }
}