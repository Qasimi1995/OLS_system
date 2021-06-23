using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLS.ViewModels
{
    public class PrincipleEditViewModel
    {
        public Guid PersonId { get; set; }
         [Required(ErrorMessage ="*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام درست را وارد نماید/ورکړل شوی نوم سم نه دی/Please Enter Valid Name")]
        public string Name { get; set; }
         [Required(ErrorMessage ="*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده تخلص درست را وارد نماید/ورکړل شوی تخلص سم نه دی/Please Enter Valid Last Name")]
        public string LastName { get; set; }
         [Required(ErrorMessage ="*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدر درست را وارد نماید/ورکړل شوی پلارنوم سم نه دی/Please Enter Valid Father Name")]
        public string FatherName { get; set; }
         [Required(ErrorMessage ="*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدرکلان درست را وارد نماید/ورکړل شوی د نیکه نوم سم نه دی/Please Enter Valid Grand Father Name")]
        public string GrandFatherName { get; set; }

        [Required(ErrorMessage ="*")]
        [Remote(action: "IsPhoneUniqueEdit", controller: "Founder", AdditionalFields = "PersonId")]
        public string PhonNumber { get; set; }

        [Required(ErrorMessage ="*")]
       // [EmailAddress(ErrorMessage = "فارمت ایمیل درست نیست / ورکړل شوی برېښنالیک سم نه دی / Email Format is not valid")]
        [EmailAddress(ErrorMessage = "*")]
        [Remote(action: "IsEmailUniqueEdit", controller:"Founder", AdditionalFields = "PersonId")]
        public string Email { get; set; }
        [Required(ErrorMessage ="*")]
        [Remote(action: "IsNIDUniqueEdit", controller: "Founder", AdditionalFields = "PersonId")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage ="*")]
        [Range(20,100)]
        public int? Age { get; set; }       
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage ="*")]
        public Guid? GenderTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? EducationLevelID { get; set; }
        [Required(ErrorMessage = "*")]
       
        public string GraduationDate { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? FacultyTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public int? Eduservice { get; set; }

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
        public string ExistingPhotoPath { get; set; }
        public string Message { get; set; }
        public Guid? SchoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
