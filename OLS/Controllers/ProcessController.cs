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
    public class ProcessController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public ProcessController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProcess()
        {
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
                                 join subProcessStatus in _applicationContext.SubProcessStatus on new {a=a.SubProcessId.ToString(),b=b.ProcessStatusId.ToString() }equals new{a=subProcessStatus.SubProcessId.ToString(),b=subProcessStatus.ProcessStatusId.ToString() } into subProcessStatusGroup
                                 from e in subProcessStatusGroup.DefaultIfEmpty()
                               where process.ProcessId == id && c.SchoolId==schoolid
                               
                               select new SubProcessViewModel
                               {
                                   SubProcessId =a.SubProcessId,
                                   ProcessId=process.ProcessId,
                                   ProcessName=process.ProcessName,
                                   SubProcesName =a.SubProcesName,
                                   SubProcesNameDari=a.SubProcesNameDari,
                                   OrderNumber =a.OrderNumber,
                                   TimelineInDays=a.TimelineInDays,
                                   StatusNamePast = d.StatusNamePast,
                                   StatusNameDariPast = d.StatusNameDariPast,
                                   CompletionFlag = e.CompletionFlag,
                                   Remarks=b.Remarks,
                               }).OrderBy(p=>p.OrderNumber).ToList();
            return View(displayPlan);
        }
    }
}