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
       // [RegularExpression(@"([A-Za-z])\w+$", ErrorMessage = "لطف نموده از فارمت ذیل برای نام کابر استفاده نماید/مهرباني وکړئ د کارونکي نوم لپاره لاندې مثال وکاروئ Abc_2010, Xyz_123, Xyz_abc_123")]
        public string UserName { get; set; }

       
        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [EmailAddress(ErrorMessage = "فارمت ایمیل درست نیست / ورکړل شوی برېښنالیک سم نه دی / Email Format is not valid")]
        [Remote(action: "IsEmailUnique", controller: "Account")]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$$", ErrorMessage = "لطف نموده از فارمت ذیل برای ایمل استفاده نماید/مهرباني وکړئ د ایمل لپاره لاندې مثال وکاروئ Abc@gmail.com, abc@yahoo.com, etc..")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
       //[MaxLength(ErrorMessage = "حد اکثر طول رمز باید 8 عدد باشد/د پټنوم اعظمي حد باید 8 وي ")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")  ]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [Compare("Password",ErrorMessage = "مقایسه پسورد یکسان نیست / پټنوم ورته نه دی ")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        public string LastName { get; set; }

        public byte IsActive { get; set; }
        public string RoleId { get; set; }
    }
}
