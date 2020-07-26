using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZAddressType
    {
        public ZAddressType()
        {
            PartyAddress = new HashSet<PartyAddress>();
        }

        public Guid AddressTypeId { get; set; }
        public string AddressTypeName { get; set; }
        public string AddressTypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<PartyAddress> PartyAddress { get; set; }
    }
}
