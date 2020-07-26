using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword",ErrorMessage ="Passwords does not match")]
        public string ConfirmPassword { get; set; }
    }
}
