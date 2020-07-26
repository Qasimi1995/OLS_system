using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.Models
{
    public class SchoolListAll
    {
        public Guid SchoolId { get; set; }
        public string SchoolLevelNameDari { get; set; }
        public string SchoolName { get; set; }
        public string SchoolGenderTypeNameDari { get; set; }
        public int? Nrooms { get; set; }
        public int? DistancefromPuSchool { get; set; }
        public int? DistanceFromPrSchool { get; set; }
        public byte? HasTeachingBooks { get; set; }
        public byte? HasTeachingAids { get; set; }
        public int? NteachDeskChair { get; set; }
        public int? NstudentDeskChair { get; set; }
        public byte? HasLibrary { get; set; }
        public int? Nbooks { get; set; }
        public int? Nboards { get; set; }

        public byte? HasComputerLab { get; set; }
        public int? Ncomputers { get; set; }
        public byte? HasDrinkingWater { get; set; }
        public int? NwashRooms { get; set; }
        public byte? HasFirstAid { get; set; }
        public byte? HasFireDistinguisher { get; set; }
        public byte? HasTransportation { get; set; }
        public byte? HasSportFacilities { get; set; }
        public string Remarks { get; set; }
        public string PROV_NA_DAR { get; set; }
        public string DIST_NA_DAR { get; set; }
        public string VILLAGE_NAME_ENG { get; set; }
        public string StatusNameDariPast { get; set; }
        public DateTime? StatusDate { get; set; }
    }
}
