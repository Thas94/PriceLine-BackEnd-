using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("SYSTEM_ROLE")]
    public class System_Role
    {

        [Key]
        public int Id { get; set; }
        public int Name { get; set; } 

        public Role Role { get; set; }

    }
}