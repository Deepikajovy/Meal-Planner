namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Likes_Dislikes_AddedTo_MealObject : DbMigration
    {
        public override void Up()
        {
            AddColumn("roameals.Meals", "Likes", c => c.Int(nullable: false));
            AddColumn("roameals.Meals", "Dislikes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("roameals.Meals", "Dislikes");
            DropColumn("roameals.Meals", "Likes");
        }
    }
}
