using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZSchoolGenderType
    {
        public ZSchoolGenderType()
        {
            School = new HashSet<School>();
        }

        public Guid SchoolGenderTypeId { get; set; }
        public string SchoolGenderTypeName { get; set; }
        public string SchoolGenderTypeNameDari { get; set; }
        public string OrderNumber { get; set; }

        public virtual ICollection<School> School { get; set; }
    }
}
