using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;
using OLS.ViewModels;
using OLS.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace OLS.FunctionsLibrary
{
    public class Functions:Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        public Functions(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
        }
        public  PersonDisplayViewModel GetPerson(Guid schoolid, Guid roleid)
        {
       
            var personid = _applicationContext.Person.Where(p => p.SchoolId == schoolid && p.PartyRoleTypeId == roleid)
                               .Select(p => p.PersonId).FirstOrDefault();
            var thePerson = _applicationContext.Person.Find(personid);
            var phone = _applicationContext.ContactDetails.Where(p => p.PartyId == personid && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).Select(p => p.Value).FirstOrDefault();
            var email = _applicationContext.ContactDetails.Where(p => p.PartyId == personid && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).Select(p => p.Value).FirstOrDefault();
            var EducationLevel = (from personEducation in _applicationContext.PersonEducation
                                  join zEducationLevel in _applicationContext.ZEducationLevel on personEducation.EducationLevelId equals zEducationLevel.EducationLevelId
                                  where personEducation.PersonId == personid
                                  select new
                                  {
                                      EducationLevel = zEducationLevel.EducationLevelNameDari
                                  }).FirstOrDefault();

            var PerAddress = (from partyAddress in _applicationContext.PartyAddress
                              join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                              join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                             // join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                              where partyAddress.PartyId == personid && partyAddress.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")
                              select new
                              {
                                  PerProvince = zProvince.ProvNaDar,
                                  PerDistrict = zDistrict.DistNaDar,
                                  PerNahia = partyAddress.Nahia,

                              }).FirstOrDefault();

            var PreAddress = (from partyAddress in _applicationContext.PartyAddress
                              join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                              join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                             // join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                              where partyAddress.PartyId == personid && partyAddress.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")
                              select new
                              {
                                  PreProvince = zProvince.ProvNaDar,
                                  PreDistrict = zDistrict.DistNaDar,
                                  PreNahia = partyAddress.Nahia

                              }).FirstOrDefault();

            var GenderType = (from person in _applicationContext.Person
                              join zGenderType in _applicationContext.ZGenderType on person.GenderTypeId equals zGenderType.GenderTypeId
                              where person.PersonId == personid
                              select new
                              {

                                  GenderType = zGenderType.GenderTypeNameDari
                              }).FirstOrDefault();
            PersonDisplayViewModel Myperson = new PersonDisplayViewModel
            {
                PersonId = thePerson.PersonId,
                Name = thePerson.Name,
                LastName = thePerson.LastName,
                FatherName = thePerson.FatherName,
                GrandFatherName = thePerson.GrandFatherName,
                PhonNumber = phone,
                Email = email,
                Nidnumber = thePerson.Nidnumber,
                Age = thePerson.Age,
                Photo = thePerson.Photo,
                GenderType = GenderType.GenderType,
                EducationLevel = EducationLevel.EducationLevel,
                PerProvince = PerAddress.PerProvince,
                PerDistrict = PerAddress.PerDistrict,
                PerNahia = PerAddress.PerNahia,
                PreProvince = PreAddress.PreProvince,
                PreDistrict = PreAddress.PreDistrict,
               PreNahia= PreAddress.PreNahia,
            };


            return Myperson;
        }
        public SchoolDisplayViewModel GetSchool(Guid schoolid)
        {

            SchoolDisplayViewModel theSchool = (from school in _applicationContext.School
                                                join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                                                join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                                                join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                                               // join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                                                join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                                                join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                                                join zLaboratoryMaterialType in _applicationContext.ZLaboratoryMaterialType on school.LaboratoryMaterialTypeId equals zLaboratoryMaterialType.LaboratoryMaterialTypeId
                                                where school.SchoolId == schoolid
                                                select new SchoolDisplayViewModel
                                                {
                                                    SchoolId = school.SchoolId,
                                                    SchoolLevel = zSchoolLevel.SchoolLevelNameDari,
                                                    SchoolName = school.SchoolName,
                                                    SchoolEnglishName = school.SchoolEnglishName,
                                                    SchoolLatitude=school.SchoolLatitude,
                                                    SchoolLongitude=school.SchoolLongitude,
                                                    SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari,
                                                    Nrooms = school.Nrooms,
                                                    DistancefromPuSchool = school.DistancefromPuSchool,
                                                    DistanceFromPrSchool = school.DistanceFromPrSchool,
                                                    HasTeachingBooks = school.HasTeachingBooks,
                                                    HasTeachingAids = school.HasTeachingAids,
                                                    NteachDeskChair = school.NteachDeskChair,
                                                    NstudentDeskChair = school.NstudentDeskChair,
                                                    HasLibrary = school.HasLibrary,
                                                    Nbooks = school.Nbooks,
                                                    Nboards = school.Nboards,
                                                    LaboratoryMaterialType = zLaboratoryMaterialType.LaboratoryMaterialTypeNameDari,
                                                    HasComputerLab = school.HasComputerLab,
                                                    Ncomputers = school.Ncomputers,
                                                    HasDrinkingWater = school.HasDrinkingWater,
                                                    NwashRooms = school.NwashRooms,
                                                    HasFirstAid = school.HasFirstAid,
                                                    HasFireDistinguisher = school.HasFireDistinguisher,
                                                    HasTransportation = school.HasTransportation,
                                                    HasSportFacilities = school.HasSportFacilities,
                                                    Remarks = school.Remarks,
                                                    Province = zProvince.ProvNaDar,
                                                    District = zDistrict.DistNaDar,
                                                    VillageNahia = partyAddress.Nahia,

                                                }).FirstOrDefault();
            var a = 1;
            return theSchool;
        }
        public SchoolFinancialResourceDisplayViewModel GetSchoolFinancialResource(Guid schoolid)
        {
            var TheschoolFinancialResource = (from schoolFinancialResource in _applicationContext.SchoolFinancialResource
                                              join zSchoolBussinessType in _applicationContext.ZSchoolBussinessType on schoolFinancialResource.SchoolBussinessTypeId equals zSchoolBussinessType.SchoolBussinessTypeId
                                              where schoolFinancialResource.SchoolId == schoolid
                                              select new SchoolFinancialResourceDisplayViewModel
                                              {
                                                  SchoolId = schoolFinancialResource.SchoolId,
                                                  SchoolBussinessType = zSchoolBussinessType.BussinessTypeNameDari,
                                                  FundingSourceName = schoolFinancialResource.FundingSourceName,

                                              }).FirstOrDefault();
            return TheschoolFinancialResource;


        }
        public IList<PersonViewModel> GetPersonList(Guid schoolid, Guid roleid)
        {
            List<PersonViewModel> thePersons = (from person in _applicationContext.Person
                                                join personEducation in _applicationContext.PersonEducation
                                                on person.PersonId equals personEducation.PersonId
                                                join zEducation in _applicationContext.ZEducationLevel on personEducation.EducationLevelId equals zEducation.EducationLevelId
                                                join zGender in _applicationContext.ZGenderType on person.GenderTypeId equals zGender.GenderTypeId
                                                join zfaculty in _applicationContext.ZFacultyType on personEducation.FacultyTypeId equals zfaculty.FacultyTypeId
                                                where person.PartyRoleTypeId == roleid
                                                && person.SchoolId == schoolid
                                                select new PersonViewModel
                                                {
                                                    PersonId = person.PersonId,
                                                    Name = person.Name,
                                                    LastName = person.LastName,
                                                    FatherName = person.FatherName,
                                                    GrandFatherName = person.GrandFatherName,
                                                    Nidnumber = person.Nidnumber,
                                                    Age = person.Age,
                                                    GenderType = zGender.GenderTypeNameDari,
                                                    Eduservice = person.Eduservice,
                                                    EducationLevel = zEducation.EducationLevelNameDari,
                                                    FacultyType = zfaculty.FacultypeName,
                                                    GraduationDate = personEducation.GraduationDate,
                                                }).ToList();

            return thePersons;

        }
        public IList<PersonViewModel> GetPersonList(Guid schoolid)
        {
            List<PersonViewModel> thePersons = (from person in _applicationContext.Person
                                                join personEducation in _applicationContext.PersonEducation
                                                on person.PersonId equals personEducation.PersonId
                                                join zEducation in _applicationContext.ZEducationLevel on personEducation.EducationLevelId equals zEducation.EducationLevelId
                                                join zGender in _applicationContext.ZGenderType on person.GenderTypeId equals zGender.GenderTypeId
                                                join zfaculty in _applicationContext.ZFacultyType on personEducation.FacultyTypeId equals zfaculty.FacultyTypeId
                                                join zPartyRoleType in _applicationContext.ZPartyRoleType on person.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                                                where person.SchoolId == schoolid
                                                orderby zPartyRoleType.OrderNumber
                                                select new PersonViewModel
                                                {
                                                    PersonId = person.PersonId,
                                                    Name = person.Name,
                                                    LastName = person.LastName,
                                                    FatherName = person.FatherName,
                                                    GrandFatherName = person.GrandFatherName,
                                                    Nidnumber = person.Nidnumber,
                                                    Age = person.Age,
                                                    GenderType = zGender.GenderTypeNameDari,
                                                    Eduservice = person.Eduservice,
                                                    EducationLevel = zEducation.EducationLevelNameDari,
                                                    FacultyType = zfaculty.FacultypeName,
                                                    GraduationDate = personEducation.GraduationDate,
                                                    PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari,
                                                }).ToList();

            return thePersons;

        }
        public IList<EnrollmentPlanDisplayViewModel> GetEnrollmentPlan(Guid schoolid, string year)
        {

            string Planyear = null;
            if (year == "Current")
            {
                Planyear = _applicationContext.StudentEnrollmentPlan.Where(p => p.SchoolId == schoolid).Select(p => p.Year).Min();
            }
            else if (year == "Next")
            {

                Planyear = _applicationContext.StudentEnrollmentPlan.Where(p => p.SchoolId == schoolid).Select(p => p.Year).Max();

            }

            List<EnrollmentPlanDisplayViewModel> enrollmentPlan = (from studentEnrollmentPlan in _applicationContext.StudentEnrollmentPlan
                                                                   join schoolSubLevel in _applicationContext.ZSchoolSubLevel on studentEnrollmentPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                                                                   where studentEnrollmentPlan.SchoolId == schoolid && studentEnrollmentPlan.Year == Planyear
                                                                   orderby schoolSubLevel.OrderNumber, studentEnrollmentPlan.GenderTypeId
                                                                   select new EnrollmentPlanDisplayViewModel
                                                                   {
                                                                       Id = studentEnrollmentPlan.Id,
                                                                       NumberOfStudents = studentEnrollmentPlan.NumberOfStudents,
                                                                       SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelName,
                                                                       GenderTypeId = studentEnrollmentPlan.GenderTypeId


                                                                   }).ToList();



            return enrollmentPlan;

        }
        public IList<SchoolFinancialPlanViewModel> GetSchoolFinancialPlan(Guid schoolid)
        {
            var SF = _applicationContext.SchoolFinancialPlan.Where(x => x.SchoolId == schoolid).ToList();

            var schoolBussinesType = _applicationContext.SchoolFinancialResource.Include(a => a.SchoolBussinessType)
               .Where(a => a.SchoolId == schoolid).Select(a => a.SchoolBussinessType).FirstOrDefault();

            if (schoolBussinesType.BussinessTypeName == "For Profit")
            {
                List<SchoolFinancialPlanViewModel> TheSchoolFinancialPlan = (from schoolFinancialPlan in _applicationContext.SchoolFinancialPlan
                                                                             join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schoolFinancialPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                                                                             where schoolFinancialPlan.SchoolId == schoolid
                                                                             orderby schoolSubLevel.OrderNumber
                                                                             select new SchoolFinancialPlanViewModel
                                                                             {
                                                                                 Id = schoolFinancialPlan.Id,
                                                                                 SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelName,
                                                                                 FeeAmount = decimal.Parse((string.Format("{0:0.0}", schoolFinancialPlan.FeeAmount))),
                                                                                 NfreeStudents = schoolFinancialPlan.NfreeStudents,
                                                                                 NpaidStudents = schoolFinancialPlan.NpaidStudents,
                                                                                 Year = schoolFinancialPlan.Year,
                                                                                 AdmissionFee = decimal.Parse((string.Format("{0:0.0}", schoolFinancialPlan.AdmissionFee))),
                                                                             }).ToList();
                return TheSchoolFinancialPlan;
            }
            else
            {
                List<SchoolFinancialPlanViewModel> TheSchoolFinancialPlan = (from schoolFinancialPlan in _applicationContext.SchoolFinancialPlan
                                                                             join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schoolFinancialPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                                                                             where schoolFinancialPlan.SchoolId == schoolid
                                                                             orderby schoolSubLevel.OrderNumber
                                                                             select new SchoolFinancialPlanViewModel
                                                                             {
                                                                                 Id = schoolFinancialPlan.Id,
                                                                                 SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelName,
                                                                       
                                                                                 FeeAmount =  schoolFinancialPlan.FeeAmount,
                                                                                 NfreeStudents = schoolFinancialPlan.NfreeStudents,
                                                                                 NpaidStudents = schoolFinancialPlan.NpaidStudents,
                                                                                 Year = schoolFinancialPlan.Year,
                                                                                 AdmissionFee = schoolFinancialPlan.AdmissionFee,
                                                                             }).ToList();
                return TheSchoolFinancialPlan;
            }
        }
        public IList<SchoolStaffExpensesViewModel> GetSchoolStaffExpenses(Guid schoolid)
        {

            List<SchoolStaffExpensesViewModel> TheSchoolStaffExpenses = (from zEducationalRole in _applicationContext.ZEducationalRole
                                                                         join zPartyRoleType in _applicationContext.ZPartyRoleType on zEducationalRole.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                                                                         join schoolStaffExpenses in _applicationContext.SchoolStaffExpenses on zPartyRoleType.PartyRoleTypeId equals schoolStaffExpenses.PartyRoleTypeId
                                                                         where schoolStaffExpenses.SchoolId == schoolid
                                                                         orderby zPartyRoleType.OrderNumber
                                                                         select new SchoolStaffExpensesViewModel
                                                                         {
                                                                             Id = schoolStaffExpenses.Id,
                                                                             PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari + "/" + zPartyRoleType.PartyRoleTypeName,
                                                                             PartyRoleTypeId = zPartyRoleType.PartyRoleTypeId,
                                                                             Salary = decimal.Parse((string.Format("{0:0.0}", schoolStaffExpenses.Salary))),
                                                                             Amount = schoolStaffExpenses.Amount,
                                                                         }).ToList();
            return TheSchoolStaffExpenses;

        }
        public IList<SchoolOtherExpensesViewModel> GetSchoolOtherExpenses(Guid schoolid)
        {
            List<SchoolOtherExpensesViewModel> TheSchoolOtherExpenses = (from zOtherExpenseType in _applicationContext.ZOtherExpenseType
                                                                         join schoolOtherExpenses in _applicationContext.SchoolOtherExpenses on zOtherExpenseType.OtherExpenseTypeId equals schoolOtherExpenses.OtherExpenseTypeId
                                                                         where schoolOtherExpenses.SchoolId == schoolid
                                                                         orderby zOtherExpenseType.OrderNumber
                                                                         select new SchoolOtherExpensesViewModel
                                                                         {
                                                                             OtherExpenseId = schoolOtherExpenses.OtherExpenseId,
                                                                             OtherExpenseTypeName = zOtherExpenseType.OtherExpenseTypeNameDari + "/" + zOtherExpenseType.OtherExpenseTypeName,
                                                                             ExpensePerMonth = decimal.Parse((string.Format("{0:0.0}", schoolOtherExpenses.ExpensePerMonth))),

                                                                         }).ToList();
            return TheSchoolOtherExpenses;

        }
        public IList<SubProcessStatusViewModel> GetZProcessStatuses(int? OrderNumber, Guid schoolid)
        {
            List<SubProcessStatusViewModel> zProcessStatuses = (from zProcessStatus in _applicationContext.ZProcessStatus
                                                                join subProcessStatus in _applicationContext.SubProcessStatus on zProcessStatus.ProcessStatusId equals subProcessStatus.ProcessStatusId
                                                                join processProgress in _applicationContext.ProcessProgress on subProcessStatus.SubProcessId equals processProgress.SubProcessId
                                                                join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                                                               where subProcess.OrderNumber==OrderNumber && processProgress.SchoolId==schoolid
                                                                select new SubProcessStatusViewModel
                                                                {
                                                                    ProcessProgressId = processProgress.ProcessProgressId,
                                                                    ProcessStatusId = zProcessStatus.ProcessStatusId,
                                                                    SubProcessId = subProcessStatus.SubProcessId,
                                                                    StatusNameDari = zProcessStatus.StatusNameDari,
                                                                    StatusName = zProcessStatus.StatusName,
                                                                    OrderNumber = subProcess.OrderNumber,
                                                                }).Distinct().ToList();
            return zProcessStatuses;

        }
        public IList<DocTypeViewModelEdit> GetDocurmentList(Guid schoolid,Guid DocCategoryId)
        {
            List<DocTypeViewModelEdit> DocumentList = (from zDocType in _applicationContext.ZDocType
                                join partyDocument in _applicationContext.PartyDocument on zDocType.DocTypeId equals partyDocument.DocTypeId
                                where zDocType.DocCategoryId == DocCategoryId && partyDocument.PartyId == schoolid
                                orderby zDocType.OrderNumber
                                select new DocTypeViewModelEdit
                                {
                                    DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                    DocTypeNameEng = zDocType.DocTypeName,
                                    DocTypeId = zDocType.DocTypeId,
                                    DocPath = partyDocument.DocPath,
                                    SchoolId = partyDocument.PartyId,
                                    DocTypeNameDari = zDocType.DocTypeNameDari,
                                }).ToList();
            return DocumentList;

        }
        public IList<DashboardStatuses> GetDashBoardStatuses(List<int?> OrderNumber,string RoleId, Guid? ProvinceId) {

            string CaseValues = "";

            for (int i=0;i<OrderNumber.Count;i++) {
                CaseValues += OrderNumber[i]-1;
                if (i !=OrderNumber.Count-1) {

                    CaseValues += ",";
                }
            
            }
            string ProcessJoins = "";

            for (int i = 0; i < OrderNumber.Count; i++)
            {
                ProcessJoins += @$"Left join (
                                     Select schoolid  from ProcessProgress inner join SubProcess on ProcessProgress.SubProcessID = SubProcess.SubProcessID
                                     where SubProcess.OrderNumber = {OrderNumber[i]} and ProcessProgress.ProcessStatusID is not null
                                     ) Stage{OrderNumber[i]} on Stage{OrderNumber[i]}.schoolid = processprogress.schoolid 
                                  ";

                ProcessJoins = ProcessJoins.Replace("\r\n", string.Empty);
            }

            string CountCase = "";

            for (int i = 0; i < OrderNumber.Count; i++)
            {
                CountCase += @$" when ProcessProgress.SchoolID=Stage{OrderNumber[i]}.SchoolID and OrderNumber ={OrderNumber[i] - 1} then null 
                                            when  OrderNumber ={OrderNumber[i] - 1} then ProcessProgress.SchoolID ";

                CountCase = CountCase.Replace("\r\n", string.Empty);
            }

            string CountCasePED = "";

            for (int i = 0; i < OrderNumber.Count; i++)
            {
                CountCasePED += @$" when ProcessProgress.SchoolID=Stage{OrderNumber[i]}.SchoolID and OrderNumber ={OrderNumber[i] - 1} 
                                  and PartyAddress.ProvinceID='{ProvinceId}'  then null 
                                            when  OrderNumber ={OrderNumber[i] - 1} and PartyAddress.ProvinceID='{ProvinceId}' then ProcessProgress.SchoolID ";

                CountCasePED = CountCasePED.Replace("\r\n", string.Empty);
            }
            string WhereConditions = "";

            for (int i = 0; i < OrderNumber.Count; i++)
            {
                WhereConditions += @$" (SubProcess.OrderNumber={OrderNumber[i] - 1} and SubProcessStatus.CompletionFlag=1) or ";

                WhereConditions = WhereConditions.Replace("\r\n", string.Empty);
            }

            StringBuilder query = null;

            if (RoleId == "DB70DAE4-9422-4A2F-A14B-7552702F026B")
            {
                query = new StringBuilder(@$"select SubProcess.SubProcessID
    , zProcessStatus.ProcessStatusID
    ,case when SubProcess.OrderNumber in ({CaseValues}) then 'New' else zProcessStatus.StatusNameDash end as 'StatusNameDash'
    ,case when SubProcess.OrderNumber in ({CaseValues}) then 'جدید' else zProcessStatus.StatusNameDashDari end as 'StatusNameDashDari'

    ,SubProcess.OrderNumber
    ,SubProcessStatus.CompletionFlag,
    COUNT(distinct case {CountCasePED}
           when  SubProcess.RoleID='{RoleId}' and ProcessProgress.SubProcessID=SubProcessStatus.SubProcessID and PartyAddress.ProvinceID='{ProvinceId}' then ProcessProgress.SchoolID 
   end


    ) as 'Count'
     from zProcessStatus
     left join ProcessProgress on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
     join SubProcessStatus on zProcessStatus.ProcessStatusID=SubProcessStatus.ProcessStatusID
     left join PartyAddress on ProcessProgress.SchoolID=PartyAddress.PartyID
    join SubProcess on SubProcessStatus.SubProcessID=SubProcess.SubProcessID 
     {ProcessJoins}
    where {WhereConditions} (SubProcess.RoleID='{RoleId}' ) group by 
    SubProcess.SubProcessID,
    zProcessStatus.ProcessStatusID
    , zProcessStatus.StatusNameDash
    , zProcessStatus.StatusNameDashDari
    ,SubProcess.OrderNumber
    ,SubProcessStatus.CompletionFlag

    order by SubProcess.OrderNumber
   ");

            }
            else {

                query = new StringBuilder(@$"select SubProcess.SubProcessID
    , zProcessStatus.ProcessStatusID
    ,case when SubProcess.OrderNumber in ({CaseValues}) then 'New' else zProcessStatus.StatusNameDash end as 'StatusNameDash'
    ,case when SubProcess.OrderNumber in ({CaseValues}) then 'جدید/نوی' else zProcessStatus.StatusNameDashDari end as 'StatusNameDashDari'

    ,SubProcess.OrderNumber
    ,SubProcessStatus.CompletionFlag,
    COUNT(distinct case {CountCase}
           when  SubProcess.RoleID='{RoleId}' and ProcessProgress.SubProcessID=SubProcessStatus.SubProcessID then ProcessProgress.SchoolID 
   end


    ) as 'Count'
     from zProcessStatus
     left join ProcessProgress on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
     join SubProcessStatus on zProcessStatus.ProcessStatusID=SubProcessStatus.ProcessStatusID
    join SubProcess on SubProcessStatus.SubProcessID=SubProcess.SubProcessID 
     {ProcessJoins}
    where {WhereConditions} (SubProcess.RoleID='{RoleId}' ) group by 
    SubProcess.SubProcessID,
    zProcessStatus.ProcessStatusID
    , zProcessStatus.StatusNameDash
    , zProcessStatus.StatusNameDashDari
    ,SubProcess.OrderNumber
    ,SubProcessStatus.CompletionFlag

    order by SubProcess.OrderNumber
   ");

            }
            

            List<DashboardStatuses> dashboardStatuses = _applicationContext.Status.FromSqlRaw(query.ToString()).ToList();

            return dashboardStatuses;

        }
    }
}
