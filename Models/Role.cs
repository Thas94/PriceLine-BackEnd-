using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Key]
        public int Role_Id { get; set; }
        [ForeignKey("User")]
        public int User_Id { get; set; }
        [ForeignKey("System_Role")]
        public int Id { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<System_Role> System_Roles { get; set; }

    }
}