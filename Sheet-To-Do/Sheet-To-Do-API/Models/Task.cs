using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chronic;

namespace Sheet_To_Do_API.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public User User { get; set; }
        public TaskCategory TaskCategory { get; set; }
        public bool IsArchived { get; set; }

        public void ParseTimeFromTaskTitle()
        {
            Parser parser = new Parser(new Options { FirstDayOfWeek = DayOfWeek.Monday });
            var baseTask = parser.ParseToTask(Title);
            //information in baseTask field, im find something?
            Title = baseTask.Title;
            DueDate = baseTask.DueDate;
        }
    }

}