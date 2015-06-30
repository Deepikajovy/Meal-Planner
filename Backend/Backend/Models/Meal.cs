﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<MealPlanItem> MealPlanItems { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}