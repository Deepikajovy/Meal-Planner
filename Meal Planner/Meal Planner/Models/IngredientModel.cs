using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meal_Planner.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public decimal Quantity { get; set; }
        public string Measurement { get; set; }
        public virtual MealModel Meal { get; set; }
        
    }
}