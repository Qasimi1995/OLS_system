using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using OLS.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using OLS.ViewModels;

namespace OLS.ViewModels
{
    public class PersonViewModel
    {
        public Guid PersonId { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "*")]
        public string GrandFatherName { get; set; }
        [Remote(action: "IsNIDUnique", controller: "Founder")]
        public string Nidnumber { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(20, 100)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "*")]
        public string GenderType { get; set; }
        [Required(ErrorMessage = "*")]
        public string EducationLevel { get; set; }
        [Required(ErrorMessage = "*")]
        public string GraduationDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string FacultyType { get; set; }
        [Required(ErrorMessage = "*")]
        public int? Eduservice { get; set; }
        public Guid? SchoolId { get; set; }
        public string PartyRoleType { get; set; }
        TeacherViewModel Tmodel { get; set; }
        TeacherEditViewModel TEditModel { get; set; }

    }
}
