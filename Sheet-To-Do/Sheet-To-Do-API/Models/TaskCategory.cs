using System.Collections.Generic;

namespace Sheet_To_Do_API.Models
{
    public class TaskCategory
    {
        public int TaskCategoryId { get; set; }

        public string Name { get; set; }
        public User User { get; set; }
        public List<Task> Tasks { get; set; }

    }
}