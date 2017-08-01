using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sheet_To_Do.Models;

namespace Sheet_To_Do.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Task> tasks = null;
            using (var db = new SheetToDoContext())
            {
                tasks = db.Tasks.ToList();
            }
            return View(tasks);
        }
    }
}