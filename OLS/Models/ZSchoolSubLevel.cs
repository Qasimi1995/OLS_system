using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolSubLevel
    {
        public ZSchoolSubLevel()
        {
            SchoolFinancialPlan = new HashSet<SchoolFinancialPlan>();
            StudentEnrollmentPlan = new HashSet<StudentEnrollmentPlan>();
            ZSchoolLevelSubLevel = new HashSet<ZSchoolLevelSubLevel>();
        }

        public Guid SchoolSubLevelId { get; set; }
        public string SubLevelName { get; set; }
        public string SubLevelNameDari { get; set; }
        public string SubLevelNamePashto { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<SchoolFinancialPlan> SchoolFinancialPlan { get; set; }
        public virtual ICollection<StudentEnrollmentPlan> StudentEnrollmentPlan { get; set; }
        public virtual ICollection<ZSchoolLevelSubLevel> ZSchoolLevelSubLevel { get; set; }
    }
}
