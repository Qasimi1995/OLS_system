using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OLS.Models;

namespace OLS.ViewModels
{
    public class SchoolDisplayViewModel
    {
     
        public Guid     SchoolId                { get; set; }
        public string   SchoolLevel           { get; set; }
        public string   SchoolName              { get; set; }
        public string   SchoolGenderType       { get; set; }
        public int?     Nrooms                   { get; set; }
        public int?     DistancefromPuSchool     { get; set; }
        public int?     DistanceFromPrSchool    { get; set; }
        public byte?    HasTeachingBooks          { get; set; }
        public byte?    HasTeachingAids         { get; set; }
        public int?     NteachDeskChair         { get; set; }
        public int?     NstudentDeskChair       { get; set; }
        public byte?    HasLibrary              { get; set; }
        public int?     Nbooks                   { get; set; }
        public int?     Nboards                     { get; set; }
        public string    LaboratoryMaterialType { get; set; }
        public byte?    HasComputerLab              { get; set; }
        public int?     Ncomputers                  { get; set; }
        public decimal? AdmissionFee                { get; set; }
        public byte?    HasDrinkingWater            { get; set; }
        public int?     NwashRooms                  { get; set; }
        public byte?    HasFirstAid                  { get; set; }
        public byte?    HasFireDistinguisher         { get; set; }
        public byte?    HasTransportation               { get; set; }
        public byte?    HasSportFacilities           { get; set; }
        public string   Remarks                         { get; set; }
        public string   Province                      { get; set; }
        public string   District                      { get; set; }
        public string   VillageNahia                  { get; set; }
        public int?     OrderNumber { get; set; }
        public string StausName { get; set; }
        public DateTime? StatusDate { get; set; }

    }
}
