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
            var taskCategory1 = new TaskCategory { Name = "Birthday" };
            var taskCategory2 = new TaskCategory { Name = "D-Day" };
            context.TaskCategories.AddOrUpdate(
                taskCategory1,
                taskCategory2
            );

            User user1 = new User { Login = "Stefan", Password = "kkk" };
            User user2 = new User { Login = "Maria", Password = "kkk" };

            context.Tasks.AddOrUpdate(
                new Task { Title = "Go to codecool", User = user1, TaskCategory = taskCategory1 },
                new Task { Title = "Buy some milk", User = user1 },
                new Task { Title = "Go to movie", User = user2, taskCategory1 = taskCategory2 },
                new Task { Title = "Create over app", User = user2 }
            );
        }
    }
}
