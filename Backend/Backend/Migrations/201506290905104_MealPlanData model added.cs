namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealPlanDatamodeladded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "roameals.MealPlanDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                        Date = c.DateTime(nullable: false),
                        MealPlan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.MealPlans", t => t.MealPlan_Id)
                .Index(t => t.MealPlan_Id);
            
            AddColumn("roameals.Meals", "MealPlanData_Id", c => c.Int());
            CreateIndex("roameals.Meals", "MealPlanData_Id");
            AddForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("roameals.Meals", "MealPlanData_Id", "roameals.MealPlanDatas");
            DropForeignKey("roameals.MealPlanDatas", "MealPlan_Id", "roameals.MealPlans");
            DropIndex("roameals.Meals", new[] { "MealPlanData_Id" });
            DropIndex("roameals.MealPlanDatas", new[] { "MealPlan_Id" });
            DropColumn("roameals.Meals", "MealPlanData_Id");
            DropTable("roameals.MealPlanDatas");
        }
    }
}
