using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class LicensePayment
    {
        public Guid SchoolId { get; set; }
        public string RecieptNumber { get; set; }
        public Guid? PaymentId { get; set; }

        public virtual School School { get; set; }
    }
}
