namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasDefaultSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Ingredients", newSchema: "roameals");
            MoveTable(name: "dbo.MealPlans", newSchema: "roameals");
            MoveTable(name: "dbo.Meals", newSchema: "roameals");
            MoveTable(name: "dbo.AspNetUsers", newSchema: "roameals");
            MoveTable(name: "dbo.AspNetUserClaims", newSchema: "roameals");
            MoveTable(name: "dbo.AspNetUserLogins", newSchema: "roameals");
            MoveTable(name: "dbo.AspNetUserRoles", newSchema: "roameals");
            MoveTable(name: "dbo.AspNetRoles", newSchema: "roameals");
        }
        
        public override void Down()
        {
            MoveTable(name: "roameals.AspNetRoles", newSchema: "dbo");
            MoveTable(name: "roameals.AspNetUserRoles", newSchema: "dbo");
            MoveTable(name: "roameals.AspNetUserLogins", newSchema: "dbo");
            MoveTable(name: "roameals.AspNetUserClaims", newSchema: "dbo");
            MoveTable(name: "roameals.AspNetUsers", newSchema: "dbo");
            MoveTable(name: "roameals.Meals", newSchema: "dbo");
            MoveTable(name: "roameals.MealPlans", newSchema: "dbo");
            MoveTable(name: "roameals.Ingredients", newSchema: "dbo");
        }
    }
}
