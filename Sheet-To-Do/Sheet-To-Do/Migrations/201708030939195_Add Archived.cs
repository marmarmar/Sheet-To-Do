namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchived : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Archived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Archived");
        }
    }
}
