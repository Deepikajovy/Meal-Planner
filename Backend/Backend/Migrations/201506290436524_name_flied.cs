namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name_flied : DbMigration
    {
        public override void Up()
        {
            AddColumn("roameals.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("roameals.AspNetUsers", "Name");
        }
    }
}
