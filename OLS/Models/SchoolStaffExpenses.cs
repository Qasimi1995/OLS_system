using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolStaffExpenses
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? PartyRoleTypeId { get; set; }
        public decimal? Salary { get; set; }
        public int? Amount { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public virtual ZPartyRoleType PartyRoleType { get; set; }
        public virtual School School { get; set; }
    }
}
