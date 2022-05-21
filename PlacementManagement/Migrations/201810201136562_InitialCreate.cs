namespace PlacementManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        adminId = c.Int(nullable: false),
                        adminName = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.adminId)
                .ForeignKey("dbo.Departments", t => t.adminId)
                .Index(t => t.adminId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        deptId = c.Int(nullable: false, identity: true),
                        deptName = c.String(),
                    })
                .PrimaryKey(t => t.deptId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        studentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.String(),
                        CPI = c.Single(nullable: false),
                        MobNumber = c.String(),
                        email = c.String(),
                        otherdetails = c.String(),
                        deptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.studentId)
                .ForeignKey("dbo.Departments", t => t.deptId, cascadeDelete: true)
                .Index(t => t.deptId);
            
            CreateTable(
                "dbo.Placementstatus",
                c => new
                    {
                        companyId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => new { t.companyId, t.studentId })
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentId, cascadeDelete: true)
                .Index(t => t.companyId)
                .Index(t => t.studentId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        companyId = c.Int(nullable: false, identity: true),
                        companyName = c.String(),
                        location = c.String(),
                        minRequirements = c.Single(nullable: false),
                        avgPackage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        email = c.String(),
                        arrivalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.companyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "adminId", "dbo.Departments");
            DropForeignKey("dbo.Placementstatus", "studentId", "dbo.Students");
            DropForeignKey("dbo.Placementstatus", "companyId", "dbo.Companies");
            DropForeignKey("dbo.Students", "deptId", "dbo.Departments");
            DropIndex("dbo.Placementstatus", new[] { "studentId" });
            DropIndex("dbo.Placementstatus", new[] { "companyId" });
            DropIndex("dbo.Students", new[] { "deptId" });
            DropIndex("dbo.Admins", new[] { "adminId" });
            DropTable("dbo.Companies");
            DropTable("dbo.Placementstatus");
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
            DropTable("dbo.Admins");
        }
    }
}
