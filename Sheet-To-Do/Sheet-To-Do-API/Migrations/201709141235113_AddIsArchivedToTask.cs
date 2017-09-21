using System.Data.Entity.Migrations;

namespace Sheet_To_Do_API.Migrations
{
    public partial class AddIsArchivedToTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "IsArchived");
        }
    }
}
