using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolOtherExpenses
    {
        public Guid OtherExpenseId { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? OtherExpenseTypeId { get; set; }
        public decimal? ExpensePerMonth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ZOtherExpenseType OtherExpenseType { get; set; }
        public virtual School School { get; set; }
    }
}
