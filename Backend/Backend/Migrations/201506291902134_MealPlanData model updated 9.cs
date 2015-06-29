namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodelupdated9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealMealPlanDatas", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.MealMealPlanDatas", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropIndex("roameals.MealMealPlanDatas", new[] { "Meal_Id" });
            DropIndex("roameals.MealMealPlanDatas", new[] { "MealPlanData_Id" });
            AddColumn("roameals.Meals", "MealPlanData_Id", c => c.Int());
            CreateIndex("roameals.Meals", "MealPlanData_Id");
            AddForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas", "Id");
            DropTable("roameals.MealMealPlanDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "roameals.MealMealPlanDatas",
                c => new
                    {
                        Meal_Id = c.Int(nullable: false),
                        MealPlanData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_Id, t.MealPlanData_Id });
            
            DropForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropIndex("roameals.Meals", new[] { "MealPlanData_Id" });
            DropColumn("roameals.Meals", "MealPlanData_Id");
            CreateIndex("roameals.MealMealPlanDatas", "MealPlanData_Id");
            CreateIndex("roameals.MealMealPlanDatas", "Meal_Id");
            AddForeignKey("roameals.MealMealPlanDatas", "MealPlanData_Id", "roameals.MealPlanDatas", "Id", cascadeDelete: true);
            AddForeignKey("roameals.MealMealPlanDatas", "Meal_Id", "roameals.Meals", "Id", cascadeDelete: true);
        }
    }
}
