using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class MealPlanData
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<MealPlan> MealPlans { get; set; }
    }
}