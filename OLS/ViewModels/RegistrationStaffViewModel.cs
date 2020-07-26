using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OLS.ViewModels
{
    public class RegistrationStaffViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string RoleId { get; set; }
        public byte IsActive { get; set; }
        public Guid? ProvinceId { get; set; }
    }
}
