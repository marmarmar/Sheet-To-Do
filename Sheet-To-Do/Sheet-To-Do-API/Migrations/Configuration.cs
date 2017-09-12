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
                context.Tasks.AddOrUpdate(
                  new Task { Title = "Go to codecool" },
                  new Task { Title = "Buy some milk" },
                  new Task { Title = "Go to movie" },
                  new Task { Title = "Create over app" }
                );
                context.Users.AddOrUpdate(
                   new User { Login = "Stefan", Password = "kkk"},
                   new User { Login = "Monika", Password = "kkk"},
                   new User { Login = "Diana", Password = "kkk"},
                   new User { Login = "Maria", Password = "kkk"}
                );

        }
    }
}
