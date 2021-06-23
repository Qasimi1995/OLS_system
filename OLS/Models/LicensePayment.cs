using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class LicensePayment
    {
        public Guid SchoolId { get; set; }
        public string RecieptNumber { get; set; }
        public Guid? PaymentId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual School School { get; set; }
    }
}
