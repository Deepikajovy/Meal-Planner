namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "roameals.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Double(nullable: false),
                        Measurement = c.String(),
                        Meal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.Meals", t => t.Meal_Id)
                .Index(t => t.Meal_Id);
            
            CreateTable(
                "roameals.MealPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "roameals.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "roameals.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "roameals.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("roameals.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "roameals.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("roameals.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "roameals.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("roameals.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("roameals.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "roameals.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("roameals.AspNetUserRoles", "RoleId", "roameals.AspNetRoles");
            DropForeignKey("roameals.MealPlans", "User_Id", "roameals.AspNetUsers");
            DropForeignKey("roameals.Meals", "User_Id", "roameals.AspNetUsers");
            DropForeignKey("roameals.AspNetUserRoles", "UserId", "roameals.AspNetUsers");
            DropForeignKey("roameals.AspNetUserLogins", "UserId", "roameals.AspNetUsers");
            DropForeignKey("roameals.AspNetUserClaims", "UserId", "roameals.AspNetUsers");
            DropForeignKey("roameals.MealMealPlans", "MealPlan_Id", "roameals.MealPlans");
            DropForeignKey("roameals.MealMealPlans", "Meal_Id", "roameals.Meals");
            DropForeignKey("roameals.Ingredients", "Meal_Id", "roameals.Meals");
            DropIndex("roameals.MealMealPlans", new[] { "MealPlan_Id" });
            DropIndex("roameals.MealMealPlans", new[] { "Meal_Id" });
            DropIndex("roameals.AspNetRoles", "RoleNameIndex");
            DropIndex("roameals.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("roameals.AspNetUserRoles", new[] { "UserId" });
            DropIndex("roameals.AspNetUserLogins", new[] { "UserId" });
            DropIndex("roameals.AspNetUserClaims", new[] { "UserId" });
            DropIndex("roameals.AspNetUsers", "UserNameIndex");
            DropIndex("roameals.Meals", new[] { "User_Id" });
            DropIndex("roameals.MealPlans", new[] { "User_Id" });
            DropIndex("roameals.Ingredients", new[] { "Meal_Id" });
            DropTable("roameals.MealMealPlans");
            DropTable("roameals.AspNetRoles");
            DropTable("roameals.AspNetUserRoles");
            DropTable("roameals.AspNetUserLogins");
            DropTable("roameals.AspNetUserClaims");
            DropTable("roameals.AspNetUsers");
            DropTable("roameals.Meals");
            DropTable("roameals.MealPlans");
            DropTable("roameals.Ingredients");
        }
    }
}
