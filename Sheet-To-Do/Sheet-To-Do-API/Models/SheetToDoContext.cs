﻿using System.Data.Entity;

namespace Sheet_To_Do_API.Models
{
    public class SheetToDoContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
    } 
}