namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        DueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Description = c.String(),
                        Done = c.Boolean(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_UserId)
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
            DropForeignKey("dbo.Tasks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
        }
    }
}
