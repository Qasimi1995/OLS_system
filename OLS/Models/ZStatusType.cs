using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZStatusType
    {
        public ZStatusType()
        {
            ZPayment = new HashSet<ZPayment>();
        }

        public Guid StatusTypeId { get; set; }
        public string StatusTypeName { get; set; }
        public string StatusTypeNameDari { get; set; }
        public string OrderNumber { get; set; }

        public virtual ICollection<ZPayment> ZPayment { get; set; }
    }
}
