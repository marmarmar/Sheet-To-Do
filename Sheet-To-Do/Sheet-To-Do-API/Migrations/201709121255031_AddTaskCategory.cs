namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskCategories",
                c => new
                    {
                        TaskCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskCategoryId);
            
            AddColumn("dbo.Tasks", "TaskCategory_TaskCategoryId", c => c.Int());
            CreateIndex("dbo.Tasks", "TaskCategory_TaskCategoryId");
            AddForeignKey("dbo.Tasks", "TaskCategory_TaskCategoryId", "dbo.TaskCategories", "TaskCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskCategory_TaskCategoryId", "dbo.TaskCategories");
            DropIndex("dbo.Tasks", new[] { "TaskCategory_TaskCategoryId" });
            DropColumn("dbo.Tasks", "TaskCategory_TaskCategoryId");
            DropTable("dbo.TaskCategories");
        }
    }
}
