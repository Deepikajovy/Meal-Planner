namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanymeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "roameals.MealMealPlans",
                c => new
                    {
                        Meal_Id = c.Int(nullable: false),
                        MealPlan_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.MealPlan_Id })
                .ForeignKey("roameals.Meals", t => t.Meal_Id, cascadeDelete: true)
                .ForeignKey("roameals.MealPlans", t => t.MealPlan_Id, cascadeDelete: true)
                .Index(t => t.Meal_Id)
                .Index(t => t.MealPlan_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealMealPlans", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealMealPlans", new[] { "Meal_Id" });
            DropTable("roameals.MealMealPlans");
        }
    }
}
