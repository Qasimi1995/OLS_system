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
    public class SchoolOtherExpensesController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public SchoolOtherExpensesController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
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
        public IActionResult RecieveApplication()
        {
            var UserId = _userManager.GetUserId(User);
            var school = _applicationContext.School.Where(p => p.CreatedBy == UserId).ToList();
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
                        SchoolId = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                       
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
                if (result ==processProgresses.Count)
                {
                    ViewBag.AppSubMessage = "Application submitted sucessfully, Please keep following your application";
                }
                else {
                    ViewBag.AppSubMessage = "Couldnn't Submit application";

                }
                return View();
            }
        

            return View();
        }

        [HttpGet]
        public IActionResult Navigate()
        {
            var UserId = _userManager.GetUserId(User);
            var schoolOtherExpenses = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == UserId);
            if (schoolOtherExpenses.Count() != 0)
            {
                return RedirectToAction("Edit");
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
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            
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
            p.UserId == _userManager.GetUserId(User) && p.ProcessStatusId== Guid.Parse("C42112A8-0C7B-4D4A-A93E-01B93F794420")).Select(p => new ProcessProgress {
            SchoolId=p.SchoolId,
            
            }).FirstOrDefault();
            ViewBag.AppSubmitButton = IsSubmitted;
            return View(displayPlan);
        }
        public IActionResult Create()
        {
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
            var schoolid = _applicationContext.SchoolOtherExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
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
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),

                        OtherExpenseTypeId = schoolOtherExpensesViewModels[i].OtherExpenseTypeId,
                        ExpensePerMonth = schoolOtherExpensesViewModels[i].ExpensePerMonth,                  
                        CreatedBy = _userManager.GetUserId(User),
                        CreatedAt = DateTime.Now,
                    };
                    plans.Add(plan);
                }
                _applicationContext.AddRange(plans);
                _applicationContext.SaveChanges();
                ViewBag.Message = "sucessfully";
                return RedirectToAction("Edit");
            }
            else {

                return View(displayPlan);
                }
        }
    }
}