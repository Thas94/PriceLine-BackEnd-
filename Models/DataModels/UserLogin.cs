using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.DataModels
{
    public class UserLogin
    {

        public UserDto User { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
        public ICollection<System_RoleDto> System_RoleDtos { get; set; }

    }
}