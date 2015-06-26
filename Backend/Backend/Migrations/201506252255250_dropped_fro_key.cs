namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropped_fro_key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("roameals.Meals", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.Meals", new[] { "MealPlan_Id" });
            DropColumn("roameals.Meals", "MealPlan_Id");
        }
        
        public override void Down()
        {
            AddColumn("roameals.Meals", "MealPlan_Id", c => c.Int());
            CreateIndex("roameals.Meals", "MealPlan_Id");
            AddForeignKey("roameals.Meals", "MealPlan_Id", "roameals.MealPlans", "Id");
        }
    }
}
