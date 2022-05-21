namespace PlacementManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Placementstatus",
                c => new
                    {
                        companyId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.companyId, t.studentId })
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentId, cascadeDelete: true)
                .Index(t => t.companyId)
                .Index(t => t.studentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Placementstatus", "studentId", "dbo.Students");
            DropForeignKey("dbo.Placementstatus", "companyId", "dbo.Companies");
            DropIndex("dbo.Placementstatus", new[] { "studentId" });
            DropIndex("dbo.Placementstatus", new[] { "companyId" });
            DropTable("dbo.Placementstatus");
        }
    }
}
