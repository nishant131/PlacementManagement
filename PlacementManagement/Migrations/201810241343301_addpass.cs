namespace PlacementManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Password");
            DropColumn("dbo.Admins", "Password");
        }
    }
}
