using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZPayment
    {
        public Guid PaymentId { get; set; }
        public decimal? AmountInNumbers { get; set; }
        public string AmountInLetters { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? StatusTypeId { get; set; }

        public virtual ZStatusType StatusType { get; set; }
    }
}
