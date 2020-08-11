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

namespace OLS.Controllers
{
    [Route("Founder")]
    [Authorize]
    public class FounderController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public FounderController(ApplicationContext applicationContext,IHostingEnvironment environment,UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
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

            var founder = _applicationContext.Person.Where(p => p.CreatedBy == UserId && p.PartyRoleTypeId == Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236")).FirstOrDefault();
            if (founder != null)
            {
                return RedirectToAction("Edit", new { founderid = founder.PersonId });
            }


            return RedirectToAction("Create");

        }

        [Route("FindDistrict/{ProvinceId}")]
        public IActionResult FindDistrict(Guid ProvinceId)
        {
            var districts = _applicationContext.ZDistrict.Where(district => district.ProvinceId == ProvinceId).Select(distict => new { 
            Id=distict.DistrictId,
            DistNameEng=distict.DistNaEng,
            DistNameDar=distict.DistNaDar
            
            }).ToList();
            return new JsonResult(districts);
        }
        [Route("FindVillagNahia/{DistrictId}")]
        public IActionResult FindVillagNahia(Guid DistrictId)
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
                return Json($"Email {email} already in use");
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

                return Json($"Phone number {Email} already in use");
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

                return Json($"Phone number must be 10 digits");
            }
            if (founder == null)
            {

                return Json(true);
            }
            else
            {
                return Json($"Phone number {PhonNumber} already in use");
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

                return Json($"Phone number must be 10 digits");
            }
            if (PhoneCount.Count == 1)
            {

                return Json(true);
            }
            else if (PhoneCountOther.Count>0) {
                return Json($"Phone number {PhonNumber} already in use");

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
                return Json($"Tazkira number {NIDNumber} already in use");
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
                return Json($"Tazkira number {NIDNumber} already in use");

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
                    var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                    ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince;
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                    ViewBag.PrevillageNahia = PrevillageNahia;

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
                        PerVillageNahiaId = founderPerAddress.VillageNahiaId,
                        PreProvinceId = founderPreAddress.ProvinceId,
                        PreDistrictId = founderPreAddress.DistrictId,
                        PreVillageNahiaId = founderPreAddress.VillageNahiaId,                   
                        ExistingPhotoPath = foundedetails.Photo,
                        SchoolId = foundedetails.SchoolId
                    };
                    HttpContext.Session.SetString("FounderID", foundedetails.PersonId.ToString());
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
                    var PervillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPerAddress.DistrictId);
                    ViewBag.PervillageNahia = PervillageNahia;

                    var Preprovince = _applicationContext.ZProvince.Where(p => p.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Preprovince = Preprovince;
                    var Predistrict = _applicationContext.ZDistrict.Where(d => d.ProvinceId == founderPreAddress.ProvinceId);
                    ViewBag.Predistrict = Predistrict;
                    var PrevillageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == founderPreAddress.DistrictId);
                    ViewBag.PrevillageNahia = PrevillageNahia;

                    Person person = _applicationContext.Person.Find(founderModel.PersonId);
                    person.Name = founderModel.Name;
                    person.LastName = founderModel.LastName;
                    person.FatherName = founderModel.FatherName;
                    person.GrandFatherName = founderModel.GrandFatherName;
                    person.Nidnumber = founderModel.Nidnumber;
                    person.Age = founderModel.Age;
                    person.GenderTypeId = founderModel.GenderTypeId;
                    person.UpdatedBy = _userManager.GetUserId(User);
                    person.UpdatedAt = DateTime.Now;


                    ContactDetails PhoneNumber = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("B1B3DB1A-A3FB-43B9-839F-47A38C7F93CB")).FirstOrDefault();
                    PhoneNumber.Value = founderModel.PhonNumber;

                    ContactDetails Email = _applicationContext.ContactDetails.Where(p => p.PartyId == founderModel.PersonId && p.ContactMechanismTypeId == Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647")).FirstOrDefault();
                    Email.Value = founderModel.Email;

                    PersonEducation personEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == founderModel.PersonId).FirstOrDefault();
                    personEducation.EducationLevelId = founderModel.EducationLevelID;

                    PartyAddress PermenantAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7")).FirstOrDefault();
                    PermenantAddress.ProvinceId = founderModel.PerProvinceId;
                    PermenantAddress.DistrictId = founderModel.PerDistrictId;
                    PermenantAddress.VillageNahiaId = founderModel.PerVillageNahiaId;

                    PartyAddress CurrentAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == founderModel.PersonId && p.AddressTypeId == Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52")).FirstOrDefault();
                    CurrentAddress.ProvinceId = founderModel.PreProvinceId;
                    CurrentAddress.DistrictId = founderModel.PreDistrictId;
                    CurrentAddress.VillageNahiaId = founderModel.PreVillageNahiaId;


                    string FilePath = "";

                    if (founderModel.Photo != null)
                    {
                        string[] _Extensions = new string[] { ".jpg", ".png", ".jpeg" };
                        int _maxFileSize = 100 * 1024;
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

                            founderModel.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
                            person.Photo = "/Person/"+ founderModel.PersonId.ToString()+"/" +fileName;
                        }
                        else
                        {
                            ViewBag.photoerror = " only .jpg, png and jpeg format is allowed and max of 100 kb ";
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
                    ViewBag.Message = "معلومات ثبت گردید";
                    HttpContext.Session.SetString("FounderID", founderModel.PersonId.ToString());
                    return View(founderModel);

                }              
                return View("index");
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
                    await founder.Photo.CopyToAsync(new FileStream(FilePath, FileMode.Create));
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
                    Value = founder.PhonNumber
                };

                Guid EmailCid = Guid.NewGuid();
                ContactDetails Email = new ContactDetails()
                {
                    ContactDetailId = EmailCid,
                    PartyId = Pid,
                    ContactMechanismTypeId = Guid.Parse("1BE17772-A613-49A5-A67B-C1538DCBF647"),
                    Value = founder.Email
                };

                Guid personEductionID = Guid.NewGuid();
                PersonEducation personEducation = new PersonEducation()
                {
                    PersonId = Pid,
                    PersonEducationId = personEductionID,
                    EducationLevelId = founder.EducationLevelID,
                };

                Guid PerAaddressId = Guid.NewGuid();
                PartyAddress PermenantAddress = new PartyAddress()
                {
                    PartyAddressId = PerAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("EDDCDD48-67D0-4BAE-B96E-B7ACB5C87DF7"),
                    ProvinceId = founder.PerProvinceId,
                    DistrictId = founder.PerDistrictId,
                    VillageNahiaId = founder.PerVillageNahiaId
                };
                Guid CurrAaddressId = Guid.NewGuid();
                PartyAddress CurrentAddress = new PartyAddress()
                {
                    PartyAddressId = CurrAaddressId,
                    PartyId = Pid,
                    AddressTypeId = Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52"),
                    ProvinceId = founder.PreProvinceId,
                    DistrictId = founder.PreDistrictId,
                    VillageNahiaId = founder.PreVillageNahiaId
                };
                _applicationContext.Add(party);
                _applicationContext.Add(person);
                _applicationContext.Add(PhoneNumber);
                _applicationContext.Add(Email);
                _applicationContext.Add(personEducation);
                _applicationContext.Add(PermenantAddress);
                _applicationContext.Add(CurrentAddress);
                await _applicationContext.SaveChangesAsync();

                ViewBag.Message = "معلومات ثبت گردید";
                HttpContext.Session.SetString("FounderID", Pid.ToString());
               
                return RedirectToAction("Edit", new { founderid = Pid });
            }
            return View("Create"); 
           
        }
    }
}