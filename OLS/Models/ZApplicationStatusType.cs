using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZApplicationStatusType
    {
        public ZApplicationStatusType()
        {
            ApplicationStatus = new HashSet<ApplicationStatus>();
        }

        public Guid ApplicationStatusTypeId { get; set; }
        public string StatusTypeName { get; set; }

        public virtual ICollection<ApplicationStatus> ApplicationStatus { get; set; }
    }
}
