namespace mvc_project_auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        Salary = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Fk_DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.Fk_DepartmentId, cascadeDelete: true)
                .Index(t => t.Fk_DepartmentId);
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "E_mail", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Fk_DepartmentId", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "Fk_DepartmentId" });
            DropColumn("dbo.AspNetUsers", "E_mail");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
        }
    }
}
