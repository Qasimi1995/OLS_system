using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class Party
    {
        public Party()
        {
            ContactDetails = new HashSet<ContactDetails>();
            PartyAddress = new HashSet<PartyAddress>();
            PartyDocument = new HashSet<PartyDocument>();
        }

        public Guid PartyId { get; set; }

        public virtual Person Person { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<ContactDetails> ContactDetails { get; set; }
        public virtual ICollection<PartyAddress> PartyAddress { get; set; }
        public virtual ICollection<PartyDocument> PartyDocument { get; set; }
    }
}
