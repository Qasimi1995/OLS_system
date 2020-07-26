using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZDistrict
    {
        public ZDistrict()
        {
            PartyAddress = new HashSet<PartyAddress>();
            ZVillageNahia = new HashSet<ZVillageNahia>();
        }

        public Guid DistrictId { get; set; }
        public Guid? ProvinceId { get; set; }
        public string DistNaEng { get; set; }
        public string DistNaDar { get; set; }

        public virtual ZProvince Province { get; set; }
        public virtual ICollection<PartyAddress> PartyAddress { get; set; }
        public virtual ICollection<ZVillageNahia> ZVillageNahia { get; set; }
    }
}
