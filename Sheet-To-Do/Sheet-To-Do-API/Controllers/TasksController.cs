using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly ServerFacade _serverFacade = new ServerFacade();

        // GET: api/Tasks
        public IQueryable<Task> GetTasks([FromUri] int userId)
        {
            return _serverFacade.GetTasksFor(userId);
        }

        // GET: api/Tasks?userid=
        // by category
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetTasksByTaskCategory([FromUri] int taskCategoryId)
        {
            return Ok(_serverFacade.GetTasksByTaskCategoryFor(taskCategoryId));
        }

        // GET: api/Tasks?userId= &startDate= &endDate=
        // by date
        [ResponseType(typeof(List<Task>))]
        public IHttpActionResult GetTasksByDateRange([FromUri] int userId, [FromUri] DateTime startDate, [FromUri] DateTime endDate)
        {
            return Ok(_serverFacade.GetTasksByDateRangeFor(userId, startDate, endDate));
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            var task = _serverFacade.FindTaskBy(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        // PATCH: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult PatchTask(int id, [FromBody]JsonPatchDocument<Task> taskPatchDocument)
        {
            var task = _serverFacade.FindTaskBy(id);
            if (task == null)
                return NotFound();
            _serverFacade.Apply(taskPatchDocument, task);
            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTask(int id, Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != task.TaskId)
                return BadRequest();
            if (!_serverFacade.Save(task, id))
                return InternalServerError();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask([FromUri] int userId, Task task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
               _serverFacade.PostTask(userId, task);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            var task = _serverFacade.FindTaskBy(id);
            if (task == null)
                return NotFound();
            _serverFacade.DeleteTask(task);
            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _serverFacade.Dispose();
            base.Dispose(disposing);
        }

        public bool TaskExists(int id)
        {
            return _serverFacade.TaskExistsFor(id);
        }
    }
}