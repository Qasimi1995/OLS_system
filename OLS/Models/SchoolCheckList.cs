using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SchoolCheckList
    {
        public Guid SchoolCheckListId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? SchoolIndicatorId { get; set; }
        public Guid? SchoolQualityLevelId { get; set; }

        public virtual School School { get; set; }
        public virtual ZSchoolIndicator SchoolIndicator { get; set; }
        public virtual ZSchoolQualityLevel SchoolQualityLevel { get; set; }
    }
}
