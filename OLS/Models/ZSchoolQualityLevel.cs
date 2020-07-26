using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolQualityLevel
    {
        public ZSchoolQualityLevel()
        {
            SchoolCheckList = new HashSet<SchoolCheckList>();
        }

        public Guid SchoolQualityLevelId { get; set; }
        public string LevelName { get; set; }
        public string OrderNumber { get; set; }
        public string LevelNameDari { get; set; }

        public virtual ICollection<SchoolCheckList> SchoolCheckList { get; set; }
    }
}
