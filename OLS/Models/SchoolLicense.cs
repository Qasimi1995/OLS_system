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

        public virtual School School { get; set; }
    }
}
