using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZVillageNahia
    {
        public ZVillageNahia()
        {
            PartyAddress = new HashSet<PartyAddress>();
        }

        public int VillageNahiaId { get; set; }
        public int? DistrictId { get; set; }
        public string VillageNameEng { get; set; }
        public string MistiProvCode { get; set; }
        public string MistiDistCode { get; set; }
        public string VilUid { get; set; }
        public string Center { get; set; }
        public double? CntrCode { get; set; }
        public string AfgUid { get; set; }
        public double? Population { get; set; }
        public string Language { get; set; }
        public double? LangCode { get; set; }
        public double? Elevation { get; set; }
        public double? LatY { get; set; }
        public double? LonX { get; set; }

        public virtual ZDistrict District { get; set; }
        public virtual ICollection<PartyAddress> PartyAddress { get; set; }
    }
}
