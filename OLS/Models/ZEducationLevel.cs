using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZEducationLevel
    {
        public ZEducationLevel()
        {
            PersonEducation = new HashSet<PersonEducation>();
        }

        public Guid EducationLevelId { get; set; }
        public string EducationLevelName { get; set; }
        public string EducationLevelNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<PersonEducation> PersonEducation { get; set; }
    }
}
