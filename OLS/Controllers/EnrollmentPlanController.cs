using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;
using OLS.ViewModels;

namespace OLS.Controllers
{
    [Authorize]
    public class EnrollmentPlanController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public EnrollmentPlanController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
        }
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

            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year).Min();
            var studentEnrollmentPlan = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == UserId && p.Year == year);

            if (studentEnrollmentPlan.Count() != 0)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit");
                        }
                    }
                    return RedirectToAction("NoEdit");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
              
            }


            return RedirectToAction("Create");
        }
        public IActionResult NavigateNextY()
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

            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year);
            var studentEnrollmentPlan = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == UserId).Select(p =>p.Year).Distinct().ToList();
            if (studentEnrollmentPlan.Count() >1)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("EditNextY");
                        }
                    }
                    return RedirectToAction("NoEditNextY");
                }
                else
                {
                    return RedirectToAction("EditNextY");
                }
                
            }


            return RedirectToAction("CreateNextY");
        }

        [HttpPost]
        public IActionResult Edit(IList<EnrollmentPlanEditViewModel> studentEnrollmentPlan)
        {
            if (ModelState.IsValid)
            {

                IList<StudentEnrollmentPlan> enrollmentPlans = new List<StudentEnrollmentPlan>();
                
                for (int i = 0; i < studentEnrollmentPlan.Count; i++)
                {
                    StudentEnrollmentPlan enrollmentPlan = _applicationContext.StudentEnrollmentPlan.Find(studentEnrollmentPlan[i].Id);
                    enrollmentPlan.NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudents;
                    enrollmentPlan.UpdatedBy = _userManager.GetUserId(User);
                    enrollmentPlan.UpdatedAt = DateTime.Now;
                    enrollmentPlans.Add(enrollmentPlan);

                    
                }
                _applicationContext.UpdateRange(enrollmentPlans);
                _applicationContext.SaveChanges();
                ViewBag.Message = "معلومات موفقانه تصحیح گردید / معلومات په بریالیتوب سره اصلاح شول / Record Successfully updated ";
                return RedirectToAction("Edit");
                
            }
            return View();
        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year).Min();
            var displayPlan = (from studentEnrollmentPlan in _applicationContext.StudentEnrollmentPlan
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on studentEnrollmentPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where studentEnrollmentPlan.SchoolId == schoolId && studentEnrollmentPlan.Year== year
                               orderby schoolSubLevel.OrderNumber, studentEnrollmentPlan.GenderTypeId
                               select new EnrollmentPlanEditViewModel
                               {
                                   Id=studentEnrollmentPlan.Id,                                
                                    NumberOfStudents=studentEnrollmentPlan.NumberOfStudents,
                                    SchoolSubLevelName=schoolSubLevel.SubLevelNameDari + "/"+schoolSubLevel.SubLevelNamePashto+ "/"+schoolSubLevel.SubLevelName,
                                   GenderTypeId=studentEnrollmentPlan.GenderTypeId


                               }).ToList();

            return View(displayPlan);
        }
        [Route("NoEdit")]
        [HttpGet]
        public IActionResult NoEdit()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year).Min();
            var displayPlan = (from studentEnrollmentPlan in _applicationContext.StudentEnrollmentPlan
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on studentEnrollmentPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where studentEnrollmentPlan.SchoolId == schoolId && studentEnrollmentPlan.Year == year
                               orderby schoolSubLevel.OrderNumber, studentEnrollmentPlan.GenderTypeId
                               select new EnrollmentPlanEditViewModel
                               {
                                   Id = studentEnrollmentPlan.Id,
                                   NumberOfStudents = studentEnrollmentPlan.NumberOfStudents,
                                   SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   GenderTypeId = studentEnrollmentPlan.GenderTypeId


                               }).ToList();

            return View(displayPlan);
        }
        public IActionResult Create()
        {
            var schoolLevelid = _applicationContext.School.Where(p => p.CreatedBy==_userManager.GetUserId(User)).Select(p => p.SchoolLevelId).FirstOrDefault();

            var displayPlan = (from schoolLevel in _applicationContext.ZSchoolLevel
                                                  join schooLevelSubLevel in _applicationContext.ZSchoolLevelSubLevel on schoolLevel.SchoolLevelId equals schooLevelSubLevel.SchoolLevelId
                                                  join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schooLevelSubLevel.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                                                  where schoolLevel.SchoolLevelId == schoolLevelid
                                                  orderby schoolSubLevel.OrderNumber
                                                  select new
                                                  {
                                                      SubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                                      SubLevelId = schoolSubLevel.SchoolSubLevelId,

                                                  }).ToList().Select(x => new EnrollmentPlanViewModel()
                                                  {
                                                      
                                                      
                                                      SchoolSubLevelName = x.SubLevelName,
                                                      SchoolSubLevelId = x.SubLevelId,
                                                   
                                                  }).ToList();




         
            return View(displayPlan);
        }
        [HttpPost]
        public IActionResult Create(IList<EnrollmentPlanViewModel> studentEnrollmentPlan)
        {
            if (ModelState.IsValid) {

                IList<StudentEnrollmentPlan> planMales = new List<StudentEnrollmentPlan>();
                IList<StudentEnrollmentPlan> planFemales = new List<StudentEnrollmentPlan>();
                for (int i = 0; i < studentEnrollmentPlan.Count; i++)
                    {


                    StudentEnrollmentPlan planMale = new StudentEnrollmentPlan {
                        Id = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                        Year = DateTime.Now.Year.ToString(),
                        GenderTypeId = Guid.Parse("E575CE44-2BBE-4175-A334-CE5ABC3CDDDA"),
                        SchoolSubLevelId = studentEnrollmentPlan[i].SchoolSubLevelId,
                        NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudentsMale,
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                                };
                        planMales.Add(planMale);
                        StudentEnrollmentPlan planFemale = new StudentEnrollmentPlan
                        {
                            Id = Guid.NewGuid(),
                            SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                            Year = DateTime.Now.Year.ToString(),
                            GenderTypeId = Guid.Parse("A10E9D59-C1B9-4983-97A0-F0A97A85F71D"),
                            SchoolSubLevelId = studentEnrollmentPlan[i].SchoolSubLevelId,
                            NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudentsFemale,
                            CreatedBy = _userManager.GetUserId(User),
                            CreatedAt = DateTime.Now,
                        };
                            planFemales.Add(planFemale);
                            }
                    _applicationContext.AddRange(planMales);
                    _applicationContext.AddRange(planFemales);
                _applicationContext.SaveChanges();

            }
            var schoolLevelid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolLevelId).FirstOrDefault();

            var displayPlan = (from schoolLevel in _applicationContext.ZSchoolLevel
                               join schooLevelSubLevel in _applicationContext.ZSchoolLevelSubLevel on schoolLevel.SchoolLevelId equals schooLevelSubLevel.SchoolLevelId
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schooLevelSubLevel.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where schoolLevel.SchoolLevelId == schoolLevelid
                               orderby schoolSubLevel.OrderNumber
                               select new
                               {
                                   SubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   SubLevelId = schoolSubLevel.SchoolSubLevelId,
                                   

                               }).ToList()
                                         .Select(x => new TheList()
                                         {
                                             SubLevelName = x.SubLevelName,
                                             SubLevelId = x.SubLevelId,
                                         });


            ViewBag.displaylist = displayPlan;
            ViewBag.Message = "sucessfully";
            return RedirectToAction("Edit");
        }

        //Next year section start
        [HttpPost]
        public IActionResult EditNextY(IList<EnrollmentPlanEditViewModel> studentEnrollmentPlan)
        {
            if (ModelState.IsValid)
            {

                IList<StudentEnrollmentPlan> enrollmentPlans = new List<StudentEnrollmentPlan>();

                for (int i = 0; i < studentEnrollmentPlan.Count; i++)
                {
                    StudentEnrollmentPlan enrollmentPlan = _applicationContext.StudentEnrollmentPlan.Find(studentEnrollmentPlan[i].Id);
                    enrollmentPlan.NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudents;
                    enrollmentPlan.UpdatedBy = _userManager.GetUserId(User);
                    enrollmentPlan.UpdatedAt = DateTime.Now;
                    enrollmentPlans.Add(enrollmentPlan);


                }

                _applicationContext.UpdateRange(enrollmentPlans);
                _applicationContext.SaveChanges();
                ViewBag.Message = "معلومات تصحیح گردید";
                return RedirectToAction("NavigateNextY");
            }
            return View();
        }
        [Route("NoEditNextY")]
        [HttpGet]
        public IActionResult NoEditNextY()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year).Max();

            var displayPlan = (from studentEnrollmentPlan in _applicationContext.StudentEnrollmentPlan
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on studentEnrollmentPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where studentEnrollmentPlan.SchoolId == schoolId && studentEnrollmentPlan.Year == year
                               orderby schoolSubLevel.OrderNumber, studentEnrollmentPlan.GenderTypeId
                               select new EnrollmentPlanEditViewModel
                               {
                                   Id = studentEnrollmentPlan.Id,
                                   NumberOfStudents = studentEnrollmentPlan.NumberOfStudents,
                                   SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   GenderTypeId = studentEnrollmentPlan.GenderTypeId


                               }).ToList();
            return View(displayPlan);
        }


        public IActionResult EditNextY()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            var year = _applicationContext.StudentEnrollmentPlan.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.Year).Max();

            var displayPlan = (from studentEnrollmentPlan in _applicationContext.StudentEnrollmentPlan
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on studentEnrollmentPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where studentEnrollmentPlan.SchoolId == schoolId && studentEnrollmentPlan.Year == year
                               orderby schoolSubLevel.OrderNumber, studentEnrollmentPlan.GenderTypeId
                               select new EnrollmentPlanEditViewModel
                               {
                                   Id = studentEnrollmentPlan.Id,
                                   NumberOfStudents = studentEnrollmentPlan.NumberOfStudents,
                                   SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   GenderTypeId = studentEnrollmentPlan.GenderTypeId


                               }).ToList();
            return View(displayPlan);
        }
        public IActionResult CreateNextY()
        {
            var schoolLevelid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolLevelId).FirstOrDefault();

            var displayPlan = (from schoolLevel in _applicationContext.ZSchoolLevel
                               join schooLevelSubLevel in _applicationContext.ZSchoolLevelSubLevel on schoolLevel.SchoolLevelId equals schooLevelSubLevel.SchoolLevelId
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schooLevelSubLevel.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where schoolLevel.SchoolLevelId == schoolLevelid
                               orderby schoolSubLevel.OrderNumber
                               select new
                               {
                                   SubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   SubLevelId = schoolSubLevel.SchoolSubLevelId,

                               }).ToList().Select(x => new EnrollmentPlanViewModel()
                               {


                                   SchoolSubLevelName = x.SubLevelName,
                                   SchoolSubLevelId = x.SubLevelId,

                               }).ToList();





            return View(displayPlan);
        }
        [HttpPost]
        public IActionResult CreateNextY(IList<EnrollmentPlanViewModel> studentEnrollmentPlan)
        {
            if (ModelState.IsValid)
            {

                IList<StudentEnrollmentPlan> planMales = new List<StudentEnrollmentPlan>();
                IList<StudentEnrollmentPlan> planFemales = new List<StudentEnrollmentPlan>();
                for (int i = 0; i < studentEnrollmentPlan.Count; i++)
                {


                    StudentEnrollmentPlan planMale = new StudentEnrollmentPlan
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                        Year = DateTime.Now.AddYears(1).Year.ToString(),
                        GenderTypeId = Guid.Parse("E575CE44-2BBE-4175-A334-CE5ABC3CDDDA"),
                        SchoolSubLevelId = studentEnrollmentPlan[i].SchoolSubLevelId,
                        NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudentsMale,
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                    };
                    planMales.Add(planMale);
                    StudentEnrollmentPlan planFemale = new StudentEnrollmentPlan
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                        Year = DateTime.Now.AddYears(1).Year.ToString(),
                        GenderTypeId = Guid.Parse("A10E9D59-C1B9-4983-97A0-F0A97A85F71D"),
                        SchoolSubLevelId = studentEnrollmentPlan[i].SchoolSubLevelId,
                        NumberOfStudents = studentEnrollmentPlan[i].NumberOfStudentsFemale,
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                    };
                    planFemales.Add(planFemale);
                }
                _applicationContext.AddRange(planMales);
                _applicationContext.AddRange(planFemales);
                _applicationContext.SaveChanges();

            }
            var schoolLevelid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolLevelId).FirstOrDefault();

            var displayPlan = (from schoolLevel in _applicationContext.ZSchoolLevel
                               join schooLevelSubLevel in _applicationContext.ZSchoolLevelSubLevel on schoolLevel.SchoolLevelId equals schooLevelSubLevel.SchoolLevelId
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schooLevelSubLevel.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where schoolLevel.SchoolLevelId == schoolLevelid
                               orderby schoolSubLevel.OrderNumber
                               select new
                               {
                                   SubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   SubLevelId = schoolSubLevel.SchoolSubLevelId,


                               }).ToList()
                                         .Select(x => new TheList()
                                         {
                                             SubLevelName = x.SubLevelName,
                                             SubLevelId = x.SubLevelId,
                                         });


            ViewBag.displaylist = displayPlan;
            ViewBag.Message = "sucessfully";
            return RedirectToAction("NavigateNextY");
        }
        //Next year section end
    }
}