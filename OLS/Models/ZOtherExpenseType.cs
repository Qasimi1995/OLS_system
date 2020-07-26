using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZOtherExpenseType
    {
        public ZOtherExpenseType()
        {
            SchoolOtherExpenses = new HashSet<SchoolOtherExpenses>();
        }

        public Guid OtherExpenseTypeId { get; set; }
        public string OtherExpenseTypeName { get; set; }
        public string OtherExpenseTypeNameDari { get; set; }
        public string OtherExpenseTypeNamePashto { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<SchoolOtherExpenses> SchoolOtherExpenses { get; set; }
    }
}
