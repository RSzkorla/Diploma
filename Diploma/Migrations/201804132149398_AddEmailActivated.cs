namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailActivated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmailActivated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EmailActivated");
        }
    }
}
