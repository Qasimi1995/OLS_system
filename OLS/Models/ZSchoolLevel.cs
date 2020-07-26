using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolLevel
    {
        public ZSchoolLevel()
        {
            School = new HashSet<School>();
            ZSchoolLevelSubLevel = new HashSet<ZSchoolLevelSubLevel>();
        }

        public Guid SchoolLevelId { get; set; }
        public string SchoolLevelName { get; set; }
        public string SchoolLevelNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<School> School { get; set; }
        public virtual ICollection<ZSchoolLevelSubLevel> ZSchoolLevelSubLevel { get; set; }
    }
}
