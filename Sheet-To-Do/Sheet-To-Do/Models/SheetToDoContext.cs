using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheet_To_Do.Models
{
    public class SheetToDoContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    } 
}