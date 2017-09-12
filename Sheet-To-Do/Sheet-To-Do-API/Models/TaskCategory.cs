using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sheet_To_Do.Models
{
    public class TaskCategory
    {
        public int TaskCategoryId { get; set; }

        public string Name { get; set; }
        public virtual List<Task> Tasks { get; set; }

    }
}