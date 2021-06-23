using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class StudentEnrollmentPlan
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string Year { get; set; }
        public Guid? GenderTypeId { get; set; }
        public Guid? SchoolSubLevelId { get; set; }
        public int? NumberOfStudents { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ZGenderType GenderType { get; set; }
        public virtual School School { get; set; }
        public virtual ZSchoolSubLevel SchoolSubLevel { get; set; }
    }
}
