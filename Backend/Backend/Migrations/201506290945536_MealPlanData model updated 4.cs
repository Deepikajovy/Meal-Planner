namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodelupdated4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropIndex("roameals.Meals", new[] { "MealPlanData_Id" });
            CreateTable(
                "roameals.MealMealPlanDatas",
                c => new
                    {
                        Meal_Id = c.Int(nullable: false),
                        MealPlanData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.MealPlanData_Id })
                .ForeignKey("roameals.Meals", t => t.Meal_Id, cascadeDelete: true)
                .ForeignKey("roameals.MealPlanDatas", t => t.MealPlanData_Id, cascadeDelete: true)
                .Index(t => t.Meal_Id)
                .Index(t => t.MealPlanData_Id);
            
            DropColumn("roameals.Meals", "MealPlanData_Id");
        }
        
        public override void Down()
        {
            AddColumn("roameals.Meals", "MealPlanData_Id", c => c.Int());
            DropForeignKey("roameals.MealMealPlanDatas", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropForeignKey("roameals.MealMealPlanDatas", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealMealPlanDatas", new[] { "MealPlanData_Id" });
            DropIndex("roameals.MealMealPlanDatas", new[] { "Meal_Id" });
            DropTable("roameals.MealMealPlanDatas");
            CreateIndex("roameals.Meals", "MealPlanData_Id");
            AddForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas", "Id");
        }
    }
}
