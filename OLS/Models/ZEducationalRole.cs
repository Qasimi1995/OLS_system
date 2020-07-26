using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZEducationalRole
    {
        public Guid PartyRoleTypeId { get; set; }

        public virtual ZPartyRoleType PartyRoleType { get; set; }
    }
}
