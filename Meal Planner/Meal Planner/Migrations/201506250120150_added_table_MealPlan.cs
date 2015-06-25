namespace Meal_Planner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_table_MealPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Meal_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MealModels", t => t.Meal_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Meal_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealPlans", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MealPlans", "Meal_Id", "dbo.MealModels");
            DropIndex("dbo.MealPlans", new[] { "User_Id" });
            DropIndex("dbo.MealPlans", new[] { "Meal_Id" });
            DropTable("dbo.MealPlans");
        }
    }
}
