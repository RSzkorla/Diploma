namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Promo_Id = c.Guid(),
                        User_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Promoes", t => t.Promo_Id)
                .ForeignKey("dbo.Users", t => t.User_Email)
                .Index(t => t.Promo_Id)
                .Index(t => t.User_Email);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        Tag = c.Int(nullable: false),
                        Project_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Promoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailActivated = c.Boolean(nullable: false),
                        ActivationId = c.Guid(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "User_Email", "dbo.Users");
            DropForeignKey("dbo.Projects", "Promo_Id", "dbo.Promoes");
            DropForeignKey("dbo.ProjectTasks", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ProjectTasks", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "User_Email" });
            DropIndex("dbo.Projects", new[] { "Promo_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Promoes");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Projects");
        }
    }
}
