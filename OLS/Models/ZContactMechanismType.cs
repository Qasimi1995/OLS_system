using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZContactMechanismType
    {
        public ZContactMechanismType()
        {
            ContactDetails = new HashSet<ContactDetails>();
        }

        public Guid ContactMechanismTypeId { get; set; }
        public string ContactMechanismTypeName { get; set; }
        public string ContactMechanismTypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<ContactDetails> ContactDetails { get; set; }
    }
}
