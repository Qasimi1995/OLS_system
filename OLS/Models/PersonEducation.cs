using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class PersonEducation
    {
        public Guid PersonId { get; set; }
        public Guid? EducationLevelId { get; set; }
        public string GraduationDate { get; set; }
        public Guid? FacultyTypeId { get; set; }
        public Guid PersonEducationId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public virtual ZEducationLevel EducationLevel { get; set; }
        public virtual ZFacultyType FacultyType { get; set; }
        public virtual Person Person { get; set; }
    }
}
