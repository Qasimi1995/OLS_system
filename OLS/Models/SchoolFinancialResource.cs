using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolFinancialResource
    {
        public Guid SchoolId { get; set; }
        public Guid? SchoolBussinessTypeId { get; set; }
        public string FundingSourceName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual School School { get; set; }
        public virtual ZSchoolBussinessType SchoolBussinessType { get; set; }
    }
}
