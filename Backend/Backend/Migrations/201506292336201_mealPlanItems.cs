namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mealPlanItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.MealMealPlans", new[] { "Meal_Id" });
            DropIndex("roameals.MealMealPlans", new[] { "MealPlan_Id" });
            RenameColumn(table: "roameals.Meals", name: "User_Id", newName: "CreatedBy_Id");
            RenameIndex(table: "roameals.Meals", name: "IX_User_Id", newName: "IX_CreatedBy_Id");
            CreateTable(
                "roameals.MealPlanItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                        Date = c.DateTime(nullable: false),
                        Meal_Id = c.Int(),
                        MealPlan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.Meals", t => t.Meal_Id)
                .ForeignKey("roameals.MealPlans", t => t.MealPlan_Id)
                .Index(t => t.Meal_Id)
                .Index(t => t.MealPlan_Id);
            
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
            
            DropForeignKey("roameals.MealPlanItems", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealPlanItems", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealPlanItems", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealPlanItems", new[] { "Meal_Id" });
            DropTable("roameals.MealPlanItems");
            RenameIndex(table: "roameals.Meals", name: "IX_CreatedBy_Id", newName: "IX_User_Id");
            RenameColumn(table: "roameals.Meals", name: "CreatedBy_Id", newName: "User_Id");
            CreateIndex("roameals.MealMealPlans", "MealPlan_Id");
            CreateIndex("roameals.MealMealPlans", "Meal_Id");
            AddForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans", "Id", cascadeDelete: true);
            AddForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals", "Id", cascadeDelete: true);
        }
    }
}
