namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDueDateToTaskDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "DueDate", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "DueDate", c => c.DateTime(nullable: false));
        }
    }
}
