namespace PlacementManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "adminName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Departments", "deptName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "MobNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Placementstatus", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "companyName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Companies", "location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "location", c => c.String());
            AlterColumn("dbo.Companies", "companyName", c => c.String());
            AlterColumn("dbo.Placementstatus", "Status", c => c.String());
            AlterColumn("dbo.Students", "MobNumber", c => c.String());
            AlterColumn("dbo.Students", "Gender", c => c.String());
            AlterColumn("dbo.Students", "Name", c => c.String());
            AlterColumn("dbo.Departments", "deptName", c => c.String());
            AlterColumn("dbo.Admins", "adminName", c => c.String());
            DropColumn("dbo.Students", "Age");
        }
    }
}
