using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AccountController : ApiController
    { private string man_id;

        //Add user
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public string Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.Password;
            //man_id = 
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3

            };
            IdentityResult result = manager.Create(user, model.Password);
            manager.AddToRoles(user.Id,model.Roles); //Add coloumn in UserRole table
            man_id = user.Id;
            return man_id;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("api/GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                Id = identityClaims.FindFirst("Id").Value,
                UserName = identityClaims.FindFirst("UserName").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                PasswordHash = identityClaims.FindFirst("PasswordHash").Value,
                Password = identityClaims.FindFirst("Password").Value,
                SecurityStamp = identityClaims.FindFirst("SecurityStamp").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/ForAdminRole")]
        public string ForAdminRole()
        { 
            return "For admin role";
        }

        [HttpGet]
        [Authorize(Roles = "Hotel_Manager")]
        [Route("api/ForManagerRole")]
        public string ForManagerRole()
        {
            return "For hotel manager Role";
        }



        [HttpGet]
        [Authorize(Roles = "Hotel_Manager, Customer")]
        [Route("api/ForManagerOrCustomer")]
        public string ForManagerOrCustomer()
        {
            return "For hotel manager/customer role";
        }
    }
}
//