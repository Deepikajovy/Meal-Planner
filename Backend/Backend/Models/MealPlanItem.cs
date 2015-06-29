using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class MealPlanItem
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual MealPlan MealPlan { get; set; }
    }
}