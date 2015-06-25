using System;
using System.Data.Entity;

namespace Backend.Models
{
    public interface IApplicationDbContext : IDisposable 
    {
        IDbSet<Ingredient> Ingredients { get; set; }
        IDbSet<Meal> Meals { get; set; }
        IDbSet<MealPlan> MealPlans { get; set; }
        int SaveChanges();
    }
}