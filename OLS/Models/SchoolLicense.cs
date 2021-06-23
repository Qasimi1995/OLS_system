using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolLicense
    {
        public Guid SchoolLicenseId { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public Guid? SchoolId { get; set; }
        public DateTime? ExpirateDate { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual School School { get; set; }
    }
}
