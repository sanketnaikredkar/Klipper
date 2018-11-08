using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Core
{
    public class Credential
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "User ID")]
        public string UserName { get; set; }

        //Password should be protected / encrypted
        [Required]
        [StringLength(20)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<UserRole> Roles { get; set; }
    }
}
