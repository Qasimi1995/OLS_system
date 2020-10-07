using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLS.Models;
using OLS.ViewModels;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace OLS.Controllers
{
    [Authorize]
    public class SchoolFinancialPlanController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public SchoolFinancialPlanController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
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
            var studentEnrollmentPlan = _applicationContext.SchoolFinancialPlan.Where(p => p.CreatedBy == UserId);
            if (studentEnrollmentPlan.Count() != 0)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Create");
        }


        [HttpPost]
        public IActionResult Edit(IList<SchoolFinancialPlanViewModel> schoolFinancialPlans)
        {
            var schoolBussinesType = _applicationContext.SchoolFinancialResource.Include(a => a.SchoolBussinessType)
                .Where(a => a.SchoolId == schoolFinancialPlans.ElementAt(0).SchoolId).Select(a => a.SchoolBussinessType).FirstOrDefault();

            if (schoolBussinesType.BussinessTypeName == "For Profit")
            {
                if (!ModelState.IsValid)//now it will work
                {

                    return RedirectToAction("Edit");
                }

            }
            IList<SchoolFinancialPlan> Plans = new List<SchoolFinancialPlan>();
            for (int i = 0; i < schoolFinancialPlans.Count; i++)
            {
                SchoolFinancialPlan plan = _applicationContext.SchoolFinancialPlan.Find(schoolFinancialPlans[i].Id);
                plan.FeeAmount = schoolFinancialPlans[i].FeeAmount;
                plan.NfreeStudents = schoolFinancialPlans[i].NfreeStudents;
                plan.NpaidStudents = schoolFinancialPlans[i].NpaidStudents;
                plan.AdmissionFee = schoolFinancialPlans[i].AdmissionFee;
                plan.UpdatedBy = _userManager.GetUserId(User);
                plan.UpdatedAt = DateTime.Now;
                Plans.Add(plan);
            }
            _applicationContext.UpdateRange(Plans);
            _applicationContext.SaveChanges();

            return RedirectToAction("Edit");


        }
        public IActionResult Edit()
        {
            var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();


            var displayPlan = (from schoolFinancialPlan in _applicationContext.SchoolFinancialPlan
                               join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schoolFinancialPlan.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                               where schoolFinancialPlan.SchoolId == schoolId
                               orderby schoolSubLevel.OrderNumber
                               select new SchoolFinancialPlanViewModel
                               {
                                   Id = schoolFinancialPlan.Id,
                                   SchoolId = schoolId,
                                   SchoolSubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                   FeeAmount = schoolFinancialPlan.FeeAmount,
                                   NfreeStudents = schoolFinancialPlan.NfreeStudents,
                                   NpaidStudents = schoolFinancialPlan.NpaidStudents,
                                   Year = schoolFinancialPlan.Year,
                                   AdmissionFee = schoolFinancialPlan.AdmissionFee,
                               }).ToList();

            ViewBag.Tax = String.Format("{0:0.00}", _applicationContext.SchoolFinancialPlan.Where(p => p.SchoolId == schoolId).Select(p => (p.NpaidStudents * p.FeeAmount * p.AdmissionFee) * 0.1m).Sum());

            ViewBag.schoolBussinesType = _applicationContext.SchoolFinancialResource.Include(a => a.SchoolBussinessType)
                 .Where(a => a.SchoolId == displayPlan.ElementAt(0).SchoolId).Select(a => a.SchoolBussinessType).FirstOrDefault().BussinessTypeName;

            return View(displayPlan);
        }

        public IActionResult Create()
        {
            var schoolLevelid = _applicationContext.School.Where(p => p.CreatedBy==_userManager.GetUserId(User)).Select(p => p.SchoolLevelId).FirstOrDefault();
             var schoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();

            var displayPlan = (from schoolLevel in _applicationContext.ZSchoolLevel
                                                  join schooLevelSubLevel in _applicationContext.ZSchoolLevelSubLevel on schoolLevel.SchoolLevelId equals schooLevelSubLevel.SchoolLevelId
                                                  join schoolSubLevel in _applicationContext.ZSchoolSubLevel on schooLevelSubLevel.SchoolSubLevelId equals schoolSubLevel.SchoolSubLevelId
                                                  where schoolLevel.SchoolLevelId == schoolLevelid
                                                  orderby schoolSubLevel.OrderNumber
                                                  select new
                                                  {
                                                      SubLevelName = schoolSubLevel.SubLevelNameDari + "/" + schoolSubLevel.SubLevelNamePashto + "/" + schoolSubLevel.SubLevelName,
                                                      SubLevelId = schoolSubLevel.SchoolSubLevelId,
                                                     


                                                  }).ToList().Select(x => new SchoolFinancialPlanViewModel()
                                                  {                                                    
                                                      
                                                      SchoolSubLevelName = x.SubLevelName,
                                                      SchoolSubLevelId = x.SubLevelId,
                                                      SchoolId = schoolId,

                                                  }).ToList();

            ViewBag.schoolBussinesType = _applicationContext.SchoolFinancialResource.Include(a => a.SchoolBussinessType)
                .Where(a => a.SchoolId == displayPlan.ElementAt(0).SchoolId).Select(a => a.SchoolBussinessType).FirstOrDefault().BussinessTypeName;

            return View(displayPlan);
        }


        [HttpPost]
        public IActionResult Create(IList<SchoolFinancialPlanViewModel> schoolFinancialPlans)
        {
            var schoolBussinesType = _applicationContext.SchoolFinancialResource.Include(a => a.SchoolBussinessType)
                .Where(a => a.SchoolId == schoolFinancialPlans.ElementAt(0).SchoolId).Select(a => a.SchoolBussinessType).FirstOrDefault();


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


                               }).ToList().Select(x => new SchoolFinancialPlanViewModel()
                               {

                                   SchoolSubLevelName = x.SubLevelName,
                                   SchoolSubLevelId = x.SubLevelId,

                               }).ToList();

            if (schoolBussinesType.BussinessTypeName == "For Profit")
            {
                if (!ModelState.IsValid)
                {

                    return RedirectToAction("Edit");
                }
                else
                {
                    return View(displayPlan);
                }

            }
           

                IList<SchoolFinancialPlan> plans = new List<SchoolFinancialPlan>();

                for (int i = 0; i < schoolFinancialPlans.Count; i++)
                {


                    SchoolFinancialPlan plan = new SchoolFinancialPlan
                    {
                        Id = Guid.NewGuid(),
                        SchoolId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),
                        Year = DateTime.Now.Year.ToString(),
                        SchoolSubLevelId = schoolFinancialPlans[i].SchoolSubLevelId,
                        FeeAmount = schoolFinancialPlans[i].FeeAmount,
                        NfreeStudents = schoolFinancialPlans[i].NfreeStudents,
                        NpaidStudents = schoolFinancialPlans[i].NpaidStudents,
                        AdmissionFee= schoolFinancialPlans[i].AdmissionFee,
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
    }
}