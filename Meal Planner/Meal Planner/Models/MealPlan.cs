using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meal_Planner.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual MealModel Meal { get; set; }


    }
}