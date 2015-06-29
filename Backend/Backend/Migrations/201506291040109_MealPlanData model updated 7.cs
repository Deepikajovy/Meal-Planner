namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodelupdated7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealPlanDatas", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlanDatas", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealPlans", new[] { "Meal_Id" });
            CreateTable(
                "roameals.MealPlanMealPlanDatas",
                c => new
                    {
                        MealPlan_Id = c.Int(nullable: false),
                        MealPlanData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealPlan_Id, t.MealPlanData_Id })
                .ForeignKey("roameals.MealPlans", t => t.MealPlan_Id, cascadeDelete: true)
                .ForeignKey("roameals.MealPlanDatas", t => t.MealPlanData_Id, cascadeDelete: true)
                .Index(t => t.MealPlan_Id)
                .Index(t => t.MealPlanData_Id);
            
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
            
            DropColumn("roameals.MealPlanDatas", "MealPlan_Id");
            DropColumn("roameals.MealPlans", "Meal_Id");
        }
        
        public override void Down()
        {
            AddColumn("roameals.MealPlans", "Meal_Id", c => c.Int());
            AddColumn("roameals.MealPlanDatas", "MealPlan_Id", c => c.Int());
            DropForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.MealPlanMealPlanDatas", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropForeignKey("roameals.MealPlanMealPlanDatas", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.MealMealPlans", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealMealPlans", new[] { "Meal_Id" });
            DropIndex("roameals.MealPlanMealPlanDatas", new[] { "MealPlanData_Id" });
            DropIndex("roameals.MealPlanMealPlanDatas", new[] { "MealPlan_Id" });
            DropTable("roameals.MealMealPlans");
            DropTable("roameals.MealPlanMealPlanDatas");
            CreateIndex("roameals.MealPlans", "Meal_Id");
            CreateIndex("roameals.MealPlanDatas", "MealPlan_Id");
            AddForeignKey("roameals.MealPlans", "Meal_Id", "roameals.Meals", "Id");
            AddForeignKey("roameals.MealPlanDatas", "MealPlan_Id", "roameals.MealPlans", "Id");
        }
    }
}
