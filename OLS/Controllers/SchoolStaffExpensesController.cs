using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;
using Microsoft.AspNetCore.Http;
using OLS.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class SchoolStaffExpensesController : Controller
    {
        private ApplicationContext _applicationContext;
        IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly INotyfService notyfService;
        private readonly IHtmlLocalizer _localizer;

        public SchoolStaffExpensesController(ApplicationContext applicationContext, IWebHostEnvironment environment, IHtmlLocalizer<SchoolStaffExpensesController> localizer, UserManager<User> userManager,
             INotyfService notyfService)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
            _localizer = localizer;
            this.notyfService = notyfService;
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
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();



            //New Changes

            var result = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
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
            var staffExpensesPan = _applicationContext.SchoolStaffExpenses.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolid);
            if (staffExpensesPan.Count() != 0)
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
                    return RedirectToAction("NoEditSSEC");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
                
            }

            return RedirectToAction("Create");
        }


        [HttpPost]
        public IActionResult Edit(IList<SchoolStaffExpensesViewModel> staffExpensesViewModels)
        {
            if (ModelState.IsValid)
            {

                IList<SchoolStaffExpenses> Plans = new List<SchoolStaffExpenses>();
                
                for (int i = 0; i < staffExpensesViewModels.Count; i++)
                {
                    SchoolStaffExpenses plan = _applicationContext.SchoolStaffExpenses.Find(staffExpensesViewModels[i].Id);
                    plan.Salary = staffExpensesViewModels[i].Salary;
                    plan.Amount = staffExpensesViewModels[i].Amount;             
                    plan.UpdatedBy = _userManager.GetUserId(User);
                    plan.UpdatedAt = DateTime.Now;
                    Plans.Add(plan);

                }
                _applicationContext.UpdateRange(Plans);
                _applicationContext.SaveChanges();
                notyfService.Custom(_localizer["SchoolStaffExpensesUpdated"].Value, 10, "#67757c", "fa fa-check");
              
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();


            //New Changes


            var result = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolId && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
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

            if (result == null)
            {
                schoolId = re_schoolid;
            }





            var displayPlan = (from zEducationalRole in _applicationContext.ZEducationalRole
                               join zPartyRoleType in _applicationContext.ZPartyRoleType on zEducationalRole.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                               join schoolStaffExpenses in _applicationContext.SchoolStaffExpenses on zPartyRoleType.PartyRoleTypeId equals schoolStaffExpenses.PartyRoleTypeId
                               where schoolStaffExpenses.SchoolId == schoolId
                               orderby zPartyRoleType.OrderNumber
                               select new SchoolStaffExpensesViewModel
                               {
                                   Id= schoolStaffExpenses.Id,
                                   PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari + "/" + zPartyRoleType.PartyRoleTypeNamePashto + "/" + zPartyRoleType.PartyRoleTypeName,
                                   PartyRoleTypeId = zPartyRoleType.PartyRoleTypeId,
                                   Salary=schoolStaffExpenses.Salary,
                                   Amount = schoolStaffExpenses.Amount,
                               }).ToList();


            HttpContext.Session.SetString("SchoolStaffExpenses", "Create");


            return View(displayPlan);
        }

        [HttpGet]
        [Route("NoEditSSEC")]
        public IActionResult NoEditSSEC()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();





            



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


           

           

      








            var displayPlan = (from zEducationalRole in _applicationContext.ZEducationalRole
                               join zPartyRoleType in _applicationContext.ZPartyRoleType on zEducationalRole.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                               join schoolStaffExpenses in _applicationContext.SchoolStaffExpenses on zPartyRoleType.PartyRoleTypeId equals schoolStaffExpenses.PartyRoleTypeId
                               where schoolStaffExpenses.SchoolId == schoolId
                               orderby zPartyRoleType.OrderNumber
                               select new SchoolStaffExpensesViewModel
                               {
                                   Id = schoolStaffExpenses.Id,
                                   PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari + "/" + zPartyRoleType.PartyRoleTypeNamePashto + "/" + zPartyRoleType.PartyRoleTypeName,
                                   PartyRoleTypeId = zPartyRoleType.PartyRoleTypeId,
                                   Salary = schoolStaffExpenses.Salary,
                                   Amount = schoolStaffExpenses.Amount,
                               }).ToList();


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
            var schoolFinancialPlan = HttpContext.Session.GetString("SchoolFinancialPlan");
            var shcoolFinancialPlanBussinessType = HttpContext.Session.GetString("SchoolBusinessTypeID");
            if (schoolFinancialPlan == null && shcoolFinancialPlanBussinessType =="7e743fd4-78c6-4ff5-aec3-4c9f78f8f41a")
            {
                return RedirectToAction("Navigate", "SchoolFinancialPlan");
            }


            var displayPlan = (from zEducationalRole in _applicationContext.ZEducationalRole
                               join zPartyRoleType in _applicationContext.ZPartyRoleType on zEducationalRole.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                              
                               orderby zPartyRoleType.OrderNumber
                               select new
                               {
                                   PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari + "/" + zPartyRoleType.PartyRoleTypeNamePashto + "/" + zPartyRoleType.PartyRoleTypeName,
                                   PartyRoleTypeId = zPartyRoleType.PartyRoleTypeId,


                               }).ToList().Select(x => new SchoolStaffExpensesViewModel()
                               {

                                   PartyRoleType = x.PartyRoleType,
                                   PartyRoleTypeId = x.PartyRoleTypeId,

                               }).ToList();





            return View(displayPlan);
        }
        [HttpPost]
        public IActionResult Create(IList<SchoolStaffExpensesViewModel> staffExpensesViewModels)
        {
            var schoolid = _applicationContext.SchoolStaffExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();



            var result = _applicationContext.ProcessProgress.Where(p => p.SchoolId == schoolid && p.SubProcessId == Guid.Parse("E592365F-2FB6-4B0F-9C5E-01277BE052F0")).FirstOrDefault();
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

            //if (result == null)
            //{
            //    schoolid = re_schoolid;
            //}



            var displayPlan = (from zEducationalRole in _applicationContext.ZEducationalRole
                               join zPartyRoleType in _applicationContext.ZPartyRoleType on zEducationalRole.PartyRoleTypeId equals zPartyRoleType.PartyRoleTypeId
                               join schoolStaffExpenses in _applicationContext.SchoolStaffExpenses on zPartyRoleType.PartyRoleTypeId equals schoolStaffExpenses.PartyRoleTypeId
                               where schoolStaffExpenses.SchoolId == schoolid
                               orderby zPartyRoleType.OrderNumber
                               select new
                               {
                                   PartyRoleType = zPartyRoleType.PartyRoleTypeNameDari + "/" + zPartyRoleType.PartyRoleTypeNamePashto + "/" + zPartyRoleType.PartyRoleTypeName,
                                   PartyRoleTypeId = zPartyRoleType.PartyRoleTypeId,


                               }).ToList().Select(x => new SchoolStaffExpensesViewModel()
                               {

                                   PartyRoleType = x.PartyRoleType,
                                   PartyRoleTypeId = x.PartyRoleTypeId,

                               }).ToList();

            if (ModelState.IsValid) {

                IList<SchoolStaffExpenses> plans = new List<SchoolStaffExpenses>();

                for (int i = 0; i < staffExpensesViewModels.Count; i++)
                {


                    SchoolStaffExpenses plan = new SchoolStaffExpenses
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User) && p.SchoolId==schoolid).Select(p => p.SchoolId).FirstOrDefault(),

                        PartyRoleTypeId = staffExpensesViewModels[i].PartyRoleTypeId,
                        Salary = staffExpensesViewModels[i].Salary,
                        Amount = staffExpensesViewModels[i].Amount,
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                    };
                    plans.Add(plan);
                }
                _applicationContext.AddRange(plans);
                _applicationContext.SaveChanges();
                notyfService.Custom(_localizer["SchoolStaffExpensesCreated"].Value, 10, "#67757c", "fa fa-check");
             
                HttpContext.Session.SetString("SchoolStaffExpenses", "Create");
                // return RedirectToAction("Edit");
                return RedirectToAction("Navigate", "SchoolOtherExpenses");
            }
            else {

                return View(displayPlan);
                 }
        }
    }
}