using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class StaffFinancialPlan
    {
        public Guid SchoolId { get; set; }
        public Guid? PartyRoleTypeId { get; set; }
        public decimal? Salary { get; set; }
        public string Remarks { get; set; }
        public int? Amount { get; set; }

        public virtual ZPartyRoleType PartyRoleType { get; set; }
        public virtual School School { get; set; }
    }
}
