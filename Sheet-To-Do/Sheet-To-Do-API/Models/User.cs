using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Chronic;

namespace Sheet_To_Do.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User name is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}