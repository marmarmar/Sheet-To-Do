namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeInTaskToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
