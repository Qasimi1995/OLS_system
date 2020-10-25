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

namespace OLS.Controllers
{
    [Route("FinancialResource")]
    [Authorize]
    public class FinancialResourceController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public FinancialResourceController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager)
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

            var FinancialResource = _applicationContext.SchoolFinancialResource.Where(p => p.CreatedBy == UserId).FirstOrDefault();
            if (FinancialResource != null)
            {
                return RedirectToAction("Edit", new { schoolId = FinancialResource.SchoolId });
            }


            return RedirectToAction("Create");

        }

        [Route("Edit/{schoolId}")]
        [HttpGet]
        public IActionResult Edit(Guid schoolId)
        {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;
            if (schoolId != null) {

                SchoolFinancialResource FinancialResource = _applicationContext.SchoolFinancialResource.Find(schoolId);
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
                ViewBag.Message = "معلومات ثبت گردید / معلومات په بریالیتوب سره ثبت شوي/ record Saved Successfuly";
                return View(financialResource);
             
            }

            return View();
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create() {
            var BussinessType = _applicationContext.ZSchoolBussinessType.OrderBy(o => o.OrderNumber);
            ViewBag.BussinessType = BussinessType;

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
                    CreateAt=DateTime.Now,

                };

                _applicationContext.Add(FinancialResource);
                var result=_applicationContext.SaveChanges();
                if (result == 1)
                {
                    ViewBag.Message = "معلومات ثبت گردید";
                    return RedirectToAction("Edit", new { schoolId = SchoolID });
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