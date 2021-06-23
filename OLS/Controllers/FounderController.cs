using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using OLS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OLS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    [Route("Founder")]
    [Authorize(Roles = "Applicant")]
    public class FounderController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly INotyfService notyfService;
        private readonly IHtmlLocalizer _localizer;

        public FounderController(ApplicationContext applicationContext, IHtmlLocalizer<FounderController> localizer, IHostingEnvironment environment,UserManager<User> userManager, INotyfService notyfService)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
            this.notyfService = notyfService;
            _localizer = localizer;
        }

        [Route("index")]
        [Route("")]
        [Route("~")]
        public IActionResult Index()
        {
            return View();
         
        }
        [HttpGet]
        public IActionResult Navigate(string? newSchool,string? mySchoolId)
        {
            var UserId = _userManager.GetUserId(User);
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p => p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();









            //New codes for applying multiple applications


            var result = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolid;


            if (mySchoolId != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("mySchoolId", mySchoolId);
             
            }


            var sch_id = HttpContext.Session.GetString("mySchoolId");
            if (sch_id != null)
            {
                schoolid = Guid.Parse(sch_id);
            }

            if (newSchool != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.SetString("new", "new");
               
            }
            var new_sch = HttpContext.Session.GetString("new");
            if (new_sch != null)
            {
                schoolid = Guid.NewGuid();
            }


            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid != null)
            {
                schoolid =Guid.Parse(myschoolid);
            }

            if (result == null)
            {
                schoolid = re_schoolid;
            }


            //End New Changes


            var displayPlan = (from process in _applicationContext.Process
                               join subProcess in _applicationContext.SubProcess on process.ProcessId equals subProcess.ProcessId into processgroup
                               from a in processgroup.DefaultIfEmpty()
                               join processProgress in _applicationContext.ProcessProgress on a.SubProcessId equals processProgress.SubProcessId into processProgressGroup
                               from b in processProgressGroup.DefaultIfEmpty()
                               join school in _applicationContext.School on b.SchoolId equals school.SchoolId into schoolGroup
                               from c in schoolGroup.DefaultIfEmpty()
                               join zProcessStatus in _applicationContext.ZProcessStatus on b.ProcessStatusId equals zProcessStatus.ProcessStatusId into zProcessStatusGroup
                               from d in zProcessStatusGroup.DefaultIfEmpty()
                               join subProcessStatus in _applicationContext.SubProcessStatus on new { a = a.SubProcessId.ToString(), b = b.ProcessStatusId.ToString() } equals new { a = subProcessStatus.SubProcessId.ToString(), b = subProcessStatus.ProcessStatusId.ToString() } into subProcessStatusGroup
                               from e in subProcessStatusGroup.DefaultIfEmpty()
                               where process.ProcessId == id && c.SchoolId == schoolid

                               select new SubProcessViewModel
                               {
                                   SubProcessId = a.SubProcessId,
                                   ProcessId = process.ProcessId,
                                   ProcessName = process.ProcessName,
                                   SubProcesName = a.SubProcesName,
                                   SubProcesNameDari = a.SubProcesNameDari,
                                   OrderNumber = a.OrderNumber,
                                   TimelineInDays = a.TimelineInDays,
                                   StatusNamePast = d.StatusNamePast,
                                   StatusNameDariPast = d.StatusNameDariPast,
                                   CompletionFlag = e.CompletionFlag,
                                   Remarks = b.Remarks,
                                   StatusDate = b.StatusDate,

                               }).OrderBy(p => p.OrderNumber).ToList();





            var founder = new Person();


             founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId &&  p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            if (result==null)
            {
                
                    founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolid && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();
            }
            else if(result!=null && founder!=null)

            {
                if (founder.SchoolId == null)
                {
                    founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();
                } 
                else if (founder.SchoolId != null)
                {
                    founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.SchoolId == schoolid && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();
                }
                else
                {
                    founder = null;
                }
                    
            }
          
            //else
            //{
              


            //    if (newSchool != null && sch_check!=null)
            //    {
            //        founder = null;
            //    }
            //    else
            //    {
            //        //    var founder_id = HttpContext.Session.GetString("FounderID");
            //        //    var personId = Guid.NewGuid();
            //        //    if (founder_id != null)
            //        //    {
            //        //        personId = Guid.Parse(founder_id);
            //        //    }


            //        //    founder = _applicationContext.Person.Where(p => p.PersonId == personId && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            //        founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();
            //    }



            //        //if (sch_id != null || myschoolid != null)
            //        //{
            //        //    founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.SchoolId == schoolid && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            //        //}

                   
            //    }

     

          
          
            ViewBag.found = founder;
            if (founder != null && founder.CreatedBy !=null)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { founderid = founder.PersonId });
                        }
                    }
                    return RedirectToAction("NoEdit", new { founderid = founder.PersonId });
                }
                else
                {
                    return RedirectToAction("Edit", new { founderid = founder.PersonId });
                }

            }


            return RedirectToAction("Create");

        }

        [Route("FindDistrict/{ProvinceId}")]
        public IActionResult FindDistrict(int ProvinceId)
        {
            var districts = _applicationContext.ZDistrict.Where(district => district.ProvinceId == ProvinceId).Select(distict => new { 
            Id=distict.DistrictId,
            DistNameEng=distict.DistNaEng,
            DistNameDar=distict.DistNaDar
            
            }).ToList();
            return new JsonResult(districts);
        }
        [Route("FindVillagNahia/{DistrictId}")]
        public IActionResult FindVillagNahia(int DistrictId)
        {
            var VillageNahias = _applicationContext.ZVillageNahia.Where(villigaeNahia => villigaeNahia.DistrictId == DistrictId)
                .Select(villageNahia =>new { 
                    Vid =villageNahia.VillageNahiaId,
                    VNameEng=villageNahia.VillageNameEng
            
            }).ToList();
            return new JsonResult(VillageNahias);
        }


      
        [AcceptVerbs("Get","Post")]
        [Route("IsEmailUnique")]
        public async Task<IActionResult> IsEmailUnique(string email) {

            var founder = _applicationContext.ContactDetails.Where( p => p.Value==email).FirstOrDefault();
            if (founder == null)
            {

                return Json(true);
            }
            else {
                return Json(_localizer["EmailInUse"].Value);
            }

        }

        [AcceptVerbs("Get", "Post")]
        [Route("IsEmailUniqueEdit")]
        public async Task<IActionResult> IsEmailUniqueEdit(string Email, Guid PersonId)
        {
            var EmailCount = _applicationContext.ContactDetails.Where(p => p.Value == Email && p.PartyId == PersonId).GroupBy(i => new { i.Value }).Select(g => new { g.Key.Value, Count = g.Count() }).ToList();
            var EmailCountOther = _applicationContext.ContactDetails.Where(p => p.Value == Email).GroupBy(i => new { i.Value }).Select(g => new { g.Key.Value, Count = g.Count() }).ToList();

            if (EmailCount.Count == 1)
            {

                return Json(true);
            }
            else if (EmailCountOther.Count>0) {

                return Json(_localizer["EmailInUse"].Value);
            }
            else
            {
                return Json(true);
            }

        }


        [AcceptVerbs("Get", "Post")]
        [Route("IsPhoneUnique")]
        public async Task<IActionResult> IsPhoneUnique(string PhonNumber)
        {

            var founder = _applicationContext.ContactDetails.Where(p => p.Value == PhonNumber).FirstOrDefault();
            if (PhonNumber.Length != 10) {

                return Json(_localizer["PhoneNumberDigits"].Value);
            }
            if (founder == null)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["PhoneNumberInUse"].Value);
            }

        }

        [AcceptVerbs("Get", "Post")]
        [Route("IsPhoneUniqueEdit")]
        public async Task<IActionResult> IsPhoneUniqueEdit(string PhonNumber, Guid PersonId)
        {          
            var PhoneCount = _applicationContext.ContactDetails.Where(p => p.Value == PhonNumber && p.PartyId == PersonId).GroupBy(i => new { i.Value }).Select(g => new { g.Key.Value, Count = g.Count() }).ToList();
            var PhoneCountOther = _applicationContext.ContactDetails.Where(p => p.Value == PhonNumber).GroupBy(i => new { i.Value }).Select(g => new { g.Key.Value, Count = g.Count() }).ToList();
            if (PhonNumber.Length != 10)
            {

                return Json(_localizer["PhoneNumberDigits"].Value);
            }
            if (PhoneCount.Count == 1)
            {

                return Json(true);
            }
            else if (PhoneCountOther.Count>0) {
                return Json(_localizer["PhoneNumberInUse"].Value);

            }
            else
            {
                return Json(true);
            }

        }


        [AcceptVerbs("Get", "Post")]
        [Route("IsNIDUnique")]
        public async Task<IActionResult> IsNIDUnique(string NIDNumber)
        {

            var founder = _applicationContext.Person.Where(p => p.Nidnumber == NIDNumber).FirstOrDefault();
            if (founder == null)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["NIDNumberInUse"].Value);
            }

        }
        [AcceptVerbs("Get", "Post")]
        [Route("IsNIDUniqueEdit")]
        public async Task<IActionResult> IsNIDUniqueEdit(string NIDNumber, Guid PersonId)
        {

            var NIDCount =  _applicationContext.Person.Where(p => p.Nidnumber == NIDNumber && p.PersonId== PersonId).GroupBy( i=> new { i.Nidnumber }).Select(g => new {g.Key.Nidnumber, Count = g.Count() }).ToList();
            var NIDCountOther = _applicationContext.Person.Where(p => p.Nidnumber == NIDNumber).GroupBy(i => new { i.Nidnumber }).Select(g => new { g.Key.Nidnumber, Count = g.Count() }).ToList();
            if (NIDCount.Count == 1)
            {

                return Json(true);
            }
            else if (NIDCountOther.Count > 0)
            {
                return Json(_localizer["NIDNumberInUse"].Value);

            }
            else
            {
                return Json(true);
            }

        }

        [Route("GetFounderByPhoneOrEmail")]
        [HttpPost]
        public IActionResult GetFounderByPhoneOrEmail(string val) {

            if (val != null) {
                var person = _applicationContext.ContactDetails.Where(p => p.Value == val).FirstOrDefault();

                return RedirectToAction("Edit", new { founderid = person.PartyId });


            }
            return View("index");

        }
        [Route("GetFounderByPhoneOrEmail")]
        [HttpGet]
        public IActionResult GetFounderByPhoneOrEmail()
        {

            return View();

        }

        [Route("NoEdit/{founderid}")]
        [HttpGet]
        public IActionResult NoEdit(Guid founderid)
        {
            try
            {
                if (founderid != null)
                {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;
                    var foundedetails = _applicationContext.Person.Find(founderid);
                    var founderPhone = _applicationContext.ContactDetails.Where(p => p.PartyId == founderid && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var founderEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == founderid && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var founderEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == founderid).FirstOrDefault();
                    var founderPerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderid && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var founderPreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderid && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince;
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                    // var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                    //ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince;
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    // var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                    // ViewBag.PrevillageNahia = PrevillageNahia;

                    FounderEditViewModel founder = new FounderEditViewModel
                    {
                        PersonId = foundedetails.PersonId,
                        Name = foundedetails.Name,
                        LastName = foundedetails.LastName,
                        FatherName = foundedetails.FatherName,
                        GrandFatherName = foundedetails.GrandFatherName,
                        Nidnumber = foundedetails.Nidnumber,
                        Age = foundedetails.Age,
                        GenderTypeId = foundedetails.GenderTypeId,
                        PhonNumber = founderPhone.Value,
                        Email = founderEmail.Value,
                        EducationLevelID = founderEducation.EducationLevelId,
                        PerProvinceId = founderPerAddress.ProvinceId,
                        PerDistrictId = founderPerAddress.DistrictId,
                        PerNahia = founderPerAddress.Nahia,
                        PreProvinceId = founderPreAddress.ProvinceId,
                        PreDistrictId = founderPreAddress.DistrictId,
                        PreNahia = founderPreAddress.Nahia,
                        ExistingPhotoPath = foundedetails.Photo,
                        SchoolId = foundedetails.SchoolId
                    };
                    
                    return View(founder);
                }
                else
                {
                    return View("index");

                }


            }
            catch (Exception ex)
            {

                return View("Index");
            }

        }


        [Route("Edit/{founderid}")]
        [HttpGet]
        public IActionResult Edit(Guid founderid)
        {
            try {
                if (founderid!=null) {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;
                    var foundedetails = _applicationContext.Person.Find(founderid);
                    var founderPhone = _applicationContext.ContactDetails.Where(p => p.PartyId == founderid && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var founderEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == founderid && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var founderEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == founderid).FirstOrDefault();
                    var founderPerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderid && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var founderPreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderid && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince;
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                   // var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                    //ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince;
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                   // var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                   // ViewBag.PrevillageNahia = PrevillageNahia;

                    FounderEditViewModel founder = new FounderEditViewModel
                    {
                        PersonId = foundedetails.PersonId,
                        Name = foundedetails.Name,
                        LastName = foundedetails.LastName,
                        FatherName = foundedetails.FatherName,
                        GrandFatherName = foundedetails.GrandFatherName,
                        Nidnumber = foundedetails.Nidnumber,
                        Age = foundedetails.Age,
                        GenderTypeId = foundedetails.GenderTypeId,
                        PhonNumber = founderPhone.Value,
                        Email = founderEmail.Value,
                        EducationLevelID = founderEducation.EducationLevelId,
                        PerProvinceId = founderPerAddress.ProvinceId,
                        PerDistrictId = founderPerAddress.DistrictId,
                        PerNahia = founderPerAddress.Nahia,
                        PreProvinceId = founderPreAddress.ProvinceId,
                        PreDistrictId = founderPreAddress.DistrictId,
                        PreNahia = founderPreAddress.Nahia,                   
                        ExistingPhotoPath = foundedetails.Photo,
                        SchoolId = foundedetails.SchoolId
                    };

                    HttpContext.Session.SetString("FounderID", founderid.ToString());

                    return View(founder);
                }
                else {
                    return View("index");

                }
               

            } catch (Exception ex) {

                return View("Index");
            }
           
        }

        [Route("Edit/{founderid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(FounderEditViewModel founderModel)
        {

            try {

                if (ModelState.IsValid)
                {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;
                    var foundedetails = _applicationContext.Person.Find(founderModel.PersonId);
                    var founderPhone = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var founderEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var founderEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == founderModel.PersonId).FirstOrDefault();
                    var founderPerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var founderPreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                    //var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                    //ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    //var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                    //ViewBag.PrevillageNahia = PrevillageNahia;

                    Person person = _applicationContext.Person.Find(founderModel.PersonId);
                    person.Name = founderModel.Name;
                    person.LastName = founderModel.LastName;
                    person.FatherName = founderModel.FatherName;
                    person.GrandFatherName = founderModel.GrandFatherName;
                    person.Nidnumber = founderModel.Nidnumber;
                    person.Age = founderModel.Age;
                    person.GenderTypeId = founderModel.GenderTypeId;
                    person.UpdatedBy = _userManager.GetUserId(User).ToString();
                    person.UpdatedAt = DateTime.Now;


                    ContactDetails PhoneNumber = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    PhoneNumber.Value = founderModel.PhonNumber;
                    PhoneNumber.UpdatedBy = _userManager.GetUserId(User).ToString();
                    PhoneNumber.UpdatedAt = DateTime.Now;

                    ContactDetails Email = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    Email.Value = founderModel.Email;
                    Email.UpdatedBy = _userManager.GetUserId(User).ToString();
                    Email.UpdatedAt = DateTime.Now;


                    PersonEducation personEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == founderModel.PersonId).FirstOrDefault();
                    personEducation.EducationLevelId = founderModel.EducationLevelID;
                    personEducation.UpdatedBy = _userManager.GetUserId(User).ToString();
                    personEducation.UpdatedAt = DateTime.Now;

                    PartyAddress PermenantAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    PermenantAddress.ProvinceId = founderModel.PerProvinceId;
                    PermenantAddress.DistrictId = founderModel.PerDistrictId;
                    PermenantAddress.Nahia = founderModel.PerNahia;
                    PermenantAddress.UpdatedBy = _userManager.GetUserId(User).ToString();
                    PermenantAddress.UpdatedAt = DateTime.Now;

                    PartyAddress CurrentAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();
                    CurrentAddress.ProvinceId = founderModel.PreProvinceId;
                    CurrentAddress.DistrictId = founderModel.PreDistrictId;
                    CurrentAddress.Nahia = founderModel.PreNahia;
                    CurrentAddress.UpdatedBy = _userManager.GetUserId(User).ToString();
                    CurrentAddress.UpdatedAt = DateTime.Now;


                    string FilePath = "";

                    if (founderModel.Photo != null)
                    {
                        string[] _Extensions = new string[] { ".jpg", ".png", ".jpeg" };
                        int _maxFileSize = 500 * 1024;
                        var extension = Path.GetExtension(founderModel.Photo.FileName);
                        if (_Extensions.Contains(extension.ToLower()) && founderModel.Photo.Length < _maxFileSize)
                        {
                            string UploadsFolder = Path.Combine(_env.WebRootPath, "Person", founderModel.PersonId.ToString());
                            if (!Directory.Exists(Path.Combine(UploadsFolder)))
                            {
                                Directory.CreateDirectory(UploadsFolder);

                            }
                            string fileName = founderModel.PersonId.ToString() + "-Founder photo.jpeg";
                            FilePath = Path.Combine(UploadsFolder, fileName);
                            System.IO.File.Delete(FilePath);
                            using (var filestream = new FileStream(FilePath, FileMode.Create))
                            {
                                founderModel.Photo.CopyTo(filestream);
                            }
                            person.Photo = "/Person/"+ founderModel.PersonId.ToString()+"/" +fileName;
                        }
                        else
                        {
                            ViewBag.photoerror = _localizer["PhotoError"].Value;
                            return View(founderModel);
                           
                        }
                    }

                    _applicationContext.Update(person);
                    _applicationContext.Update(PhoneNumber);
                    _applicationContext.Update(Email);
                    _applicationContext.Update(personEducation);
                    _applicationContext.Update(PermenantAddress);
                    _applicationContext.Update(CurrentAddress);
                    await _applicationContext.SaveChangesAsync();
                    notyfService.Custom(_localizer["FounderUpdated"].Value, 10, "#67757c", "fa fa-check");
                    ViewBag.Message = _localizer["RecordSaved"].Value;
                    
                    return View(founderModel);

                }


                var EducationL = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                ViewBag.EducationLevel = EducationL;

                var GenderT = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                ViewBag.GenderType = GenderT;


                var founded = _applicationContext.Person.Find(founderModel.PersonId);
                var founderPh = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                var founderEm = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                var founderEdu = _applicationContext.PersonEducation.Where(p => p.PersonId == founderModel.PersonId).FirstOrDefault();
                var founderPerAdd = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                var founderPreAdd = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();


                var Perpro = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPerAdd.ProvinceId);
                ViewBag.Perprovince = Perpro;
                var Perdist = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPerAdd.ProvinceId);
                ViewBag.Perdistrict = Perdist;
                //var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                //ViewBag.PervillageNahia = PervillageNahia;

                var Prepro = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPreAdd.ProvinceId);
                ViewBag.Preprovince = Prepro;
                var Predist = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAdd.ProvinceId);
                ViewBag.Predistrict = Predist;





                return View();

            } catch (Exception ex) {

                return View("Index");
            }

         
           

        }


        [Route("Create")]
        public IActionResult Create()
        {
 
            Guid DropPID = Guid.NewGuid();
            var province = _applicationContext.ZProvince;
            ViewBag.Province = province;

            var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
            ViewBag.EducationLevel = EducationLevel;

            var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.GenderType = GenderType;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(FounderViewModel founder )
        {
            Guid DropPID = Guid.NewGuid();
            var province = _applicationContext.ZProvince;
            ViewBag.Province = province;

            var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
            ViewBag.EducationLevel = EducationLevel;

            var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.GenderType = GenderType;

            if (ModelState.IsValid)
            {
                string FilePath = "";
                string photo = "";
                Guid Pid = Guid.NewGuid();
                if (founder.Photo != null)
                {

                    string UploadsFolder = Path.Combine(_env.WebRootPath, "Person", Pid.ToString());
                    if (!Directory.Exists(Path.Combine(UploadsFolder)))
                    {
                        Directory.CreateDirectory(UploadsFolder);
                    }
                    string fileName = Pid.ToString() + "-Founder photo.jpeg";
                    FilePath = Path.Combine(UploadsFolder, fileName);
                    using (var filestream = new FileStream(FilePath, FileMode.Create))
                    {
                        await founder.Photo.CopyToAsync(filestream);
                    }
                    photo = "/Person/" + Pid.ToString() + "/" + fileName;
                }

                Party party = new Party() { PartyId = Pid };
                Person person = new Person()
                {
                    PersonId = Pid,
                    Name = founder.Name,
                    LastName = founder.LastName,
                    FatherName = founder.FatherName,
                    GrandFatherName = founder.GrandFatherName,
                    Nidnumber = founder.Nidnumber,
                    Age = founder.Age,
                    Photo = photo,
                    GenderTypeId = founder.GenderTypeId,
                    PartyRoleTypeId = Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236"),
                    CreatedBy=_userManager.GetUserId(User),
                    CreatedAt= DateTime.Now,
                };

                Guid PhoneCid = Guid.NewGuid();
                ContactDetails PhoneNumber = new ContactDetails()
                {
                    ContactDetailId = PhoneCid,
                    PartyId = Pid,
                    ContactMechanismTypeId = Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB"),
                    Value = founder.PhonNumber,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid EmailCid = Guid.NewGuid();
                ContactDetails Email = new ContactDetails()
                {
                    ContactDetailId = EmailCid,
                    PartyId = Pid,
                    ContactMechanismTypeId = Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647"),
                    Value = founder.Email,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid personEductionID = Guid.NewGuid();
                PersonEducation personEducation = new PersonEducation()
                {
                    PersonId = Pid,
                    PersonEducationId = personEductionID,
                    EducationLevelId = founder.EducationLevelID,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid PerAaddressId = Guid.NewGuid();
                PartyAddress PermenantAddress = new PartyAddress()
                {
                    PartyAddressId = PerAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7"),
                    ProvinceId = founder.PerProvinceId,
                    DistrictId = founder.PerDistrictId,
                    Nahia = founder.PerNahia,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };
                Guid CurrAaddressId = Guid.NewGuid();
                PartyAddress CurrentAddress = new PartyAddress()
                {
                    PartyAddressId = CurrAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52"),
                    ProvinceId = founder.PreProvinceId,
                    DistrictId = founder.PreDistrictId,
                    Nahia = founder.PreNahia,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };
                _applicationContext.Add(party);
                _applicationContext.Add(person);
                _applicationContext.Add(PhoneNumber);
                _applicationContext.Add(Email);
                _applicationContext.Add(personEducation);
                _applicationContext.Add(PermenantAddress);
                _applicationContext.Add(CurrentAddress);
                await _applicationContext.SaveChangesAsync();

                HttpContext.Session.Remove("new");
                notyfService.Custom(_localizer["FounderCreated"].Value, 10, "#67757c", "fa fa-check");
                ViewBag.Message = _localizer["RecordSaved"].Value;
                HttpContext.Session.SetString("FounderID", Pid.ToString());

                // return RedirectToAction("Edit", new { founderid = Pid });
                return RedirectToAction("Navigate", "SChool");
               
            }


            var prov = _applicationContext.ZProvince;
            ViewBag.Province = prov;

            var EducationL = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
            ViewBag.EducationLevel = EducationL;

            var GType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.GenderType = GType;
            return View("Create"); 
           
        }
    }
}