using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Sheet_To_Do_API.Models;

namespace Sheet_To_Do_API.Controllers
{
    [EnableCors(origins: "http://localhost:4200,https://tokarskadiana.github.io", headers: "*", methods: "*")]
    public class TaskCategoriesController : ApiController
    {
        private SheetToDoContext db = new SheetToDoContext();

        // GET: api/TaskCategories
        /// <summary>
        /// todo czy nie lepiej użyć DTO?
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<TaskCategory> GetTaskCategories([FromUri] int userId)
        {
            return db.TaskCategories.Where(x => x.User.UserId == userId);
        }

        // GET: api/TaskCategories/5
        [ResponseType(typeof(TaskCategory))]
        public IHttpActionResult GetTaskCategory(int id)
        {
            var taskCategory = db.TaskCategories.Find(id);
            if (taskCategory == null)
            {
                return NotFound();
            }

            return Ok(taskCategory);
        }

        // PUT: api/TaskCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTaskCategory(int id, TaskCategory taskCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskCategory.TaskCategoryId)
            {
                return BadRequest();
            }

            db.Entry(taskCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TaskCategoryExists(id))
                {
                    return NotFound();
                }
                return InternalServerError(ex); //todo zwrócić 500
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TaskCategories
        [ResponseType(typeof(TaskCategory))]
        public IHttpActionResult PostTaskCategory(TaskCategory taskCategory, [FromUri] int userId)
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

            taskCategory.User = user;
            db.TaskCategories.Add(taskCategory);
            db.SaveChanges();

            //TODO json ignore user property in model
            taskCategory.User = null;

            return CreatedAtRoute("DefaultApi", new { id = taskCategory.TaskCategoryId }, taskCategory);
        }

        // DELETE: api/TaskCategories/5
        [ResponseType(typeof(TaskCategory))]
        public IHttpActionResult DeleteTaskCategory(int id)
        {
            TaskCategory taskCategory = db.TaskCategories.Find(id);
            if (taskCategory == null)
            {
                return NotFound();
            }

            db.TaskCategories.Remove(taskCategory);
            db.SaveChanges();

            return Ok(taskCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskCategoryExists(int id)
        {
            return db.TaskCategories.Count(e => e.TaskCategoryId == id) > 0;
        }
    }
}