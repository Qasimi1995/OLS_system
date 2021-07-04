using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using NLog.LayoutRenderers;
using OLS.Models;
using OLS.ViewModels;


namespace OLS.Controllers
{
    [Authorize(Roles = "DPERep,DPE,LicenseIssuer")]
    public class ReportsController : Controller
    {
        private ApplicationContext _applicationContext;
        IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly IHtmlLocalizer _localizer;

        public ReportsController(ApplicationContext applicationContext, IWebHostEnvironment environment, UserManager<User> userManager, IHtmlLocalizer<ReportsController> localizer)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
            _localizer = localizer;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult SchoolListWithLicense()
        {
            DateTime dt1 = DateTime.Now;
            var schoolList = (from school in _applicationContext.School
                          join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                          join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                          join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                          //join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                          join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                          join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                          join processProgress in _applicationContext.ProcessProgress on school.SchoolId equals processProgress.SchoolId
                          join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                          where processProgress.ProcessStatusId == Guid.Parse("F457B052-AD64-4E06-9780-E385946EB624") 
                       
                          select new SchoolDisplayViewModel
                          {
                              SchoolId = school.SchoolId,
                              SchoolLevel = zSchoolLevel.SchoolLevelNameDari+"/"+zSchoolLevel.SchoolLevelName,
                              SchoolLevelEng= zSchoolLevel.SchoolLevelName,
                              SchoolEnglishName = school.SchoolEnglishName,
                              SchoolName = school.SchoolName,
                              SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari+"/"+zSchoolGenderType.SchoolGenderTypeName,
                              SchoolGenderTypeEnglish=zSchoolGenderType.SchoolGenderTypeName,
                              Province = zProvince.ProvNaDar+"/"+zProvince.ProvNaEng,
                              District = zDistrict.DistNaDar+"/"+zDistrict.DistNaEng,
                              VillageNahia = school.Nahia,
                              OrderNumber = subProcess.OrderNumber,
                              StatusDate = processProgress.StatusDate,
                              No_days = Math.Abs((dt1 - processProgress.StatusDate).Value.Days)

                          }).Distinct().ToList();

            return View(schoolList);
        }

