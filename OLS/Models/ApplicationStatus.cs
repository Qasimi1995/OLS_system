using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ApplicationStatus
    {
        public Guid ApplicationStatusId { get; set; }
        public Guid? ApplicationStatusTypeId { get; set; }
        public DateTime? Date { get; set; }
        public Guid? SchoolId { get; set; }

        public virtual ZApplicationStatusType ApplicationStatusType { get; set; }
        public virtual School School { get; set; }
    }
}
