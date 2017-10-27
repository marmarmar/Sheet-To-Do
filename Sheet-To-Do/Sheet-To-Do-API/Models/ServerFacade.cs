using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Marvin.JsonPatch;

namespace Sheet_To_Do_API.Models
{
    public class ServerFacade
    {
        private readonly SheetToDoContext _db = new SheetToDoContext();

        public IQueryable<Task> GetTasksFor(int userId)
        {
            var tasks = _db.Tasks
                .Where(x => x.User.UserId == userId)
                .Where(x => !x.IsArchived);
            return tasks;
        }

        public IQueryable<Task> GetTasksByTaskCategoryFor(int taskCategoryId)
        {
            var tasks = _db.Tasks
                .Where(x => x.TaskCategory.TaskCategoryId == taskCategoryId)
                .Where(x => !x.IsArchived)
                .AsNoTracking();
            return tasks;
        }

        public IQueryable<Task> GetTasksByDateRangeFor(int userId, DateTime startDate, DateTime endDate)
        {
            var tasks = _db.Tasks
                .Where(x => x.User.UserId == userId)
                .Where(x => x.DueDate >= startDate && x.DueDate <= endDate)
                .Where(x => !x.IsArchived)
                .AsNoTracking();
            return tasks;
        }

        public Task FindTaskBy(int id)
        {
            var task = _db.Tasks.Find(id);
            return task;
        }

        public Task PatchTaskFor(int id, JsonPatchDocument<Task> taskPatchDocument)
        {
            var task = _db.Tasks.Find(id);
            return task;
        }

        public void Apply(JsonPatchDocument<Task> taskPatchDocument, Task task)
        {
            taskPatchDocument.ApplyTo(task);
            _db.SaveChanges();
        }

        public void DeleteTask(Task task)
        {
            _db.Tasks.Remove(task);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool TaskExistsFor(int id)
        {
            return _db.Tasks.Count(e => e.TaskId == id) > 0;
        }

        public bool Save(Task task, int id)
        {
            _db.Entry(task).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return false;
            }
            return true;
        }

        public void PostTask(int userId, Task task)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserId == userId);          
            task.User = user;
            task.ParseTimeFromTaskTitle();//todo to będzie zbędne gdy będzie setter
            _db.Tasks.Add(task);
            _db.SaveChanges();
            task.User = null;//todo co to jest i po co takie dziwactwo?
        }
    }
}