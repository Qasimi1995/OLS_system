using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OLS.Models;
using OLS.ViewModels;
using OLS.FunctionsLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace OLS.Controllers
{
    [Authorize]
    public class DPERepController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        private Functions _functions;

        public DPERepController(ApplicationContext applicationContext, IHostingEnvironment environment, UserManager<User> userManager, Functions functions)
        {
            _applicationContext = applicationContext;
            _env = environment;
            _userManager = userManager;
            _functions = functions;
        }
        public IActionResult Index()
        {
            var userid = _userManager.GetUserId(User);
            var roleid = _applicationContext.UserRoles.Where(p => p.UserId == _userManager.GetUserId(User)).Select(p => p.RoleId).FirstOrDefault();
            var ProvinceId = _applicationContext.Users.Where(p => p.Id == _userManager.GetUserId(User)).Select(p => p.ProvinceId).FirstOrDefault();
            List<int?> ProcessNumbers = _applicationContext.SubProcess.Where(p => p.RoleId == roleid).Select(p => p.OrderNumber ).ToList();
            ViewBag.DashBoardStatuses = _functions.GetDashBoardStatuses(ProcessNumbers, roleid,ProvinceId);
            ViewBag.ProcessList = _applicationContext.SubProcess.Where(p => p.RoleId == roleid).Select(p => new SubProcess
            {
               SubProcesName= p.SubProcesName,
                SubProcesNameDari= p.SubProcesNameDari + "/" + p.SubProcesName,
                OrderNumber=p.OrderNumber 
            
            }).ToList();


            return View();
        }

        [HttpGet]
        public IActionResult SchoolList(int? OrderNumber, Nullable<Guid> ProcessStatusId, byte CompletionFlag)
        {
            var header = (from subProcess in _applicationContext.SubProcess
                          join subProcessStatus in _applicationContext.SubProcessStatus on subProcess.SubProcessId equals subProcessStatus.SubProcessId
                          join zProcessStatus in _applicationContext.ZProcessStatus on subProcessStatus.ProcessStatusId equals zProcessStatus.ProcessStatusId
                          where subProcess.OrderNumber == OrderNumber && zProcessStatus.ProcessStatusId == ProcessStatusId
                          select new ZProcessStatus
                          {
                              StatusNameDash = zProcessStatus.StatusNameDash,
                              StatusNameDashDari = zProcessStatus.StatusNameDashDari,


                          }).FirstOrDefault();


            IList<SchoolDisplayViewModel> schoolList = new List<SchoolDisplayViewModel>();
            var userid = _userManager.GetUserId(User);
            var roleid = _applicationContext.UserRoles.Where(p => p.UserId == _userManager.GetUserId(User)).Select(p => p.RoleId).FirstOrDefault();
            var ProvinceId = _applicationContext.Users.Where(p => p.Id == _userManager.GetUserId(User)).Select(p => p.ProvinceId).FirstOrDefault();
            var subprocess = _applicationContext.SubProcess.Where(p => p.RoleId == roleid).Select(p => new { p.SubProcessId, p.OrderNumber }).FirstOrDefault();
            DateTime dt1 = DateTime.Now;
            //for PED roleid
            if (roleid == "DB70DAE4-9422-4A2F-A14B-7552702F026B")
            {
                if (OrderNumber == subprocess.OrderNumber - 1 || OrderNumber == 5)
                {

                    CompletionFlag = 0;
                    
                    

                    schoolList = (from school in _applicationContext.School
                                  join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                                  join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                                  join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                                  //join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                                  join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                                  join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                                  join processProgress in _applicationContext.ProcessProgress on school.SchoolId equals processProgress.SchoolId
                                  join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                                  where processProgress.ProcessStatusId == ProcessStatusId && subProcess.OrderNumber == OrderNumber
                                  && partyAddress.ProvinceId == ProvinceId
                                  && !(from s in _applicationContext.ProcessProgress
                                       join ssubProcess in _applicationContext.SubProcess on s.SubProcessId equals ssubProcess.SubProcessId
                                       where ssubProcess.OrderNumber == OrderNumber + 1 && s.ProcessStatusId != null
                                       select s.SchoolId

                                  ).Contains(school.SchoolId)
                                  select new SchoolDisplayViewModel
                                  {
                                      SchoolId = school.SchoolId,
                                      SchoolLevel = zSchoolLevel.SchoolLevelNameDari,
                                      SchoolName = school.SchoolName,
                                      SchoolEnglishName=school.SchoolEnglishName,
                                      SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari,
                                      Province = zProvince.ProvNaDar,
                                      District = zDistrict.DistNaDar,
                                      VillageNahia = partyAddress.Nahia,
                                      OrderNumber = subProcess.OrderNumber,
                                      StatusDate=processProgress.StatusDate,
                                      No_days =Math.Abs((dt1 - processProgress.StatusDate).Value.Days)



                                  }).Distinct().ToList();

                }
                else
                {

                    schoolList = (from school in _applicationContext.School
                                  join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                                  join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                                  join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                                 // join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                                  join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                                  join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                                  join processProgress in _applicationContext.ProcessProgress on school.SchoolId equals processProgress.SchoolId
                                  join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                                  where processProgress.ProcessStatusId == ProcessStatusId && subProcess.OrderNumber == OrderNumber
                                  && partyAddress.ProvinceId == ProvinceId
                                  select new SchoolDisplayViewModel
                                  {
                                      SchoolId = school.SchoolId,
                                      SchoolLevel = zSchoolLevel.SchoolLevelNameDari,
                                      SchoolName = school.SchoolName,
                                      SchoolEnglishName = school.SchoolEnglishName,
                                      SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari,
                                      Province = zProvince.ProvNaDar,
                                      District = zDistrict.DistNaDar,
                                      VillageNahia = partyAddress.Nahia,
                                      OrderNumber = subProcess.OrderNumber,
                                      StatusDate = processProgress.StatusDate,
                                      No_days = Math.Abs((dt1 - processProgress.StatusDate).Value.Days)
                                  }).Distinct().ToList();


                }


            }
            else
            {
                //For New application in all user except PED
                if (OrderNumber == subprocess.OrderNumber - 1 || OrderNumber == 5)
                {
                    
                    CompletionFlag = 0;

                    schoolList = (from school in _applicationContext.School
                                  join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                                  join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                                  join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                                  //join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                                  join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                                  join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                                  join processProgress in _applicationContext.ProcessProgress on school.SchoolId equals processProgress.SchoolId
                                  join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                                  where processProgress.ProcessStatusId == ProcessStatusId && subProcess.OrderNumber == OrderNumber

                                  && !(from s in _applicationContext.ProcessProgress
                                       join ssubProcess in _applicationContext.SubProcess on s.SubProcessId equals ssubProcess.SubProcessId
                                       where ssubProcess.OrderNumber == OrderNumber + 1 && s.ProcessStatusId != null
                                       select s.SchoolId

                                  ).Contains(school.SchoolId)
                                  select new SchoolDisplayViewModel
                                  {
                                      SchoolId = school.SchoolId,
                                      SchoolLevel = zSchoolLevel.SchoolLevelNameDari,
                                      SchoolName = school.SchoolName,
                                      SchoolEnglishName=school.SchoolEnglishName,
                                      SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari,
                                      Province = zProvince.ProvNaDar,
                                      District = zDistrict.DistNaDar,
                                      VillageNahia = partyAddress.Nahia,
                                      OrderNumber = subProcess.OrderNumber,
                                      StatusDate = processProgress.StatusDate,
                                      No_days = Math.Abs((dt1 - processProgress.StatusDate).Value.Days)

                                  }).Distinct().ToList();

                }
                else
                {
                    //for approved, rejected
                    schoolList = (from school in _applicationContext.School
                                  join partyAddress in _applicationContext.PartyAddress on school.SchoolId equals partyAddress.PartyId
                                  join zProvince in _applicationContext.ZProvince on partyAddress.ProvinceId equals zProvince.ProvinceId
                                  join zDistrict in _applicationContext.ZDistrict on partyAddress.DistrictId equals zDistrict.DistrictId
                                 // join zVillageNahia in _applicationContext.ZVillageNahia on partyAddress.VillageNahiaId equals zVillageNahia.VillageNahiaId
                                  join zSchoolLevel in _applicationContext.ZSchoolLevel on school.SchoolLevelId equals zSchoolLevel.SchoolLevelId
                                  join zSchoolGenderType in _applicationContext.ZSchoolGenderType on school.SchoolGenderTypeId equals zSchoolGenderType.SchoolGenderTypeId
                                  join processProgress in _applicationContext.ProcessProgress on school.SchoolId equals processProgress.SchoolId
                                  join subProcess in _applicationContext.SubProcess on processProgress.SubProcessId equals subProcess.SubProcessId
                                  where processProgress.ProcessStatusId == ProcessStatusId && subProcess.OrderNumber == OrderNumber
                                  select new SchoolDisplayViewModel
                                  {
                                      SchoolId = school.SchoolId,
                                      SchoolLevel = zSchoolLevel.SchoolLevelNameDari,
                                      SchoolName = school.SchoolName,
                                      SchoolEnglishName = school.SchoolEnglishName,
                                      SchoolGenderType = zSchoolGenderType.SchoolGenderTypeNameDari,
                                      Province = zProvince.ProvNaDar,
                                      District = zDistrict.DistNaDar,
                                      VillageNahia = partyAddress.Nahia,
                                      OrderNumber = subProcess.OrderNumber,
                                      StatusDate = processProgress.StatusDate,
                                      No_days = Math.Abs((dt1 - processProgress.StatusDate).Value.Days)
                                  }).Distinct().ToList();


                }



            }

            if (schoolList.Count == 0)
            {

                ViewBag.Result = "معلومات تازه موجود نیست";

            }
            ViewBag.CompletionFlag = CompletionFlag;
            ViewBag.OrderNumber = OrderNumber;
            ViewBag.ProcessStatusId = ProcessStatusId;
            ViewBag.header = header;
            ViewBag.roleid = roleid;
            return View(schoolList);
        }

        


        [HttpGet]
        public IActionResult CheckList(Guid schoolid)
        {
            ViewBag.PersonInfo = _functions.GetPersonList(schoolid);
            ViewBag.SchoolInfo = _functions.GetSchool(schoolid);
            return View();

        }


        [HttpGet]
        public IActionResult SchoolLicense(Guid schoolid)
        {
            //ViewBag.PersonInfo = _functions.GetPersonList(schoolid);
            //ViewBag.SchoolInfo = _functions.GetSchool(schoolid);

            var model = _applicationContext.School
                                          .Join(_applicationContext.Person, p => p.SchoolId, n => n.SchoolId,
                                          ((school, person) =>
                                          new DisplayLicenseViewModel
                                          {
                                              school = school,
                                              person = person,


                                          }))
                                      .Where(a => a.school.SchoolId == schoolid).FirstOrDefault();
            ViewBag.FounderInfo = _functions.GetPerson(schoolid, Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236"));
            ViewBag.SchoolInfo = _functions.GetSchool(schoolid);

            return View(model);
        }



        [HttpGet]
        public IActionResult Check(Guid schoolid, int? OrderNumber, Nullable<Guid> ProcessStatusId, byte CompletionFlag)
        {
            ViewBag.FounderInfo = _functions.GetPerson(schoolid, Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236"));
            ViewBag.SchoolInfo = _functions.GetSchool(schoolid);
            ViewBag.FinancialResouceInfo = _functions.GetSchoolFinancialResource(schoolid);
            ViewBag.PrincipleInfo = _functions.GetPerson(schoolid, Guid.Parse("FD744DE3-476C-428F-B7E0-381A9DD286FE"));
            ViewBag.TeachInfo = _functions.GetPersonList(schoolid, Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D"));
            ViewBag.EnrollmentPlanCurrentYear = _functions.GetEnrollmentPlan(schoolid, "Current");
            ViewBag.EnrollmentPlanNextYear = _functions.GetEnrollmentPlan(schoolid, "Next");
            ViewBag.FinancialPlan = _functions.GetSchoolFinancialPlan(schoolid);
            ViewBag.StaffExpenses = _functions.GetSchoolStaffExpenses(schoolid);
            ViewBag.SchoolOtherExpenses = _functions.GetSchoolOtherExpenses(schoolid);
            ViewBag.UserDocuments = _functions.GetDocurmentList(schoolid, Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A"));
            var roleid = _applicationContext.UserRoles.Where(p => p.UserId == _userManager.GetUserId(User)).Select(p => p.RoleId).FirstOrDefault();
            var subprocessid = _applicationContext.SubProcess.Where(p => p.OrderNumber == OrderNumber).Select(p => p.SubProcessId).FirstOrDefault();
            var ProcessNumbers = _applicationContext.SubProcess.Where(p => p.RoleId == roleid).Select(p =>
                new SubProcess
                {
                    OrderNumber = p.OrderNumber,
                }
                ).ToList();

            int? ExactProcessNumber = 0;
            for (int i = 0; i < ProcessNumbers.Count; i++)
            {
                if (ProcessNumbers[i].OrderNumber >= OrderNumber)
                {

                    ExactProcessNumber = ProcessNumbers[i].OrderNumber;
                    break;
                }


            }
            ViewBag.ProcessStatuses = _functions.GetZProcessStatuses(ExactProcessNumber, schoolid);
            ViewBag.CompletionFlag = CompletionFlag;
            ViewBag.OrderNumber = OrderNumber;
            ViewBag.ProcessStatusId = ProcessStatusId;
            ViewBag.roleid = roleid;


            return View();

        }
        [HttpPost]
        public IActionResult Check(ProcessProgressViewModel processProgressViewModel)
        {

            if (ModelState.IsValid)
            {



                var processprogress = _applicationContext.ProcessProgress.Find(processProgressViewModel.ProcessProgressId);
                ProcessHistory processHistory = new ProcessHistory
                {
                    HistoryId = Guid.NewGuid(),
                    ProcessId = processprogress.ProcessId,
                    SubProcessId = processprogress.SchoolId,
                    SchoolId = processprogress.SchoolId,
                    UserId = processprogress.UserId,
                    ProcessStatusId = processprogress.ProcessStatusId,
                    Remarks = processprogress.Remarks,
                    StatusDate = processprogress.StatusDate,
                    UpdatedBy = processprogress.UpdatedBy,
                    UpdatedAt = processprogress.UpdatedAt,
                    CreatedAt = DateTime.Now,
                };


                processprogress.UserId = _userManager.GetUserId(User);
                processprogress.ProcessStatusId = processProgressViewModel.ProcessStatusId;
                processprogress.StatusDate = DateTime.Now;
                processprogress.Remarks = processProgressViewModel.Remarks;
                processprogress.UpdatedBy = _userManager.GetUserId(User);
                processprogress.UpdatedAt = DateTime.Now;

                //Report submittion process by provincial user
                if (processProgressViewModel.ProcessStatusId == Guid.Parse("D279A58A-1FC1-4A01-A9A3-38EC746ABE62"))
                {
                    string FilePath = "";
                    string Report = "";

                    Guid? Pid = processprogress.SchoolId;
                    if (processProgressViewModel.Report != null)
                    {

                        string UploadsFolder = Path.Combine(_env.WebRootPath, "PED", Pid.ToString());
                        if (!Directory.Exists(Path.Combine(UploadsFolder)))
                        {
                            Directory.CreateDirectory(UploadsFolder);
                        }
                        string fileName = Pid.ToString() + "-schoolVisitReport.pdf";
                        FilePath = Path.Combine(UploadsFolder, fileName);
                        using (var filestream= new FileStream(FilePath, FileMode.Create)) {
                            processProgressViewModel.Report.CopyTo(filestream);
                        }
                       
                        Report = "/PED/" + Pid.ToString() + "/" + fileName;
                    }//Report documents which get uploaded by provincial user
                    PartyDocument partyDocument = new PartyDocument
                    {
                        PartyDocumentId = Guid.NewGuid(),
                        PartyId = processprogress.SchoolId,
                        DocCategoryId = Guid.Parse("877EBDF1-604A-4E11-A9B2-3FAF05A9F094"),
                        DocTypeId = Guid.Parse("58D356BE-AD99-498E-8579-4D98518EFD5E"),
                        DocPath = Report,
                    };
                    _applicationContext.Add(partyDocument);

                }
                _applicationContext.Add(processHistory);
                _applicationContext.Update(processprogress);
                _applicationContext.SaveChanges();

                var CompletionFlag = (from zProcessStatus in _applicationContext.ZProcessStatus
                                      join subProcessStatus in _applicationContext.SubProcessStatus on zProcessStatus.ProcessStatusId equals subProcessStatus.ProcessStatusId
                                      join subProcess in _applicationContext.SubProcess on subProcessStatus.SubProcessId equals subProcess.SubProcessId
                                      where subProcess.OrderNumber == processProgressViewModel.OrderNumber && zProcessStatus.ProcessStatusId == processProgressViewModel.ProcessStatusIdNav
                                      select subProcessStatus.CompletionFlag);
                return RedirectToAction("SchoolList", new
                {
                    OrderNumber = processProgressViewModel.OrderNumber,
                    ProcessStatusId = processProgressViewModel.ProcessStatusIdNav,

                    CompletionFlag = CompletionFlag
                });
            }

            Guid schoolid = processProgressViewModel.SchoolId;
            ViewBag.FounderInfo = _functions.GetPerson(schoolid, Guid.Parse("CAE7466D-198A-423B-903F-BB64D58C0236"));
            ViewBag.SchoolInfo = _functions.GetSchool(schoolid);
            ViewBag.FinancialResouceInfo = _functions.GetSchoolFinancialResource(schoolid);
            ViewBag.PrincipleInfo = _functions.GetPerson(schoolid, Guid.Parse("FD744DE3-476C-428F-B7E0-381A9DD286FE"));
            ViewBag.TeachInfo = _functions.GetPersonList(schoolid, Guid.Parse("E15C4649-0ABA-4B88-95AA-3936A863450D"));
            ViewBag.EnrollmentPlanCurrentYear = _functions.GetEnrollmentPlan(schoolid, "Current");
            ViewBag.EnrollmentPlanNextYear = _functions.GetEnrollmentPlan(schoolid, "Next");
            ViewBag.FinancialPlan = _functions.GetSchoolFinancialPlan(schoolid);
            ViewBag.StaffExpenses = _functions.GetSchoolStaffExpenses(schoolid);
            ViewBag.SchoolOtherExpenses = _functions.GetSchoolOtherExpenses(schoolid);
            ViewBag.UserDocuments = _functions.GetDocurmentList(schoolid, Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A"));
            var roleid = _applicationContext.UserRoles.Where(p => p.UserId == _userManager.GetUserId(User)).Select(p => p.RoleId).FirstOrDefault();
            var subprocessid = _applicationContext.SubProcess.Where(p => p.OrderNumber == processProgressViewModel.OrderNumber).Select(p => p.SubProcessId).FirstOrDefault();
            var ProcessNumbers = _applicationContext.SubProcess.Where(p => p.RoleId == roleid).Select(p =>
                new SubProcess
                {
                    OrderNumber = p.OrderNumber,
                }
                ).ToList();

            int? ExactProcessNumber = 0;
            for (int i = 0; i < ProcessNumbers.Count; i++)
            {
                if (ProcessNumbers[i].OrderNumber >= processProgressViewModel.OrderNumber)
                {

                    ExactProcessNumber = ProcessNumbers[i].OrderNumber;
                    break;
                }


            }
            ViewBag.ProcessStatuses = _functions.GetZProcessStatuses(ExactProcessNumber, processProgressViewModel.SchoolId);
            ViewBag.CompletionFlag = processProgressViewModel.CompletionFlag;
            ViewBag.OrderNumber = processProgressViewModel.OrderNumber;
            ViewBag.ProcessStatusId = processProgressViewModel.ProcessStatusId;
            ViewBag.roleid = roleid;

            return View(processProgressViewModel);
        }
    }
}