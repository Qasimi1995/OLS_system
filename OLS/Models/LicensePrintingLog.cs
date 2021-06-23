using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class LicensePrintingLog
    {
        public int Id { get; set; }
        public string? SchoolName { get; set; }
        public Guid? SchoolId { get; set; }

        public string? SchoolLicenseNo { get; set; }

        public Guid? PrintedByUserId { get; set; }
        public string? PrintedByUserName { get; set; }
       
        public DateTime?  PrintedDate { get; set; }
    }
}
