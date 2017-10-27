using System.Linq;
using System.Web.Http;
using Sheet_To_Do_API.Models;

namespace Sheet_To_Do_API.Controllers
{
    public class UserController : ApiController
    {
       /// <param name="password">TODO wygląda na to, że hasło jest przesyłane GET-em niezaszyfrowane, gdzie każdy może je podglądnąć</param>
       public IHttpActionResult GetUser([FromUri] string login, [FromUri] string password)
        {
            using (var db = new SheetToDoContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
                if (user != null)
                    return Ok(new UserView(user.UserId, user.Login));
                return NotFound();
            }
        }
    }
}
