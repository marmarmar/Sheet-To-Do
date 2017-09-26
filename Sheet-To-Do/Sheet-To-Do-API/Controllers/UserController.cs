using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sheet_To_Do_API.Models;

namespace Sheet_To_Do_API.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult GetUser([FromUri] string login, [FromUri] string password)
        {
            using (var db = new SheetToDoContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
                if (user != null)
                {
                    return Ok((UserView)user);
                }
                return NotFound();
            }
        }
    }
}
