using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using recipeDIY.Data;

namespace recipeDIY.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class UserController : Controller
    {
        private ApplicationDbContext _db;
        public UserController(ApplicationDbContext context)
        {
            this._db = context;
        }
        
        #nullable enable
        [HttpPut]
        public bool makeUserPremium(string? userId)
        {
            if (userId != null)
            {
                var currentUser = _db.Users.FirstOrDefault(u => u.Id.ToString() == userId);

                if (currentUser == null) return false;

                var getUser = _db.Users.Update(currentUser);
                getUser.Entity.IsPremium = true;
                _db.SaveChanges();
                return true;

            }
            else
            {
                var currentUser = _db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (currentUser == null) return false;

                var getUser = _db.Users.Update(currentUser);
                getUser.Entity.IsPremium = true;
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}