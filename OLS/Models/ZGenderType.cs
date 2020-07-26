using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZGenderType
    {
        public ZGenderType()
        {
            Person = new HashSet<Person>();
            StudentEnrollmentPlan = new HashSet<StudentEnrollmentPlan>();
        }

        public Guid GenderTypeId { get; set; }
        public string GenderTypeName { get; set; }
        public string GenderTypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<StudentEnrollmentPlan> StudentEnrollmentPlan { get; set; }
    }
}
