using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolIndicator
    {
        public ZSchoolIndicator()
        {
            SchoolCheckList = new HashSet<SchoolCheckList>();
        }

        public Guid SchoolIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<SchoolCheckList> SchoolCheckList { get; set; }
    }
}
