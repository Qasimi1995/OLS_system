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
    public class TeacherViewModel
    {
        public Guid PersonId { get; set; }
        [Required(ErrorMessage = "*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام درست را وارد نماید/ورکړل شوی نوم سم نه دی/Please Enter Valid Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
       // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده تخلص درست را وارد نماید/ورکړل شوی تخلص سم نه دی/Please Enter Valid Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        //[RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدر درست را وارد نماید/ورکړل شوی پلارنوم سم نه دی/Please Enter Valid Father Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "*")]
       // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام پدرکلان درست را وارد نماید/ورکړل شوی د نیکه نوم سم نه دی/Please Enter Valid Grand Father Name")]
        public string GrandFatherName { get; set; }
        [Required(ErrorMessage = "*")]
        //[Remote(action: "IsNIDUnique", controller: "Founder")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(20, 100)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "*")]
        public Guid? GenderTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? EducationLevelID { get; set; }
        [Required(ErrorMessage = "*")]
        public string GraduationDate { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? FacultyTypeId { get; set; }
        [Required(ErrorMessage = "*")]
        public int? Eduservice { get; set; }
        public Guid? SchoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