        public IActionResult FigureReport()
        {

            StringBuilder query = null;
            query = new StringBuilder(@$"select
                zProvince.PROV_NA_DAR as 'Province'
                ,COUNT(case when ProcessProgress.ProcessStatusID='F457B052-AD64-4E06-9780-E385946EB624' then ProcessProgress.SchoolID end) as 'LicensesIssued'
                ,COUNT(case when ProcessProgress.ProcessStatusID
                 in (
                'C42112A8-0C7B-4D4A-A93E-01B93F794420',
                'E532C13A-6E4D-464F-BEA2-02A0D59DE23F',
                'D279A58A-1FC1-4A01-A9A3-38EC746ABE62',
                '613E361D-1703-4727-86D7-782A9BA52DE8',
                'F98C91B5-D0AA-486B-84B2-C1EED3D66ED0',
                '0BBD5B1E-BF84-4339-A748-C25BA6852741')

                 then ProcessProgress.SchoolID end) as 'Underwork'
                 from ProcessProgress 
                inner join School on ProcessProgress.SchoolID = School.SchoolID
                join zProcessStatus on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
                join PartyAddress on School.SchoolID=PartyAddress.PartyID
                join zProvince on PartyAddress.ProvinceID = zProvince.ProvinceID
                join zDistrict on PartyAddress.DistrictID = zDistrict.DistrictID
                join zVillageNahia on PartyAddress.VillageNahiaID = zVillageNahia.VillageNahiaID
                join zSchoolGenderType on zSchoolGenderType.SchoolGenderTypeID = School.SchoolGenderTypeID
                join zSchoolLevel on zSchoolLevel.SchoolLevelID = School.SchoolLevelID
                join( select ProcessProgress.SchoolID, max(StatusDate) as maxdate from ProcessProgress join School on ProcessProgress.SchoolID= School.SchoolID
                group by ProcessProgress.SchoolID
                ) temp
                on temp.maxdate = ProcessProgress.StatusDate

                group by zProvince.PROV_NA_DAR

                ");
            List<FigureReport> List = _applicationContext.FigureReport.FromSqlRaw(query.ToString()).ToList();

            return View(List);
        }



        public IActionResult SchoolListAll()
        {

            StringBuilder query = null;
            query = new StringBuilder(@$"select school.*,zProcessStatus.StatusNameDariPast, zProcessStatus.StatusNamePast, ProcessProgress.StatusDate,
            zProvince.PROV_NA_DAR,
            zProvince.PROV_NA_ENG,
            zDistrict.DIST_NA_DAR,
            zDistrict.DIST_NA_ENG,
            ,zSchoolGenderType.SchoolGenderTypeNameDari
            ,zSchoolLevel.SchoolLevelNameDari
            ,zSchoolLevel.SchoolLevelName
             from ProcessProgress 
            inner join School on ProcessProgress.SchoolID = School.SchoolID
            join zProcessStatus on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
            join PartyAddress on School.SchoolID=PartyAddress.PartyID
            join zProvince on PartyAddress.ProvinceID = zProvince.ProvinceID
            join zDistrict on PartyAddress.DistrictID = zDistrict.DistrictID
            join zSchoolGenderType on zSchoolGenderType.SchoolGenderTypeID = School.SchoolGenderTypeID
            join zSchoolLevel on zSchoolLevel.SchoolLevelID = School.SchoolLevelID
            join( select ProcessProgress.SchoolID, max(StatusDate) as maxdate from ProcessProgress join School on ProcessProgress.SchoolID= School.SchoolID
            group by ProcessProgress.SchoolID
            ) temp
            on temp.maxdate = ProcessProgress.StatusDate");

            List<SchoolListAll> List = _applicationContext.SchoolListAll.FromSqlRaw(query.ToString()).ToList();

            return View(List);
        }


        [HttpGet]
        public IActionResult AllReports()
        {
            StringBuilder query = null;
            query = new StringBuilder(@$"select school.*,zProcessStatus.StatusNameDariPast,zProcessStatus.StatusNamePast ,ProcessProgress.StatusDate,
                zProvince.PROV_NA_DAR,
                zProvince.PROV_NA_ENG,
                zDistrict.DIST_NA_DAR,
                zDistrict.DIST_NA_ENG,
                 zSchoolGenderType.SchoolGenderTypeNameDari
                ,zSchoolGenderType.SchoolGenderTypeName
                ,zSchoolLevel.SchoolLevelNameDari
                ,zSchoolLevel.SchoolLevelName
                 from ProcessProgress 
                inner join School on ProcessProgress.SchoolID = School.SchoolID
                join zProcessStatus on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
                join PartyAddress on School.SchoolID=PartyAddress.PartyID
                join zProvince on PartyAddress.ProvinceID = zProvince.ProvinceID
                join zDistrict on PartyAddress.DistrictID = zDistrict.DistrictID
                join zSchoolGenderType on zSchoolGenderType.SchoolGenderTypeID = School.SchoolGenderTypeID
                join zSchoolLevel on zSchoolLevel.SchoolLevelID = School.SchoolLevelID
                join( select ProcessProgress.SchoolID, max(StatusDate) as maxdate from ProcessProgress join School on ProcessProgress.SchoolID= School.SchoolID
                group by ProcessProgress.SchoolID
                ) temp
                on temp.maxdate = ProcessProgress.StatusDate");

            List<SchoolListAll> List = _applicationContext.SchoolListAll.FromSqlRaw(query.ToString()).ToList();

            var SchoolName = List.OrderBy(o => o.SchoolName);
            ViewBag.SchoolN = SchoolName;

            var StatusName = _applicationContext.ZProcessStatus.OrderBy(p => p.StatusNameDariPast);
            ViewBag.StatusN = StatusName;

            var ProvinceName = _applicationContext.ZProvince.OrderBy(p => p.ProvNaDar); 
            ViewBag.ProvinceN= ProvinceName;

            var DistrictName = _applicationContext.ZDistrict.OrderBy(p => p.DistNaDar);
            ViewBag.DistrictN = DistrictName;

            var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolGenderType = SchoolGenderType;

            
            return View();
            }

        [HttpPost]
        public IActionResult AllReports(searchreportviewModel allReports)
        {
            StringBuilder query = null;
            query = new StringBuilder(@$"select school.*,zProcessStatus.StatusNameDariPast,zProcessStatus.StatusNamePast ,ProcessProgress.StatusDate,
                zProvince.PROV_NA_DAR,
                zProvince.PROV_NA_ENG,
                zDistrict.DIST_NA_DAR,
                zDistrict.DIST_NA_ENG
                ,zSchoolGenderType.SchoolGenderTypeNameDari
                ,zSchoolGenderType.SchoolGenderTypeName
                ,zSchoolLevel.SchoolLevelNameDari
                ,zSchoolLevel.SchoolLevelName
                 from ProcessProgress 
                inner join School on ProcessProgress.SchoolID = School.SchoolID
                join zProcessStatus on ProcessProgress.ProcessStatusID=zProcessStatus.ProcessStatusID
                join PartyAddress on School.SchoolID=PartyAddress.PartyID
                join zProvince on PartyAddress.ProvinceID = zProvince.ProvinceID
                join zDistrict on PartyAddress.DistrictID = zDistrict.DistrictID
                join zSchoolGenderType on zSchoolGenderType.SchoolGenderTypeID = School.SchoolGenderTypeID
                join zSchoolLevel on zSchoolLevel.SchoolLevelID = School.SchoolLevelID
                join( select ProcessProgress.SchoolID, max(StatusDate) as maxdate from ProcessProgress join School on ProcessProgress.SchoolID= School.SchoolID
                group by ProcessProgress.SchoolID
                ) temp
                on temp.maxdate = ProcessProgress.StatusDate");

            List<SchoolListAll> List = _applicationContext.SchoolListAll.FromSqlRaw(query.ToString()).ToList();

            var SchoolName = List.OrderBy(o => o.SchoolName);
            ViewBag.SchoolN = SchoolName;

            var ProvinceName = _applicationContext.ZProvince.OrderBy(p =>p.ProvNaDar);
            ViewBag.ProvinceN = ProvinceName;

            var StatusName = _applicationContext.ZProcessStatus.OrderBy(p => p.StatusNameDariPast);
            ViewBag.StatusN = StatusName;

            var DistrictName = _applicationContext.ZDistrict.OrderBy(p => p.DistNaDar);
            ViewBag.DistrictN = DistrictName;

            var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolGenderType = SchoolGenderType;


            if (allReports.schoolname != null) 
            {
                return View(List.Where(x => x.SchoolName.Contains(allReports.schoolname) || allReports.schoolname == null).ToList());
            }


            if (allReports.schoollevel != null)
            {
                //selecting schoollevel , provincename, status, schoolgender at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, status and provincename at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, status and schoolgender at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, provincename and schoolgender at same time
                if (allReports.schoollevel != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }

                //selecting schoollevel, startdate, enddate, provincename, status and schoolgender at the samt time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.status !=null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and schoolgender  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.schoolgender !=null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate and province name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, startdate, enddate and status name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and schoolgender name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate and enddate at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate).ToList());
                }
                if (allReports.schoollevel != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.PROV_NA_DAR.Contains(allReports.provincename) || x.PROV_NA_ENG.Contains(allReports.provincename)).ToList());
                }
                if (allReports.schoollevel != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                if (allReports.schoollevel != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) || x.SchoolLevelName.Contains(allReports.schoollevel) || allReports.schoollevel == null).ToList());
            }

            //Condtions for province Name
            if (allReports.provincename != null)
            {
                //selecting schoollevel , provincename, status, schoolgender at same time
                if (allReports.provincename != null && allReports.status != null && allReports.schoollevel != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, status and provincename at same time
                if (allReports.provincename != null && allReports.status != null && allReports.schoollevel != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting provincename, status and schoolgender at same time
                if (allReports.provincename != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, provincename and schoolgender at same time
                if (allReports.provincename != null && allReports.schoollevel != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, provincename, status and schoolgender at the samt time
                if (allReports.provincename != null && allReports.startdate != null && allReports.enddate != null && allReports.schoollevel != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate  && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and schoolgender  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, provincename and status  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and provincename name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting provincename, startdate, enddate and status name at same time
                if (allReports.provincename != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting provincename, startdate, enddate and schoolgender name at same time
                if (allReports.provincename != null && allReports.startdate != null && allReports.enddate != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting provincename, startdate and enddate at same time
                if (allReports.provincename != null && allReports.startdate != null && allReports.enddate != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate).ToList());
                }
                //province name and school level
                if (allReports.schoollevel != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.PROV_NA_DAR.Contains(allReports.provincename) || x.PROV_NA_ENG.Contains(allReports.provincename)).ToList());
                }
                //provincename and school gender
                if (allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //province name and status
                if (allReports.provincename != null && allReports.status != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }

                return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) || x.PROV_NA_ENG.Contains(allReports.provincename) || allReports.provincename == null).ToList());
            }

            //condtion for status
            if (allReports.status != null)
            {
                //selecting schoollevel , provincename, status, schoolgender at same time
                if (allReports.status != null && allReports.schoollevel != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, status and provincename at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, status and schoolgender at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, provincename and schoolgender at same time
                if (allReports.status != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, provincename, status and schoolgender at the samt time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and schoolgender  name at same time
                if (allReports.status != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and status  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate, schoolgender and status  name at same time
                if (allReports.status != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and province name at same time
                if (allReports.status != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, startdate, enddate and status name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and schoolgender name at same time
                if (allReports.status != null && allReports.startdate != null && allReports.enddate != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate and enddate at same time
                if (allReports.status != null && allReports.startdate != null && allReports.enddate != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate).ToList());
                }

                if (allReports.status != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) || x.PROV_NA_ENG.Contains(allReports.provincename)).ToList());
                }
                if (allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                if (allReports.schoollevel != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }

                return View(List.Where(x => x.StatusNameDariPast.Contains(allReports.status) || allReports.status == null).ToList());
            }
            if (allReports.schoolgender != null)
            {
                //selecting schoollevel , provincename, status, schoolgender at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, status and provincename at same time
                if (allReports.schoolgender != null && allReports.status != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusNameDariPast.Contains(allReports.status) && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, status and schoolgender at same time
                if (allReports.schoollevel != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, provincename and schoolgender at same time
                if (allReports.schoollevel != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, provincename, status and schoolgender at the samt time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.StatusNameDariPast.Contains(allReports.status) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and schoolgender  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate, enddate, province and status  name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and province name at same time
                if (allReports.schoolgender != null && allReports.startdate != null && allReports.enddate != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.PROV_NA_DAR.Contains(allReports.provincename)).ToList());
                }
                //selecting schoollevel, startdate, enddate and status name at same time
                if (allReports.schoolgender != null && allReports.startdate != null && allReports.enddate != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }
                //selecting schoollevel, startdate, enddate and schoolgender name at same time
                if (allReports.schoollevel != null && allReports.startdate != null && allReports.enddate != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                //selecting schoollevel, startdate and enddate at same time
                if (allReports.schoolgender != null && allReports.startdate != null && allReports.enddate != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate).ToList());
                }
                if (allReports.schoolgender != null && allReports.provincename != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.PROV_NA_DAR.Contains(allReports.provincename) || x.PROV_NA_ENG.Contains(allReports.provincename)).ToList());
                }
                if (allReports.schoollevel != null && allReports.schoolgender != null)
                {
                    return View(List.Where(x => x.SchoolLevelNameDari.Contains(allReports.schoollevel) && x.SchoolGenderTypeName.Contains(allReports.schoolgender)).ToList());
                }
                if (allReports.schoolgender != null && allReports.status != null)
                {
                    return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) && x.StatusNameDariPast.Contains(allReports.status)).ToList());
                }

                return View(List.Where(x => x.SchoolGenderTypeName.Contains(allReports.schoolgender) || allReports.schoolgender == null).ToList());
            }
            if(allReports.generalreport != null)
            {
                return View(List);
            }

            if (allReports.startdate != null && allReports.enddate != null)
            {
                return View(List.Where(x => x.StatusDate >= allReports.startdate && x.StatusDate <= allReports.enddate).ToList());

            }
            return View();
           
        }
    }
}