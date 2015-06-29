namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodelupdated5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlans", new[] { "Meal_Id" });
            CreateTable(
                "roameals.MealPlanMeals",
                c => new
                    {
                        MealPlan_Id = c.Int(nullable: false),
                        Meal_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealPlan_Id, t.Meal_Id })
                .ForeignKey("roameals.MealPlans", t => t.MealPlan_Id, cascadeDelete: true)
                .ForeignKey("roameals.Meals", t => t.Meal_Id, cascadeDelete: true)
                .Index(t => t.MealPlan_Id)
                .Index(t => t.Meal_Id);
            
            DropColumn("roameals.MealPlans", "Meal_Id");
        }
        
        public override void Down()
        {
            AddColumn("roameals.MealPlans", "Meal_Id", c => c.Int());
            DropForeignKey("roameals.MealPlanMeals", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.MealPlanMeals", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.MealPlanMeals", new[] { "Meal_Id" });
            DropIndex("roameals.MealPlanMeals", new[] { "MealPlan_Id" });
            DropTable("roameals.MealPlanMeals");
            CreateIndex("roameals.MealPlans", "Meal_Id");
            AddForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals", "Id");
        }
    }
}
