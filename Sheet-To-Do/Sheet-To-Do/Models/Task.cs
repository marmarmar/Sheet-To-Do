using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sheet_To_Do.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

        public Task()
        {
           
            Done = false;
        }

        public Task(Chronic.BaseTask task)
        {
            Title = task.Title;
            DueDate = task.DueDate;
        }

        public Task(string taskTitleUnformatted)
        {
            // taskTitleUnformatted - because it can containt date/time

            // there shoud be method do format title and return only it without date/time etc
            Title = taskTitleUnformatted;
            
            Done = false;
        }
    }
}