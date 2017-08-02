using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult ChangeState(int? id)
        {
            using (var db = new SheetToDoContext())
            {
                Task task = db.Tasks.Find(id);
                if (task != null)
                {
                    task.Done = task.Done ? false : true;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index"); // TODO: make error page inform about error

            }
        }


        // GET:
        public ActionResult Edit(int? id)
        {
            using (var db = new SheetToDoContext())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Task task = db.Tasks.Find(id);
                if (task == null)
                {
                    return HttpNotFound();
                }
                return View(task);
            }
        }

        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Done")] Task task)
        {
            if (ModelState.IsValid)
                using (var db = new SheetToDoContext())
                {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }



    }
}