using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class RessetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطف نموده پسورد جدید را وارد نماید / مهرباني وکړئ د نوی پاسورد داخل کړئ ") ]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطف نموده این بخش را پر نماید / مهرباني وکړئ دا برخه ډکه کړئ ")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [Compare("Password", ErrorMessage = "مقایسه پسورد یکسان نیست / پاسورد ورته نه دی ")]
        
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }

    }
}
