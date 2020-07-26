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
    public class TeacherEditViewModel
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
        [Required(ErrorMessage = "*")]
        [Remote(action: "IsNIDUniqueEdit", controller: "Teacher", AdditionalFields = "NIDNumber,PersonId")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage ="*")]
        [Range(20,100)]
        public int? Age { get; set; }       
       
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
        public Guid? SchoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
