using System.Collections.Generic;
using Backend.Models;

namespace Backend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Backend.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Backend.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },uit
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            context.Ingredients.AddOrUpdate(
                p => p.Name,
                new Ingredient { Name = "Pasta",Measurement = "grams"},
                new Ingredient { Name = "Cheese", Measurement = "grams"},
                new Ingredient { Name = "Bacon", Measurement = "grams"}
            );

            context.SaveChanges();

            context.Meals.AddOrUpdate(
                p => p.Name,
                new Meal { Name = "Lasagne",
                    Ingredients = context.Ingredients.Where(w => w.Name == "Pasta" || w.Name == "Cheese").ToList(),
                    Description = "Big Beef lasagne for the win",
                    ImageUrl = "http://mangiarebuono.it/wp-content/uploads/2013/11/lasagna.jpg",
                    },
                new Meal { Name = "Bolognaise",},
                new Meal { Name = "Chocolate Moose" }
            );

            context.SaveChanges();

                context.MealPlans.AddOrUpdate(
                p => p.Name,
                new MealPlan { Name = "Lasagne", Meals = context.Meals.Where(w => w.Name == "Lasagne").ToList()}
            );
        }
    }
}
