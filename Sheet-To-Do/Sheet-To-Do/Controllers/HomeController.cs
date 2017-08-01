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
            using (var db = new SheetToDoContext())
            {
                var dupa = new Task { Title = "obejrzec kotki" };
                db.Tasks.Add(dupa);
                db.SaveChanges();
            }
            return View();
        }
    }
}