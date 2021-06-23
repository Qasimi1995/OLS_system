using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ContactDetails
    {
        public Guid ContactDetailId { get; set; }
        public Guid PartyId { get; set; }
        public Guid? ContactMechanismTypeId { get; set; }
        public string Value { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string?     CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ZContactMechanismType ContactMechanismType { get; set; }
        public virtual Party Party { get; set; }
    }
}
