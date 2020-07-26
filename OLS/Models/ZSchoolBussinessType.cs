using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolBussinessType
    {
        public ZSchoolBussinessType()
        {
            SchoolFinancialResource = new HashSet<SchoolFinancialResource>();
        }

        public Guid SchoolBussinessTypeId { get; set; }
        public string BussinessTypeName { get; set; }
        public string BussinessTypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<SchoolFinancialResource> SchoolFinancialResource { get; set; }
    }
}
