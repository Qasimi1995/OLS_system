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
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    [Route("Principle")]
    [Authorize(Roles = "Applicant")]
    public class PrincipleController : Controller
    {
        private ApplicationContext _applicationContext;
        IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly INotyfService notyfService;
        private readonly IHtmlLocalizer _localizer;

        public PrincipleController(ApplicationContext applicationContext, IHtmlLocalizer<PrincipleController> localizer, IWebHostEnvironment environment,UserManager<User> userManager,
            INotyfService notyfService)
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
        public IActionResult Navigate()
        {
            var UserId = _userManager.GetUserId(User);
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p => p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();



            var result = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolid;


            //New Changes

            var sch_id = HttpContext.Session.GetString("mySchoolId");
            if (sch_id != null)
            {
                schoolid = Guid.Parse(sch_id);
            }

            var newSchool = HttpContext.Session.GetString("new");
            if (newSchool != null)
            {
                schoolid = Guid.NewGuid();
            }

            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid != null)
            {
                schoolid = Guid.Parse(myschoolid);
            }

            if (myschoolid == null && sch_id == null)
            {
                schoolid = Guid.NewGuid();
            }


            if (result == null)
            {
                schoolid = re_schoolid;
            }





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
            
            var principle = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolid && p.PartyRoleTypeId== Guid.Parse("FD744DE3-476C-428F-B7E0-381A9DD286FE")).FirstOrDefault();
            ViewBag.principle = principle;
            if (principle != null)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { principleid = principle.PersonId });
                        }
                    }
                    return RedirectToAction("NoEdit", new { principleid = principle.PersonId });
                }
                else
                {
                    return RedirectToAction("Edit", new { principleid = principle.PersonId });
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
        public async Task<IActionResult> IsEmailUniqueEdit(string email, Guid PersonId)
        {
            var PhoneCount = _applicationContext.ContactDetails.Where(p => p.Value == email && p.PartyId == PersonId).GroupBy(i => new { i.Value }).Select(g => new { g.Key.Value, Count = g.Count() }).ToList();
            if (PhoneCount.Count == 1)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["EmailInUse"].Value);
            }

        }


        [AcceptVerbs("Get", "Post")]
        [Route("IsPhoneUnique")]
        public async Task<IActionResult> IsPhoneUnique(string PhonNumber)
        {

            var founder = _applicationContext.ContactDetails.Where(p => p.Value == PhonNumber).FirstOrDefault();
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
            if (PhoneCount.Count == 1)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["PhoneNumberInUse"].Value);
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
                return Json(_localizer["NIDInUse"].Value);
            }

        }
        [AcceptVerbs("Get", "Post")]
        [Route("IsNIDUniqueEdit")]
        public async Task<IActionResult> IsNIDUniqueEdit(string NIDNumber, Guid PersonId)
        {

            var NIDCount =  _applicationContext.Person.Where(p => p.Nidnumber == NIDNumber && p.PersonId== PersonId).GroupBy( i=> new { i.Nidnumber }).Select(g => new {g.Key.Nidnumber, Count = g.Count() }).ToList();
            
            if (NIDCount.Count == 1)
            {

                return Json(true);
            }
            else
            {
                return Json(_localizer["NIDInUse"].Value);
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

        [Route("InsertNewFacultyType/{FacultyName}")]
        public async Task<JsonResult> InsertNewFacultyType(string FacultyName)
        {
            var FacultyType = new ZFacultyType();
            FacultyType.FacultyTypeId = Guid.NewGuid();
            FacultyType.FacultypeNameDari = FacultyName+"/"+FacultyName;
            FacultyType.FacultypeName = FacultyName;
            _applicationContext.ZFacultyType.Add(FacultyType);
            await  _applicationContext.SaveChangesAsync().ConfigureAwait(true);
            return Json(FacultyType.FacultyTypeId);
        }


        [Route("NoEdit/{principleid}")]
        [HttpGet]
        public IActionResult NoEdit(Guid principleid)
        {
            try
            {
                if (principleid != null)
                {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;

                    var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
                    ViewBag.FacultyType = FacultyType;

                    var principledetails = _applicationContext.Person.Find(principleid);
                    var principlePhone = _applicationContext.ContactDetails.Where(p => p.PartyId == principleid && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var principleEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == principleid && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var principleEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == principleid).FirstOrDefault();
                    var principlePerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principleid && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var principlePreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principleid && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince;
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                    //  var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == principlePerAddress.DistrictId);
                    // ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince;
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    //var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == principlePreAddress.DistrictId);
                    //ViewBag.PrevillageNahia = PrevillageNahia;

                    PrincipleEditViewModel principle = new PrincipleEditViewModel
                    {
                        PersonId = principledetails.PersonId,
                        Name = principledetails.Name,
                        LastName = principledetails.LastName,
                        FatherName = principledetails.FatherName,
                        GrandFatherName = principledetails.GrandFatherName,
                        Nidnumber = principledetails.Nidnumber,
                        Age = principledetails.Age,
                        GenderTypeId = principledetails.GenderTypeId,
                        Eduservice = principledetails.Eduservice,
                        PhonNumber = principlePhone.Value,
                        Email = principleEmail.Value,
                        EducationLevelID = principleEducation.EducationLevelId,
                        FacultyTypeId = principleEducation.FacultyTypeId,
                        GraduationDate = principleEducation.GraduationDate,
                        PerProvinceId = principlePerAddress.ProvinceId,
                        PerDistrictId = principlePerAddress.DistrictId,
                        PerNahia = principlePerAddress.Nahia,
                        PreProvinceId = principlePreAddress.ProvinceId,
                        PreDistrictId = principlePreAddress.DistrictId,
                        PreNahia = principlePreAddress.Nahia,
                        ExistingPhotoPath = principledetails.Photo,

                    };
                    HttpContext.Session.SetString("FounderID", principledetails.PersonId.ToString());
                    return View(principle);
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



        [Route("Edit/{principleid}")]
        [HttpGet]
        public IActionResult Edit(Guid principleid)
        {
            try {
                if (principleid != null) {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;

                    var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
                    ViewBag.FacultyType = FacultyType;

                    var principledetails = _applicationContext.Person.Find(principleid);
                    var principlePhone = _applicationContext.ContactDetails.Where(p => p.PartyId == principleid && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var principleEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == principleid && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var principleEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == principleid).FirstOrDefault();
                    var principlePerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principleid && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var principlePreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principleid && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince;
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                  //  var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == principlePerAddress.DistrictId);
                   // ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince;
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    //var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == principlePreAddress.DistrictId);
                    //ViewBag.PrevillageNahia = PrevillageNahia;

                    PrincipleEditViewModel principle = new PrincipleEditViewModel
                    {
                        PersonId = principledetails.PersonId,
                        Name = principledetails.Name,
                        LastName = principledetails.LastName,
                        FatherName = principledetails.FatherName,
                        GrandFatherName = principledetails.GrandFatherName,
                        Nidnumber = principledetails.Nidnumber,
                        Age = principledetails.Age,
                        GenderTypeId = principledetails.GenderTypeId,
                        Eduservice=principledetails.Eduservice,
                        PhonNumber = principlePhone.Value,
                        Email = principleEmail.Value,
                        EducationLevelID = principleEducation.EducationLevelId,
                        FacultyTypeId=principleEducation.FacultyTypeId,
                        GraduationDate= principleEducation.GraduationDate,
                        PerProvinceId = principlePerAddress.ProvinceId,
                        PerDistrictId = principlePerAddress.DistrictId,
                        PerNahia = principlePerAddress.Nahia,
                        PreProvinceId = principlePreAddress.ProvinceId,
                        PreDistrictId = principlePreAddress.DistrictId,
                        PreNahia = principlePreAddress.Nahia,                   
                        ExistingPhotoPath = principledetails.Photo,
                      
                    };
                    HttpContext.Session.SetString("FounderID", principledetails.PersonId.ToString());
                   
                    HttpContext.Session.SetString("Principle", "Create");
                    return View(principle);
                }
                else {
                    return View();

                }
               

            } catch (Exception ex) {

                return View("Index");
            }
           
        }

        [Route("Edit/{principleid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(PrincipleEditViewModel principle)
        {

            try {

                if (ModelState.IsValid)
                {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;

                    var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
                    ViewBag.FacultyType = FacultyType;

                    var foundedetails = _applicationContext.Person.Find(principle.PersonId);
                    var founderPhone = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    var founderEmail = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    var founderEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == principle.PersonId).FirstOrDefault();
                    var founderPerAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    var founderPreAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                    var Perprovince = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perprovince = Perprovince;
                    var Perdistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPerAddress.ProvinceId);
                    ViewBag.Perdistrict = Perdistrict;
                   // var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                   // ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    //var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                    //ViewBag.PrevillageNahia = PrevillageNahia;

                    Person person = _applicationContext.Person.Find(principle.PersonId);
                    person.Name = principle.Name;
                    person.LastName = principle.LastName;
                    person.FatherName = principle.FatherName;
                    person.GrandFatherName = principle.GrandFatherName;
                    person.Nidnumber = principle.Nidnumber;
                    person.Age = principle.Age;
                    person.GenderTypeId = principle.GenderTypeId;
                    person.Eduservice = principle.Eduservice;
                    person.UpdatedBy = _userManager.GetUserId(User);
                    person.UpdatedAt = DateTime.Now;


                    ContactDetails PhoneNumber = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    PhoneNumber.Value = principle.PhonNumber;
                    PhoneNumber.UpdatedBy = _userManager.GetUserId(User);
                    PhoneNumber.UpdatedAt = DateTime.Now;

                    ContactDetails Email = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    Email.Value = principle.Email;
                    Email.UpdatedBy = _userManager.GetUserId(User);
                    Email.UpdatedAt = DateTime.Now;

                    PersonEducation personEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == principle.PersonId).FirstOrDefault();
                    personEducation.EducationLevelId = principle.EducationLevelID;
                    personEducation.FacultyTypeId = principle.FacultyTypeId;
                    personEducation.GraduationDate = principle.GraduationDate;
                    personEducation.UpdatedBy = _userManager.GetUserId(User);
                    personEducation.UpdatedAt = DateTime.Now;

                    PartyAddress PermenantAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    PermenantAddress.ProvinceId = principle.PerProvinceId;
                    PermenantAddress.DistrictId = principle.PerDistrictId;
                    PermenantAddress.Nahia = principle.PerNahia;
                    PermenantAddress.UpdatedBy = _userManager.GetUserId(User);
                    PermenantAddress.UpdatedAt = DateTime.Now;

                    PartyAddress CurrentAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();
                    CurrentAddress.ProvinceId = principle.PreProvinceId;
                    CurrentAddress.DistrictId = principle.PreDistrictId;
                    CurrentAddress.Nahia = principle.PreNahia;
                    CurrentAddress.UpdatedBy = _userManager.GetUserId(User);
                    CurrentAddress.UpdatedAt = DateTime.Now;


                    string FilePath = "";

                    if (principle.Photo != null)
                    {
                        string[] _Extensions = new string[] { ".jpg", ".png", ".jpeg" };
                        int _maxFileSize = 500 * 1024;
                        var extension = Path.GetExtension(principle.Photo.FileName);
                        if (_Extensions.Contains(extension.ToLower()) && principle.Photo.Length < _maxFileSize)
                        {
                            string UploadsFolder = Path.Combine(_env.WebRootPath, "Person", principle.PersonId.ToString());
                            if (!Directory.Exists(Path.Combine(UploadsFolder)))
                            {
                                Directory.CreateDirectory(UploadsFolder);

                            }
                            string fileName = principle.PersonId.ToString() + "-Founder photo.jpeg";
                            FilePath = Path.Combine(UploadsFolder, fileName);
                            System.IO.File.Delete(FilePath);

                            using (var filestream = new FileStream(FilePath, FileMode.Create))
                            {
                                principle.Photo.CopyTo(filestream);
                            }
                            person.Photo = "/Person/"+ principle.PersonId.ToString()+"/" +fileName;
                        }
                        else
                        {
                            ViewBag.photoerror =_localizer["PhotoError"].Value;
                            return View(principle);
                           
                        }
                    }



                    _applicationContext.Update(person);
                    _applicationContext.Update(PhoneNumber);
                    _applicationContext.Update(Email);
                    _applicationContext.Update(personEducation);
                    _applicationContext.Update(PermenantAddress);
                    _applicationContext.Update(CurrentAddress);
                    await _applicationContext.SaveChangesAsync().ConfigureAwait(true);
                    notyfService.Custom(_localizer["PrincipleUpdated"].Value, 10, "#67757c", "fa fa-check");
                    ViewBag.Message = _localizer["RecordUpdated"].Value;
                    HttpContext.Session.SetString("FounderID", principle.PersonId.ToString());
                    return View(principle);

                }








                var EducationLev = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                ViewBag.EducationLevel = EducationLev;

                var GenderT= _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                ViewBag.GenderType = GenderT;

                var FacultyT = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
                ViewBag.FacultyType = FacultyT;

                var principlede = _applicationContext.Person.Find(principle.PersonId);
                var principlePh = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                var principleEm = _applicationContext.ContactDetails.Where(p => p.PartyId == principle.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                var principleEduc = _applicationContext.PersonEducation.Where(p => p.PersonId == principle.PersonId).FirstOrDefault();
                var principlePerAdd = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                var principlePreAdd = _applicationContext.PartyAddress.Where(p => p.PartyId == principle.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();

                var Perprov = _applicationContext.ZProvince;
                ViewBag.Perprovince = Perprov;
                var Perdistr = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePerAdd.ProvinceId);
                ViewBag.Perdistrict = Perdistr;
                //  var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == principlePerAddress.DistrictId);
                // ViewBag.PervillageNahia = PervillageNahia;

                var Preprov = _applicationContext.ZProvince;
                ViewBag.Preprovince = Preprov;
                var Predistr = _applicationContext.ZDistrict.Where(d => d.ProvinceId == principlePreAdd.ProvinceId);
                ViewBag.Predistrict = Predistr;






                return View();
            } catch (Exception ex) {
                return View("index");
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

            var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
            ViewBag.FacultyType = FacultyType;


            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid == null)
            {
                return RedirectToAction("Navigate", "School");
            }
            var financialResource = HttpContext.Session.GetString("FinancialResource");
            if (financialResource == null)
            {
                return RedirectToAction("Navigate", "FinancialResource");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(PrincipleViewModel principle )
        {
            Guid DropPID = Guid.NewGuid();
            var province = _applicationContext.ZProvince;
            ViewBag.Province = province;

            var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
            ViewBag.EducationLevel = EducationLevel;

            var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.GenderType = GenderType;

            var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
            ViewBag.FacultyType = FacultyType;

            if (ModelState.IsValid)
            {
                string FilePath = "";
                string photo= "";
                Guid Pid = Guid.NewGuid();
                if (principle.Photo != null)
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
                        await principle.Photo.CopyToAsync(filestream);
                    }
                    photo = "/Person/" + Pid.ToString() + "/" + fileName;
                }

                Party party = new Party() { PartyId = Pid };
                var UserId = _userManager.GetUserId(User);
                var schoolId = Guid.Parse(HttpContext.Session.GetString("SchoolID"));
                var school = _applicationContext.School.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolId).FirstOrDefault();
                Person person = new Person()
                {
                    PersonId = Pid,
                    Name = principle.Name,
                    LastName = principle.LastName,
                    FatherName = principle.FatherName,
                    GrandFatherName = principle.GrandFatherName,
                    Nidnumber = principle.Nidnumber,
                    Age = principle.Age,
                    Photo = photo,
                    GenderTypeId = principle.GenderTypeId,
                    Eduservice=principle.Eduservice,
                    PartyRoleTypeId = Guid.Parse("FD744DE3-476C-428F-B7E0-381A9DD286FE"),
                    SchoolId=school.SchoolId,
                    CreatedBy=_userManager.GetUserId(User),
                    CreatedAt= DateTime.Now,
                };

                Guid PhoneCid = Guid.NewGuid();
                ContactDetails PhoneNumber = new ContactDetails()
                {
                    ContactDetailId = PhoneCid,
                    PartyId = Pid,
                    ContactMechanismTypeId = Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB"),
                    Value = principle.PhonNumber ,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid EmailCid = Guid.NewGuid();
                ContactDetails Email = new ContactDetails()
                {
                    ContactDetailId = EmailCid,
                    PartyId = Pid,
                    ContactMechanismTypeId = Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647"),
                    Value = principle.Email,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid personEductionID = Guid.NewGuid();
                PersonEducation personEducation = new PersonEducation()
                {
                    PersonId = Pid,
                    PersonEducationId = personEductionID,
                    EducationLevelId = principle.EducationLevelID,
                    FacultyTypeId=principle.FacultyTypeId,
                    GraduationDate=principle.GraduationDate,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };

                Guid PerAaddressId = Guid.NewGuid();
                PartyAddress PermenantAddress = new PartyAddress()
                {
                    PartyAddressId = PerAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7"),
                    ProvinceId = principle.PerProvinceId,
                    DistrictId = principle.PerDistrictId,
                    Nahia = principle.PerNahia,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now
                };
                Guid CurrAaddressId = Guid.NewGuid();
                PartyAddress CurrentAddress = new PartyAddress()
                {
                    PartyAddressId = CurrAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52"),
                    ProvinceId = principle.PreProvinceId,
                    DistrictId = principle.PreDistrictId,
                    Nahia = principle.PreNahia,
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
                await _applicationContext.SaveChangesAsync().ConfigureAwait(true);
                HttpContext.Session.SetString("principleid", Pid.ToString());
                notyfService.Custom(_localizer["PrincipleCreated"].Value, 10, "#67757c", "fa fa-check");

                HttpContext.Session.SetString("Principle", "Create");

                ViewBag.Message = _localizer["RecordSaved"].Value;
                //   return RedirectToAction("Edit", new { principleid = Pid });
                return RedirectToAction("Navigate", "Teacher");
            }

            var prov = _applicationContext.ZProvince;
            ViewBag.Province = prov;

            var EducationL = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
            ViewBag.EducationLevel = EducationL;

            var GenderT = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.GenderType = GenderT;

            var FacultyT = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
            ViewBag.FacultyType = FacultyT;
            return View("Create"); 
           
        }
    }
}