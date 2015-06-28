namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class array_of_meals : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.MealMealPlans", new[] { "Meal_Id" });
            DropIndex("roameals.MealMealPlans", new[] { "MealPlan_Id" });
            AddColumn("roameals.MealPlans", "Meal_Id", c => c.Int());
            CreateIndex("roameals.MealPlans", "Meal_Id");
            AddForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals", "Id");
            DropTable("roameals.MealMealPlans");
        }
        
        public override void Down()
        {
            CreateTable(
                "roameals.MealMealPlans",
                c => new
                    {
                        Meal_Id = c.Int(nullable: false),
                        MealPlan_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.MealPlan_Id });
            
            DropForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlans", new[] { "Meal_Id" });
            DropColumn("roameals.MealPlans", "Meal_Id");
            CreateIndex("roameals.MealMealPlans", "MealPlan_Id");
            CreateIndex("roameals.MealMealPlans", "Meal_Id");
            AddForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans", "Id", cascadeDelete: true);
            AddForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals", "Id", cascadeDelete: true);
        }
    }
}
