using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZPartyRoleType
    {
        public ZPartyRoleType()
        {
            Person = new HashSet<Person>();
            SchoolStaffExpenses = new HashSet<SchoolStaffExpenses>();
        }

        public Guid PartyRoleTypeId { get; set; }
        public string PartyRoleTypeName { get; set; }
        public string PartyRoleTypeNameDari { get; set; }
        public string PartyRoleTypeNamePashto { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ZEducationalRole ZEducationalRole { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<SchoolStaffExpenses> SchoolStaffExpenses { get; set; }
    }
}
