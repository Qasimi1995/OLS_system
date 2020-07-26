using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZProvince
    {
        public ZProvince()
        {
            PartyAddress = new HashSet<PartyAddress>();
            ZDistrict = new HashSet<ZDistrict>();
        }

        public Guid ProvinceId { get; set; }
        public string ProvNaEng { get; set; }
        public string ProvNaDar { get; set; }

        public virtual ICollection<PartyAddress> PartyAddress { get; set; }
        public virtual ICollection<ZDistrict> ZDistrict { get; set; }
    }
}
