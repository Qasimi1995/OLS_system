using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class Person
    {
        public Person()
        {
            PersonEducation = new HashSet<PersonEducation>();
        }

        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Nidnumber { get; set; }
        public int? Age { get; set; }
        public int? Eduservice { get; set; }
        public string Photo { get; set; }
        public Guid? GenderTypeId { get; set; }
        public Guid? PartyRoleTypeId { get; set; }
        public Guid? SchoolId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ZGenderType GenderType { get; set; }
        public virtual ZPartyRoleType PartyRoleType { get; set; }
        public virtual Party PersonNavigation { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<PersonEducation> PersonEducation { get; set; }
    }
}
