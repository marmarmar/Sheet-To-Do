namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNamePropertyInTaskCategoryToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskCategories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskCategories", "Name", c => c.Int(nullable: false));
        }
    }
}
