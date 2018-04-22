namespace Diploma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActivationId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ActivationId");
        }
    }
}
