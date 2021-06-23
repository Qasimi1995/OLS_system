using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please fill this part")]
      
        [RegularExpression(@"([A-Za-z])\w+$", ErrorMessage = "Please use below format for user name Abc_2010, Xyz_123, xyz")]
      
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Please fill this part")]
      
       [StringLength(10, MinimumLength = 6, ErrorMessage = "Password length must be at least 6 characaters and maximun 10")]
      
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
