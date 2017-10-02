namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskCategories",
                c => new
                    {
                        TaskCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TaskCategoryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        DueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Description = c.String(),
                        Done = c.Boolean(nullable: false),
                        TaskCategory_TaskCategoryId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.TaskCategories", t => t.TaskCategory_TaskCategoryId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.TaskCategory_TaskCategoryId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskCategories", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "TaskCategory_TaskCategoryId", "dbo.TaskCategories");
            DropIndex("dbo.Tasks", new[] { "User_UserId" });
            DropIndex("dbo.Tasks", new[] { "TaskCategory_TaskCategoryId" });
            DropIndex("dbo.TaskCategories", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskCategories");
        }
    }
}
