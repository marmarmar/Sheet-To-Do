namespace Sheet_To_Do.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Sheet_To_Do.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Sheet_To_Do.Models.SheetToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Sheet_To_Do.Models.SheetToDoContext";
        }

        protected override void Seed(Sheet_To_Do.Models.SheetToDoContext context)
        {
            
            User user1 = new User { Login = "Stefan", Password = "kkk" };
            User user2 = new User { Login = "Maria", Password = "kkk" };
            User user3 = new User { Login = "Monika", Password = "kkk" };
            User user4 = new User { Login = "Diana", Password = "kkk" };

            context.Users.AddOrUpdate(
                user3,
                user4
            );

            var taskCategory1 = new TaskCategory { Name = "Birthday", User = user1 };
            var taskCategory2 = new TaskCategory { Name = "D-Day", User = user2 };
            context.TaskCategories.AddOrUpdate(
                taskCategory1,
                taskCategory2
            );

            context.Tasks.AddOrUpdate(
                new Task { Title = "Go to codecool", User = user1, TaskCategory = taskCategory1 },
                new Task { Title = "Buy some milk", User = user1 },
                new Task { Title = "Go to movie", User = user2, TaskCategory = taskCategory2 },
                new Task { Title = "Create over app", User = user2 }
            );
        }
    }
}
