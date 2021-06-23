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
using Microsoft.AspNetCore.Http;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class SchoolOtherExpensesController : Controller
    {
        private ApplicationContext _applicationContext;
        IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly INotyfService notyfService;
        private readonly IHtmlLocalizer _localizer;

        public SchoolOtherExpensesController(ApplicationContext applicationContext, IHtmlLocalizer<SchoolOtherExpensesController> localizer, IWebHostEnvironment environment, UserManager<User> userManager,
            INotyfService notyfService)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
            this.notyfService = notyfService;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecieveApplication()
        {
            var UserId = _userManager.GetUserId(User);

            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p => p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();

            //New Changes

            var re = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolId && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolId;



            var sch_id = HttpContext.Session.GetString("mySchoolId");
            if (sch_id != null)
            {
                schoolId = Guid.Parse(sch_id);
            }

            var newSchool = HttpContext.Session.GetString("new");
            if (newSchool != null)
            {
                schoolId = Guid.NewGuid();
            }

            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid != null)
            {
                schoolId = Guid.Parse(myschoolid);
            }


            if (re == null)
            {
                schoolId = re_schoolid;
            }







            var school = _applicationContext.School.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolId).ToList();
            IList<ProcessProgress> processProgresses = new List<ProcessProgress>();
            var subprocesses = _applicationContext.SubProcess.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).ToList();

            if (school.Count() != 0)
            {
                for (int i = 0; i < subprocesses.Count; i++)
                {
                    ProcessProgress processProgress = new ProcessProgress
                    {
                        ProcessProgressId = Guid.NewGuid(),
                        ProcessId = Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197"),
                        SubProcessId = subprocesses[i].SubProcessId,
                        SchoolId = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User) && p.SchoolId==schoolId).Select(p => p.SchoolId).FirstOrDefault(),
                        CreatedBy=_userManager.GetUserId(User)
                       
                    };
                    if (subprocesses[i].SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0"))
                    {
                        processProgress.ProcessStatusId = Guid.Parse("C42112A8-0C7B-4D4A-A93E-01B93F794420");
                        processProgress.UserId = UserId;
                        processProgress.StatusDate = DateTime.Now;
                        processProgress.UpdatedBy = _userManager.GetUserId(User);
                        processProgress.UpdatedAt = DateTime.Now;
                    }
                    processProgresses.Add(processProgress);


                }
    
                _applicationContext.AddRange(processProgresses);
               var result= _applicationContext.SaveChanges();

                HttpContext.Session.Clear();

                ViewBag.AppSubMessage = _localizer["ApplicationSubmitMessage"].Value;

                if (result ==processProgresses.Count)
                {
                    ViewBag.AppSubMessage = _localizer["ApplicationSubmitMessage"].Value;


                }
                else {
                    ViewBag.AppSubMessage = _localizer["ApplicationSubmitMessageFailed"].Value;

                     }
                return View();
            }
        

            return View();
        }

        [HttpGet]
        public IActionResult Navigate()
        {

            var UserId = _userManager.GetUserId(User);
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();



            var re = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolid;




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

            if (re == null)
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
            var schoolOtherExpenses = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolid);
            if (schoolOtherExpenses.Count() != 0)
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
                    return RedirectToAction("NoEditSOEC");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }

            return RedirectToAction("Create");
        }


        [HttpPost]
        public IActionResult Edit(IList<SchoolOtherExpensesViewModel> schoolOtherExpensesViewModels)
        {
            if (ModelState.IsValid)
            {

                IList<SchoolOtherExpenses> Plans = new List<SchoolOtherExpenses>();
                
                for (int i = 0; i < schoolOtherExpensesViewModels.Count; i++)
                {
                    SchoolOtherExpenses plan = _applicationContext.SchoolOtherExpenses.Find(schoolOtherExpensesViewModels[i].OtherExpenseId);
                    plan.ExpensePerMonth = schoolOtherExpensesViewModels[i].ExpensePerMonth;                          
                    plan.UpdatedBy = _userManager.GetUserId(User);
                    plan.UpdatedAt = DateTime.Now;
                    Plans.Add(plan);                    
                }


                _applicationContext.UpdateRange(Plans);
                _applicationContext.SaveChanges();
                notyfService.Custom(_localizer["SchoolOtherExpensesUpdated"].Value, 10, "#67757c", "fa fa-check");
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();



            var re = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolId && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolId;


            var sch_id = HttpContext.Session.GetString("mySchoolId");
            if (sch_id != null)
            {
                schoolId = Guid.Parse(sch_id);
            }

            var newSchool = HttpContext.Session.GetString("new");
            if (newSchool != null)
            {
                schoolId = Guid.NewGuid();
            }

            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid != null)
            {
                schoolId = Guid.Parse(myschoolid);
            }


            //if (re == null)
            //{
            //    schoolId = re_schoolid;
            //}





            var displayPlan = (from zOtherExpenseType in _applicationContext.ZOtherExpenseType
                               join schoolOtherExpenses in _applicationContext.SchoolOtherExpenses on zOtherExpenseType.OtherExpenseTypeId equals schoolOtherExpenses.OtherExpenseTypeId                              
                               where schoolOtherExpenses.SchoolId == schoolId
                               orderby zOtherExpenseType.OrderNumber
                               select new SchoolOtherExpensesViewModel
                               {
                                   OtherExpenseId= schoolOtherExpenses.OtherExpenseId,
                                   OtherExpenseTypeName = zOtherExpenseType.OtherExpenseTypeNameDari + "/" + zOtherExpenseType.OtherExpenseTypeNamePashto + "/" + zOtherExpenseType.OtherExpenseTypeName,
                                   ExpensePerMonth= schoolOtherExpenses.ExpensePerMonth,
                               
                               }).ToList();

            var IsSubmitted = _applicationContext.ProcessProgress.Where(p =>
            p.UserId == _userManager.GetUserId(User) && p.SchoolId==schoolId && p.ProcessStatusId== Guid.Parse("C42112A8-0C7B-4D4A-A93E-01B93F794420")).Select(p => new ProcessProgress {
            SchoolId=p.SchoolId,
            
            }).FirstOrDefault();
            ViewBag.AppSubmitButton = IsSubmitted;
            return View(displayPlan);
        }

        [HttpGet]
        [Route("NoEditSOEC")]
        public IActionResult NoEditSOEC()
        {
            var schoolId = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();


            var re = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolId && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolId;


            var sch_id = HttpContext.Session.GetString("mySchoolId");
            if (sch_id != null)
            {
                schoolId = Guid.Parse(sch_id);
            }

            var newSchool = HttpContext.Session.GetString("new");
            if (newSchool != null)
            {
                schoolId = Guid.NewGuid();
            }

            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid != null)
            {
                schoolId = Guid.Parse(myschoolid);
            }

            //if (re == null)
            //{
            //    schoolId = re_schoolid;
            //}



            var displayPlan = (from zOtherExpenseType in _applicationContext.ZOtherExpenseType
                               join schoolOtherExpenses in _applicationContext.SchoolOtherExpenses on zOtherExpenseType.OtherExpenseTypeId equals schoolOtherExpenses.OtherExpenseTypeId
                               where schoolOtherExpenses.SchoolId == schoolId
                               orderby zOtherExpenseType.OrderNumber
                               select new SchoolOtherExpensesViewModel
                               {
                                   OtherExpenseId = schoolOtherExpenses.OtherExpenseId,
                                   OtherExpenseTypeName = zOtherExpenseType.OtherExpenseTypeNameDari + "/" + zOtherExpenseType.OtherExpenseTypeNamePashto + "/" + zOtherExpenseType.OtherExpenseTypeName,
                                   ExpensePerMonth = schoolOtherExpenses.ExpensePerMonth,

                               }).ToList();

            var IsSubmitted = _applicationContext.ProcessProgress.Where(p =>
            p.UserId == _userManager.GetUserId(User) && p.SchoolId==schoolId && p.ProcessStatusId == Guid.Parse("C42112A8-0C7B-4D4A-A93E-01B93F794420")).Select(p => new ProcessProgress
            {
                SchoolId = p.SchoolId,

            }).FirstOrDefault();
            ViewBag.AppSubmitButton = IsSubmitted;
            return View(displayPlan);
        }
        public IActionResult Create()
        {



            var sch_id = HttpContext.Session.GetString("mySchoolId");




            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid == null)
            {
                return RedirectToAction("Navigate", "School");
            }

            var schoolStaffExpenses= HttpContext.Session.GetString("SchoolStaffExpenses");
            if (schoolStaffExpenses == null)
            {
                return RedirectToAction("Navigate", "SchoolStaffExpenses");
            }




            var displayPlan = (from zOtherExpenseType in _applicationContext.ZOtherExpenseType
                               orderby zOtherExpenseType.OrderNumber
                               select new
                               {
                                   OtherExpenseTypeName = zOtherExpenseType.OtherExpenseTypeNameDari + "/" + zOtherExpenseType.OtherExpenseTypeNamePashto + "/" + zOtherExpenseType.OtherExpenseTypeName,
                                   OtherExpenseTypeId = zOtherExpenseType.OtherExpenseTypeId,


                               }).ToList().Select(x => new SchoolOtherExpensesViewModel()
                               {

                                   OtherExpenseTypeName = x.OtherExpenseTypeName,
                                   OtherExpenseTypeId = x.OtherExpenseTypeId,

                               }).ToList();
            return View(displayPlan);
        }
        [HttpPost]
        public IActionResult Create(IList<SchoolOtherExpensesViewModel> schoolOtherExpensesViewModels)
        {
            var schoolid = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();


            var re = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
            var re_schoolid = schoolid;

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

            //if (re == null)
            //{
            //    schoolid = re_schoolid;
            //}


            var displayPlan = (from zOtherExpenseType in _applicationContext.ZOtherExpenseType
                               orderby zOtherExpenseType.OrderNumber
                               select new
                               {
                                   OtherExpenseTypeName = zOtherExpenseType.OtherExpenseTypeNameDari + "/" + zOtherExpenseType.OtherExpenseTypeNamePashto + "/" + zOtherExpenseType.OtherExpenseTypeName,
                                   OtherExpenseTypeId = zOtherExpenseType.OtherExpenseTypeId,


                               }).ToList().Select(x => new SchoolOtherExpensesViewModel()
                               {

                                   OtherExpenseTypeName = x.OtherExpenseTypeName,
                                   OtherExpenseTypeId = x.OtherExpenseTypeId,

                               }).ToList();

            if (ModelState.IsValid) {

                IList<SchoolOtherExpenses> plans = new List<SchoolOtherExpenses>();

                for (int i = 0; i < schoolOtherExpensesViewModels.Count; i++)
                {


                    SchoolOtherExpenses plan = new SchoolOtherExpenses
                    {
                        OtherExpenseId = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User) && p.SchoolId==schoolid).Select(p => p.SchoolId).FirstOrDefault(),

                        OtherExpenseTypeId = schoolOtherExpensesViewModels[i].OtherExpenseTypeId,
                        ExpensePerMonth = schoolOtherExpensesViewModels[i].ExpensePerMonth,                  
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                    };
                    plans.Add(plan);
                }
                _applicationContext.AddRange(plans);
                _applicationContext.SaveChanges();
                notyfService.Custom(_localizer["SchoolOtherExpensesCreated"].Value, 10, "#67757c", "fa fa-check");
            
                return RedirectToAction("Edit");
            }
            else {

                return View(displayPlan);
                }
        }
    }
}