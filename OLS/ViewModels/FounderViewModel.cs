using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using Microsoft.AspNetCore.Mvc;

namespace OLS.ViewModels
{
    public class FounderViewModel
    {
        public Guid PersonId { get; set; }
         [Required(ErrorMessage ="*")]
        public string Name { get; set; }
         [Required(ErrorMessage ="*")]
        public string LastName { get; set; }
         [Required(ErrorMessage ="*")]
        public string FatherName { get; set; }
         [Required(ErrorMessage ="*")]
        public string GrandFatherName { get; set; }

        [Required(ErrorMessage ="*")]
        [Remote(action: "IsPhoneUnique", controller: "Founder")]
        public string PhonNumber { get; set; }

        [Required(ErrorMessage ="*")]
        [EmailAddress(ErrorMessage = "فارمت ایمیل درست نیست / ورکړل شوی برېښنالیک سم نه دی / Email Format is not valid")]
        [Remote(action: "IsEmailUnique",controller:"Founder")]
        public string Email { get; set; }
        [Required(ErrorMessage ="*")]
        [Remote(action: "IsNIDUnique", controller: "Founder")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage ="*")]
        [Range(20,100)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "لطفا عکس را انتخاب کنید / مهرباني وکړئ یو عکس غوره کړئ")]
        [DataType(DataType.Upload)]
        [MaxFileSize(500 * 1024, ErrorMessage = "Max 500 Kb file is allowed /  حد اکثر اندازه عکس باید 500 kb باشد.")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = " only .jpg, png and jpeg format is allowed")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage ="*")]
        public Guid? GenderTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? EducationLevelID { get; set; }

         [Required(ErrorMessage ="*")]
        public Guid? PerProvinceId { get; set; }
         [Required(ErrorMessage ="*")]
        public Guid? PerDistrictId { get; set; }
         [Required(ErrorMessage ="*")]
        public string PerNahia { get; set; }
         [Required(ErrorMessage ="*")]
        public Guid? PreProvinceId { get; set; }
         [Required(ErrorMessage ="*")]
        public Guid? PreDistrictId { get; set; }
         [Required(ErrorMessage ="*")]
        public string PreNahia { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
