namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodelupdated6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealPlanMeals", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealPlanMeals", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlanMeals", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealPlanMeals", new[] { "Meal_Id" });
            AddColumn("roameals.MealPlans", "Meal_Id", c => c.Int());
            CreateIndex("roameals.MealPlans", "Meal_Id");
            AddForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals", "Id");
            DropTable("roameals.MealPlanMeals");
        }
        
        public override void Down()
        {
            CreateTable(
                "roameals.MealPlanMeals",
                c => new
                    {
                        MealPlan_Id = c.Int(nullable: false),
                        Meal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealPlan_Id, t.Meal_Id });
            
            DropForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlans", new[] { "Meal_Id" });
            DropColumn("roameals.MealPlans", "Meal_Id");
            CreateIndex("roameals.MealPlanMeals", "Meal_Id");
            CreateIndex("roameals.MealPlanMeals", "MealPlan_Id");
            AddForeignKey("roameals.MealPlanMeals", "Meal_Id", "roameals.Meals", "Id", cascadeDelete: true);
            AddForeignKey("roameals.MealPlanMeals", "MealPlan_Id", "roameals.MealPlans", "Id", cascadeDelete: true);
        }
    }
}
