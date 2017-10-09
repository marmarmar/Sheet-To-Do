using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Marvin.JsonPatch;
using Sheet_To_Do_API.Models;

namespace Sheet_To_Do_API.Controllers
{
    //[RoutePrefix("api/Tasks")]
    [EnableCors(origins: "http://localhost:4200,https://tokarskadiana.github.io", headers: "*", methods: "*")]
    public class TasksController : ApiController
    {
        private SheetToDoContext db = new SheetToDoContext();

        // GET: api/Tasks
        public IQueryable<Task> GetTasks([FromUri] int userId)
        {
            var tasks = db.Tasks
                .Where(x => x.User.UserId == userId)
                .Where(x => !x.IsArchived);
            return tasks;
        }

        // GET: api/Tasks?userid=
        // by category
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetTasksByTaskCategory([FromUri] int taskCategoryId)
        {
            var tasks = db.Tasks
                .Where(x => x.TaskCategory.TaskCategoryId == taskCategoryId)
                .Where(x => !x.IsArchived)
                .AsNoTracking();
            return Ok(tasks);
        }

        // GET: api/Tasks?userId= &startDate= &endDate=
        // by date
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetTasksByDateRange([FromUri] int userId, [FromUri] DateTime startDate, [FromUri] DateTime endDate)
        {
            var tasks = db.Tasks
                .Where(x => x.User.UserId == userId)
                .Where(x => x.DueDate >= startDate && x.DueDate <= endDate)
                .Where(x => !x.IsArchived)
                .AsNoTracking();
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PATCH: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult PatchTask(int id, [FromBody]JsonPatchDocument<Task> taskPatchDocument)
        {
            var task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            // apply the patch document 
            taskPatchDocument.ApplyTo(task);
            db.SaveChanges();
            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.TaskId)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task, [FromUri] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.SingleOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            task.User = user;
            task.ParseTimeFromTaskTitle();
            db.Tasks.Add(task);
            db.SaveChanges();

            //TODO json ignore user property in model
            task.User = null;

            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChanges();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskId == id) > 0;
        }
    }
}