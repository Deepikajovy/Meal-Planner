using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Backend.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MealPlanItem> MealPlanItems { get; set; }
    }
}