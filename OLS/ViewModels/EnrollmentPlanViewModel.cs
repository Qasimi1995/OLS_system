using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class EnrollmentPlanViewModel
    {
        public Guid? GenderTypeId                       { get; set; }
        public Guid?  SchoolSubLevelId               { get; set; }
        public String SchoolSubLevelName         { get; set; }
        [Required(ErrorMessage = "*")]
        public int?   NumberOfStudentsMale       { get; set; }
        [Required(ErrorMessage = "*")]
        public int?   NumberOfStudentsFemale { get; set; }

    }
}
