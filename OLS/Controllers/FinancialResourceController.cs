using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;
using Microsoft.AspNetCore.Identity;
using OLS.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.Localization;

namespace OLS.Controllers
{
    [Route("FinancialResource")]
    [Authorize(Roles ="Applicant")]
    public class FinancialResourceController : Controller
    {
        private ApplicationContext _applicationContext;
        IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly INotyfService notyfService;
        private readonly IHtmlLocalizer _localizer;

        public FinancialResourceController(ApplicationContext applicationContext, IHtmlLocalizer<FinancialResourceController> localizer, IWebHostEnvironment environment, UserManager<User> userManager, INotyfService notyfService)
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
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).OrderByDescending(p=>p.CreatedAt).Select(p => p.SchoolId).FirstOrDefault();


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

         
            if(myschoolid==null && sch_id == null)
            {
                schoolid = Guid.NewGuid();
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

            var FinancialResource = _applicationContext.SchoolFinancialResource.Where(p => p.CreatedBy == UserId && p.SchoolId==schoolid).FirstOrDefault();
            ViewBag.FinancialResource = FinancialResource;
            if (FinancialResource != null)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 6 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                        }
                    }
                    return RedirectToAction("NoEdit", new { schoolId = FinancialResource.SchoolId });
                }
                else
                {
                    return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
                }
            }
            return RedirectToAction("Create");

        }

        [Route("NoEdit/{schoolId}")]
        [HttpGet]
        public IActionResult NoEdit(Guid schoolId)
        {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
            if (schoolId != null)
            {

                SchoolFinancialResource FinancialResource = _applicationContext.SchoolFinancialResource.Find(schoolId);
                return View(FinancialResource);
            }

            return View();
        }



        [Route("Edit/{schoolId}")]
        [HttpGet]
        public IActionResult Edit(Guid schoolId)
        {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
            if (schoolId != null) {

                SchoolFinancialResource FinancialResource = _applicationContext.SchoolFinancialResource.Find(schoolId);


                HttpContext.Session.SetString("FinancialResource", "Create");
                HttpContext.Session.SetString("SchoolBusinessTypeID", FinancialResource.SchoolBussinessTypeId.ToString());

                return View(FinancialResource);
            }

            return View();
        }

        [Route("Edit/{schoolId}")]
        [HttpPost]
        public IActionResult Edit(SchoolFinancialResource schoolFinancialResource)
        {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
            if (ModelState.IsValid)
            {
                SchoolFinancialResource financialResource = _applicationContext.SchoolFinancialResource.Find(schoolFinancialResource.SchoolId);
                financialResource.SchoolBussinessTypeId = schoolFinancialResource.SchoolBussinessTypeId;
                financialResource.FundingSourceName = schoolFinancialResource.FundingSourceName;
                financialResource.UpdatedBy = _userManager.GetUserId(User);
                financialResource.UpdatedAt = DateTime.Now;

                _applicationContext.Update(financialResource);
                _applicationContext.SaveChanges();
                notyfService.Custom(_localizer["FinancialResourceUpdated"].Value, 10, "#67757c", "fa fa-check");
                ViewBag.Message = _localizer["RecordUpdated"].Value;
                return View(financialResource);
             
            }

            return View();
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create() {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
          
            var myschoolid = HttpContext.Session.GetString("SchoolID");

            if (myschoolid == null )
            {
                return RedirectToAction("Navigate", "School");
            }

            return View();
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create(SchoolFinancialResource schoolFinancialResource)
        {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
            if (ModelState.IsValid) {           
                Guid SchoolID = new Guid(HttpContext.Session.GetString("SchoolID"));

                SchoolFinancialResource FinancialResource = new SchoolFinancialResource
                {
                    SchoolId = SchoolID,
                    SchoolBussinessTypeId = schoolFinancialResource.SchoolBussinessTypeId,
                    FundingSourceName = schoolFinancialResource.FundingSourceName,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt=DateTime.Now,

                };

                _applicationContext.Add(FinancialResource);
                HttpContext.Session.SetString("FinancialResource","Create");
                HttpContext.Session.SetString("SchoolBusinessTypeID", FinancialResource.SchoolBussinessTypeId.ToString());
                var result=_applicationContext.SaveChanges();
                if (result == 1)
                {
                    notyfService.Custom(_localizer["FinancialResourceCreated"].Value, 10, "#67757c", "fa fa-check");
                    ViewBag.Message = _localizer["RecordUpdated"].Value;
                    //  return RedirectToAction("Edit", new { schoolId = SchoolID });
                    return RedirectToAction("Navigate", "Principle");
                }
                else {

                    ModelState.TryAddModelError("","Some Error");
                    return View(schoolFinancialResource);
                }
               
                
            }
            return View(schoolFinancialResource);
        }
    }
}