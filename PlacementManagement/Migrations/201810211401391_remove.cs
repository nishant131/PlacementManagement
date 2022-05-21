namespace PlacementManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Placementstatus", "companyId", "dbo.Companies");
            DropForeignKey("dbo.Placementstatus", "studentId", "dbo.Students");
            DropIndex("dbo.Placementstatus", new[] { "companyId" });
            DropIndex("dbo.Placementstatus", new[] { "studentId" });
            DropTable("dbo.Placementstatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Placementstatus",
                c => new
                    {
                        companyId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.companyId, t.studentId });
            
            CreateIndex("dbo.Placementstatus", "studentId");
            CreateIndex("dbo.Placementstatus", "companyId");
            AddForeignKey("dbo.Placementstatus", "studentId", "dbo.Students", "studentId", cascadeDelete: true);
            AddForeignKey("dbo.Placementstatus", "companyId", "dbo.Companies", "companyId", cascadeDelete: true);
        }
    }
}
