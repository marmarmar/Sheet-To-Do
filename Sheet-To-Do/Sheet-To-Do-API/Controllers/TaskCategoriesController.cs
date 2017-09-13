using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sheet_To_Do.Models;

namespace Sheet_To_Do_API.Controllers
{
    public class TaskCategoriesController : ApiController
    {
        private SheetToDoContext db = new SheetToDoContext();

        // GET: api/TaskCategories
        public IQueryable<TaskCategory> GetTaskCategories()
        {
            return db.TaskCategories.Include("Tasks");
        }

        // GET: api/TaskCategories/5
        [ResponseType(typeof(TaskCategory))]
        public IHttpActionResult GetTaskCategory(int id)
        {
            TaskCategory taskCategory = db.TaskCategories.Find(id);
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
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskCategoryExists(id))
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

        // POST: api/TaskCategories
        [ResponseType(typeof(TaskCategory))]
        public IHttpActionResult PostTaskCategory(TaskCategory taskCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TaskCategories.Add(taskCategory);
            db.SaveChanges();

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