﻿using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class PartyAddress
    {
        public Guid PartyAddressId { get; set; }
        public Guid PartyId { get; set; }
        public Guid? AddressTypeId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? VillageNahiaId { get; set; }
        public string Nahia { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ZAddressType AddressType { get; set; }
        public virtual ZDistrict District { get; set; }
        public virtual Party Party { get; set; }
        public virtual ZProvince Province { get; set; }
        public virtual ZVillageNahia VillageNahia { get; set; }
    }
}
