using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;

namespace OLS.ViewModels
{
    public class SchoolViewModel
    {
     
        public Guid     SchoolId                { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid?    SchoolLevelId           { get; set; }
        [Required(ErrorMessage = "*")]
       // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام مکتب درست را وارد نماید/ورکړل شوی دښوونځي نوم سم نه دی/Please Enter Valid School Name")]
        public string   SchoolName              { get; set; }
        [Required(ErrorMessage = "*")]
       // [RegularExpression("^(?![ .]+$)[a-zA-Z .]*$", ErrorMessage = "لطف نموده نام انگلیسی مکتب درست را وارد نماید/ورکړل شوی دانگلیسی ښوونځي نوم سم نه دی/Please Enter Valid School Name")]
        public string SchoolEnglishName { get; set; }
        [Required(ErrorMessage = "*")]
        public double? SchoolLatitude { get; set; }
        [Required(ErrorMessage = "*")]
        public double? SchoolLongitude { get; set; }


        [Required(ErrorMessage = "*")]
        public Guid?    SchoolGenderTypeId       { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNrooms", controller: "School", AdditionalFields = "SchoolLevelId,Nrooms")]
        public int?     Nrooms                   { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateDistancefromPuSchool", controller: "School", AdditionalFields = "DistancefromPuSchool")]
        public int?     DistancefromPuSchool     { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateDistanceFromPrSchool", controller: "School", AdditionalFields = "DistanceFromPrSchool")]
        public int?     DistanceFromPrSchool    { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasTeachingBooks          { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasTeachingAids         { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNteachDeskChair", controller: "School", AdditionalFields = "SchoolLevelId,NteachDeskChair")]
        public int?     NteachDeskChair         { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNstudentDeskChair", controller: "School", AdditionalFields = "SchoolLevelId,NstudentDeskChair")]
        public int?     NstudentDeskChair       { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasLibrary              { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNbooks", controller: "School", AdditionalFields = "SchoolLevelId,Nbooks")]
        public int?     Nbooks                   { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNboards", controller: "School", AdditionalFields = "SchoolLevelId,Nboards")]
        public int?     Nboards                     { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid?    LaboratoryMaterialTypeId { get; set; }
        public byte?    HasComputerLab              { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote(action: "ValidateNcomputers", controller: "School", AdditionalFields = "SchoolLevelId,Ncomputers")]
        public int?     Ncomputers                  { get; set; }
  
        public decimal? AdmissionFee                { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasDrinkingWater            { get; set; }
        [Required(ErrorMessage = "*")]
        public int?     NwashRooms                  { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasFirstAid                  { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasFireDistinguisher         { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasTransportation               { get; set; }
        [Required(ErrorMessage = "*")]
        public byte?    HasSportFacilities           { get; set; }
        public string   Remarks                         { get; set; }
        [Required(ErrorMessage = "*")]
        public int?    ProvinceId                      { get; set; }
        [Required(ErrorMessage = "*")]
        public int?    DistrictId                      { get; set; }
        [Required(ErrorMessage = "*")]
        public string Nahia { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
