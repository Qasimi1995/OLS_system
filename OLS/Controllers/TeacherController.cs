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
    [Route("Teacher")]
    [Authorize]
    public class TeacherController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public TeacherController(ApplicationContext applicationContext,IHostingEnvironment environment,UserManager<User> userManager)
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
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();

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

            if (displayPlan.Count > 0)
            {
                for (int i = 0; i < displayPlan.Count; i++)
                {
                    if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                    else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                    else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                    else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                    else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                    else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                    {

                        return RedirectToAction("GetTeachersList");
                    }
                }
                return RedirectToAction("NoGetTeachersList");
            }
            else
            {
                return RedirectToAction("GetTeachersList");
            }
            
        }

        [AcceptVerbs("Get", "Post")]
        [Route("IsNIDUnique")]
        public async Task<IActionResult> IsNIDUnique(string NIDNumber)
        {

            var teacher = _applicationContext.Person.Where(p => p.Nidnumber == NIDNumber).FirstOrDefault();
            if (teacher == null)
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

            else if (NIDCountOther.Count >0) {
                return Json($"Tazkira number {NIDNumber} already in use");

            }
            else
            {
                return Json(true);
            }

        }

        [Route("NoGetTeachersList")]
        [HttpGet]
        public IActionResult NoGetTeachersList()
        {

            var teacherList = (from person in _applicationContext.Person
                               join personEducation in _applicationContext.PersonEducation
                               on person.PersonId equals personEducation.PersonId
                               join zEducation in _applicationContext.ZEducationLevel on personEducation.EducationLevelId equals zEducation.EducationLevelId
                               join zGender in _applicationContext.ZGenderType on person.GenderTypeId equals zGender.GenderTypeId
                               join zfaculty in _applicationContext.ZFacultyType on personEducation.FacultyTypeId equals zfaculty.FacultyTypeId
                               where person.PartyRoleTypeId == Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D")
                               && person.CreatedBy == _userManager.GetUserId(User)
                               select new
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
                               }).ToList()
                               .Select(x => new TeacherViewModelDisplay()
                               {
                                   PersonId = x.PersonId,
                                   Name = x.Name,
                                   LastName = x.LastName,
                                   FatherName = x.FatherName,
                                   GrandFatherName = x.GrandFatherName,
                                   Nidnumber = x.Nidnumber,
                                   Age = x.Age,
                                   GenderType = x.GenderType,
                                   Eduservice = x.Eduservice,
                                   EducationLevel = x.EducationLevel,
                                   FacultyType = x.FacultyType,
                                   GraduationDate = x.GraduationDate,
                               });

            return View(teacherList);
        }


        [Route("GetTeachersList")]
        [HttpGet]
        public IActionResult GetTeachersList() {

            var teacherList = (from person in _applicationContext.Person
                               join personEducation in _applicationContext.PersonEducation                              
                               on person.PersonId equals personEducation.PersonId
                               join zEducation in _applicationContext.ZEducationLevel on personEducation.EducationLevelId equals zEducation.EducationLevelId
                               join zGender in _applicationContext.ZGenderType on person.GenderTypeId equals zGender.GenderTypeId
                               join zfaculty in _applicationContext.ZFacultyType on personEducation.FacultyTypeId equals zfaculty.FacultyTypeId
                               where person.PartyRoleTypeId==Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D") 
                               && person.CreatedBy==_userManager.GetUserId(User)
                               select new
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
                               }).ToList()
                               .Select(x => new TeacherViewModelDisplay() {
                                   PersonId = x.PersonId,
                                   Name = x.Name,
                                   LastName = x.LastName,
                                   FatherName = x.FatherName,
                                   GrandFatherName = x.GrandFatherName,
                                   Nidnumber = x.Nidnumber,
                                   Age = x.Age,
                                   GenderType = x.GenderType,
                                   Eduservice = x.Eduservice,
                                   EducationLevel = x.EducationLevel,
                                   FacultyType = x.FacultyType,
                                   GraduationDate = x.GraduationDate,
                               });




            return View( teacherList);
        }


        [Route("Delete/{teacherid}")]
        [HttpGet]
        public IActionResult Delete(Guid teacherid)
        {

            if (teacherid != null)
            {
                var teacherEducation = _applicationContext.PersonEducation.Where(p => p.PersonId==teacherid).FirstOrDefault();
                var teacher = _applicationContext.Person.Find(teacherid);
                _applicationContext.Remove(teacherEducation);
                _applicationContext.Remove(teacher);
                _applicationContext.SaveChanges();
                return RedirectToAction("GetTeachersList");

            }
            return View();

        }
        [Route("Edit/{teacherid}")]
        [HttpGet]
        public IActionResult Edit(Guid teacherid)
        {
            try {
                if (teacherid != null) {
                    Guid DropPID = Guid.NewGuid();

                    var EducationLevel = _applicationContext.ZEducationLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.EducationLevel = EducationLevel;

                    var GenderType = _applicationContext.ZGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.GenderType = GenderType;

                    var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
                    ViewBag.FacultyType = FacultyType;

                    var teacherdetails = _applicationContext.Person.Find(teacherid);
                    var teacherEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == teacherid).FirstOrDefault();

                   var teacherList = _applicationContext.Person.Where(p => p.PartyRoleTypeId == Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D")).ToList();

                    ViewBag.teacherList = teacherList;

                    TeacherEditViewModel teacher = new TeacherEditViewModel
                    {
                        PersonId             = teacherdetails.PersonId,
                        Name             = teacherdetails.Name,
                        LastName            = teacherdetails.LastName,
                        FatherName           = teacherdetails.FatherName,
                        GrandFatherName          = teacherdetails.GrandFatherName,
                        Nidnumber               = teacherdetails.Nidnumber,
                        Age                      = teacherdetails.Age,
                        GenderTypeId                = teacherdetails.GenderTypeId,
                        Eduservice=             teacherdetails.Eduservice,                     
                        EducationLevelID            = teacherEducation.EducationLevelId,
                        FacultyTypeId       = teacherEducation.FacultyTypeId,
                        GraduationDate       =      teacherEducation.GraduationDate,
                
                      
                    };
                    HttpContext.Session.SetString("teacherid", teacherdetails.PersonId.ToString());
                    return View(teacher);
                }
                else {
                    return View("index");

                }
               

            } catch (Exception ex) {

                return View("Index");
            }
           
        }

        [Route("Edit/{teacherid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(TeacherEditViewModel teacher)
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

                    var teacherdetails = _applicationContext.Person.Find(teacher.PersonId);
                    var teacherEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == teacher.PersonId).FirstOrDefault();
 
                    Person person = _applicationContext.Person.Find(teacher.PersonId);
                    person.Name = teacher.Name;
                    person.LastName = teacher.LastName;
                    person.FatherName = teacher.FatherName;
                    person.GrandFatherName = teacher.GrandFatherName;
                    person.Nidnumber = teacher.Nidnumber;
                    person.Age = teacher.Age;
                    person.GenderTypeId = teacher.GenderTypeId;
                    person.Eduservice = teacher.Eduservice;
                    person.UpdatedBy = _userManager.GetUserId(User);
                    person.UpdatedAt = DateTime.Now;                  

                    PersonEducation personEducation = _applicationContext.PersonEducation.Where(p => p.PersonId == teacher.PersonId).FirstOrDefault();
                    personEducation.EducationLevelId = teacher.EducationLevelID;
                    personEducation.FacultyTypeId = teacher.FacultyTypeId;
                    personEducation.GraduationDate = teacher.GraduationDate;

                    _applicationContext.Update(person);                
                    _applicationContext.Update(personEducation);               
                    await _applicationContext.SaveChangesAsync();
                    ViewBag.Message = "معلومات ثبت گردید";
                    HttpContext.Session.SetString("teacherID", teacher.PersonId.ToString());
                    return View(teacher);

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

            var FacultyType = _applicationContext.ZFacultyType.OrderBy(o => o.OrderNumber);
            ViewBag.FacultyType = FacultyType;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(TeacherViewModel teacher )
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
                Guid Pid = Guid.NewGuid();
                Party party = new Party() { PartyId = Pid };
                var UserId = _userManager.GetUserId(User);
                var school = _applicationContext.School.Where(p => p.CreatedBy == UserId).FirstOrDefault();
                Person person = new Person()
                {
                    PersonId = Pid,
                    Name = teacher.Name,
                    LastName = teacher.LastName,
                    FatherName = teacher.FatherName,
                    GrandFatherName = teacher.GrandFatherName,
                    Nidnumber = teacher.Nidnumber,
                    Age = teacher.Age,                
                    GenderTypeId = teacher.GenderTypeId,
                    Eduservice=teacher.Eduservice,
                    PartyRoleTypeId = Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D"),
                    SchoolId=school.SchoolId,
                    CreatedBy=_userManager.GetUserId(User),
                    CreatedAt= DateTime.Now,
                };
                Guid personEductionID = Guid.NewGuid();
                PersonEducation personEducation = new PersonEducation()
                {
                    PersonId = Pid,
                    PersonEducationId = personEductionID,
                    EducationLevelId = teacher.EducationLevelID,
                    FacultyTypeId=teacher.FacultyTypeId,
                    GraduationDate=teacher.GraduationDate,
                };             
                _applicationContext.Add(party);
                _applicationContext.Add(person);               
                _applicationContext.Add(personEducation);              
                await _applicationContext.SaveChangesAsync();
                HttpContext.Session.SetString("teacherid", Pid.ToString());

                ViewBag.Message = "Created Sucessfully";

                return RedirectToAction("GetTeachersList");
            }
            return View("GetTeachersList"); 
           
        }
    }
}