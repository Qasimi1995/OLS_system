using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OLS.ViewModels
{
    public class RegistrationViewModel
    {
        
        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [RegularExpression(@"([A-Z])\w+$", ErrorMessage = "لطف نموده از فارمت ذیل برای نام کابر استفاده نماید/مهرباني وکړئ د کارونکي نوم لپاره لاندې مثال وکاروئ Abc_2010, Xyz_123, Xyz_abc_123")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$$", ErrorMessage = "لطف نموده از فارمت ذیل برای ایمل استفاده نماید/مهرباني وکړئ د ایمل لپاره لاندې مثال وکاروئ Abc@gmail.com, abc@yahoo.com, etc..")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [MaxLength(10, ErrorMessage = "حد اکثر طول رمز باید 10 عدد باشد/د پټنوم اعظمي حد باید شپږ وي ")]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public byte IsActive { get; set; }
        public string RoleId { get; set; }
    }
}
