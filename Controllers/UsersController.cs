using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private UserConn db = new UserConn();

        //  Getting all the users
        [HttpGet]
        [Route("api/GetAllUsers")]
        [AllowAnonymous]
        public IEnumerable<User> Get()
        {
            using (UserConn entities = new UserConn())
            {
                return entities.Users.ToList();
            }
        }

        [HttpGet]
        [Route("api/UserDetails")]
        [AllowAnonymous]
        public IQueryable<User> GetRoom(string id)
        {
            var details = db.Users.Where(x => x.Id == id);

            if (details == null)
            {
                return null;
            }
            return details;
        }

    }
}
