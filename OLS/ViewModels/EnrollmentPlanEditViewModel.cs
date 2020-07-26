using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class EnrollmentPlanEditViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "*")]
        public int? NumberOfStudents { get; set; }
        public String SchoolSubLevelName { get; set; }
        public Guid? GenderTypeId { get; set; }
    }
}
