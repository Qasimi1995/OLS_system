using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class School
    {
        public School()
        {
            Person = new HashSet<Person>();
            ProcessProgress = new HashSet<ProcessProgress>();
            SchoolCheckList = new HashSet<SchoolCheckList>();
            SchoolFinancialPlan = new HashSet<SchoolFinancialPlan>();
            SchoolLicense = new HashSet<SchoolLicense>();
            SchoolOtherExpenses = new HashSet<SchoolOtherExpenses>();
            SchoolStaffExpenses = new HashSet<SchoolStaffExpenses>();
            StudentEnrollmentPlan = new HashSet<StudentEnrollmentPlan>();
        }

        public Guid SchoolId { get; set; }
        public Guid? SchoolLevelId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolEnglishName { get; set; }
        public double? SchoolLatitude { get; set; }
        public double? SchoolLongitude { get; set; }

        public byte? IsAcceptingCommitment { get; set; }
        public Guid? SchoolGenderTypeId { get; set; }
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
        public Guid? LaboratoryMaterialTypeId { get; set; }
        public byte? HasComputerLab { get; set; }
        public int? Ncomputers { get; set; }
        public byte? HasDrinkingWater { get; set; }
        public int? NwashRooms { get; set; }
        public byte? HasFirstAid { get; set; }
        public byte? HasFireDistinguisher { get; set; }
        public byte? HasTransportation { get; set; }
        public byte? HasSportFacilities { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public virtual ZLaboratoryMaterialType LaboratoryMaterialType { get; set; }
        public virtual ZSchoolGenderType SchoolGenderType { get; set; }
        public virtual ZSchoolLevel SchoolLevel { get; set; }
        public virtual Party SchoolNavigation { get; set; }
        public virtual LicensePayment LicensePayment { get; set; }
        public virtual SchoolFinancialResource SchoolFinancialResource { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<ProcessProgress> ProcessProgress { get; set; }
        public virtual ICollection<SchoolCheckList> SchoolCheckList { get; set; }
        public virtual ICollection<SchoolFinancialPlan> SchoolFinancialPlan { get; set; }
        public virtual ICollection<SchoolLicense> SchoolLicense { get; set; }
        public virtual ICollection<SchoolOtherExpenses> SchoolOtherExpenses { get; set; }
        public virtual ICollection<SchoolStaffExpenses> SchoolStaffExpenses { get; set; }
        public virtual ICollection<StudentEnrollmentPlan> StudentEnrollmentPlan { get; set; }
    }
}
