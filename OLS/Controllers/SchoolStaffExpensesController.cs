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
    public class SchoolStaffExpensesController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public SchoolStaffExpensesController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
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
            var staffExpensesPan = _applicationContext.SchoolStaffExpenses.Where(p => p.CreatedBy == UserId);
            if (staffExpensesPan.Count() != 0)
            {
                return RedirectToAction("Edit");
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
                ViewBag.Message = "Successful";
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
            
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
                                   Salary= schoolStaffExpenses.Salary,
                                   Amount = schoolStaffExpenses.Amount,
                               }).ToList();


            return View(displayPlan);
        }
        public IActionResult Create()
        {
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
            var schoolid = _applicationContext.SchoolStaffExpenses.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();

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
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),

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

                ViewBag.Message = "Successful";

                return RedirectToAction("Edit");
            }
            else {

                return View(displayPlan);
                 }
        }
    }
}