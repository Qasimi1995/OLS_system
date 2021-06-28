using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading;

namespace OLS.ViewModels
{
    public class FounderViewModel
    {
        CultureInfo uiCultureInfo = Thread.CurrentThread.CurrentUICulture;
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;

      
        public FounderViewModel()
        {
            
        }


        public Guid PersonId { get; set; }
         [Required(ErrorMessage ="*")]
        // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام درست را وارد نماید/ورکړل شوی نوم سم نه دی/Please Enter Valid Name")]
        public string Name { get; set; }
         [Required(ErrorMessage ="*")]
        // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده تخلص درست را وارد نماید/ورکړل شوی تخلص سم نه دی/Please Enter Valid Last Name")]
        public string LastName { get; set; }
         [Required(ErrorMessage ="*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدر درست را وارد نماید/ورکړل شوی پلار سم نه دی/Please Enter Valid Father Name")]
        public string FatherName { get; set; }
         [Required(ErrorMessage ="*")]
        // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدرکلان درست را وارد نماید/ورکړل شوی د نیکه نوم سم نه دی/Please Enter Valid Grand Father Name")]
        public string GrandFatherName { get; set; }

        [Required(ErrorMessage ="*")]
        [Remote(action: "IsPhoneUnique", controller: "Founder")]
        public string PhonNumber { get; set; }

         [Required(ErrorMessage ="*")]
      //  [EmailAddress(ErrorMessage = "فارمت ایمیل درست نیست / ورکړل شوی برېښنالیک سم نه دی / Email Format is not valid")]
        [EmailAddress(ErrorMessage = "*")]
        [Remote(action: "IsEmailUnique",controller:"Founder")]
        public string Email { get; set; }
        [Required(ErrorMessage ="*")]
        [Remote(action: "IsNIDUnique", controller: "Founder")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage ="*")]
        [Range(20,100)]
        public int? Age { get; set; }

        [Required(ErrorMessage ="*")]
        [DataType(DataType.Upload)]
      //  [MaxFileSize(500 * 1024, ErrorMessage = "Max 500 Kb file is allowed /  حد اکثر اندازه عکس باید 500 kb باشد.")]
        [MaxFileSize(500 * 1024, ErrorMessage = "*")]
       // [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = " only .jpg, png and jpeg format is allowed")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = " * ")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage ="*")]
        public Guid? GenderTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? EducationLevelID { get; set; }

         [Required(ErrorMessage ="*")]
        public int? PerProvinceId { get; set; }
         [Required(ErrorMessage ="*")]
        public int? PerDistrictId { get; set; }
         [Required(ErrorMessage ="*")]
        public string PerNahia { get; set; }
         [Required(ErrorMessage ="*")]
        public int? PreProvinceId { get; set; }
         [Required(ErrorMessage ="*")]
        public int? PreDistrictId { get; set; }
         [Required(ErrorMessage ="*")]
        public string PreNahia { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
