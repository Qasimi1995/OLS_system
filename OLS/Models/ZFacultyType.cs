using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZFacultyType
    {
        public ZFacultyType()
        {
            PersonEducation = new HashSet<PersonEducation>();
        }

        public Guid FacultyTypeId { get; set; }
        public string FacultypeName { get; set; }
        public string FacultypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<PersonEducation> PersonEducation { get; set; }
    }
}
