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
            context.Tasks.AddOrUpdate(
                new Task { Title = "Go to codecool" , TaskCategory = taskCategory1},
                new Task { Title = "Buy some milk" },
                new Task { Title = "Go to movie" , TaskCategory = taskCategory2},
                new Task { Title = "Create over app" }
            );

        }
    }
}
