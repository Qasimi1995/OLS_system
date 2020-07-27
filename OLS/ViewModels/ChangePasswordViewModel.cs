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
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد فعلی / اوسنی رمز / Current password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد جدید / نوی رمز / New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "حد اکثر طول رمز باید 10 و حد اقل 6 عدد باشد/د پټنوم اعظمي حد باید 10 او لږ حد 6 وي")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید پسورد / پاسورډ تایید کړه / Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "مقایسه پسورد یکسان نیست / پټنوم ورته نه دی ")]
        public string ConfirmPassword { get; set; }
    }
}
