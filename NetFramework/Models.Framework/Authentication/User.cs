using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Framework
{
    public class UserRole
    {
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "User Role")]
        public string Role { get; set; }
    }

    public class Credential
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        //Password should be protected / encrypted
        [Required]
        [StringLength(20)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<UserRole> Roles { get; set; }

        public List<UserAction> UserActions { get; set; }
    }
}
