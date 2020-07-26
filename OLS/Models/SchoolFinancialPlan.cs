using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolFinancialPlan
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? SchoolSubLevelId { get; set; }
        public int? NfreeStudents { get; set; }
        public int? NpaidStudents { get; set; }
        public decimal? FeeAmount { get; set; }
        public decimal? AdmissionFee { get; set; }
        public string Year { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ZSchoolSubLevel SchoolSubLevel { get; set; }
    }
}
