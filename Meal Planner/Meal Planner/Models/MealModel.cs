using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meal_Planner.Models
{
    public class MealModel
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public string MealUrl { get; set; }
        public virtual ICollection<IngredientModel> MealIngredients { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}