using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolLevelSubLevel
    {
        public Guid Id { get; set; }
        public Guid? SchoolLevelId { get; set; }
        public Guid? SchoolSubLevelId { get; set; }

        public virtual ZSchoolLevel SchoolLevel { get; set; }
        public virtual ZSchoolSubLevel SchoolSubLevel { get; set; }
    }
}
