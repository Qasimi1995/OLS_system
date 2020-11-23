using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using OLS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OLS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace OLS.Controllers
{
    [Authorize]
    [Route("School")]
    public class SchoolController : Controller
    {
        private ApplicationContext _applicationContext;
        IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;

        public SchoolController(ApplicationContext applicationContext, IHostingEnvironment environment,UserManager<User> userManager)
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
        public IActionResult Navigate() {
            var UserId = _userManager.GetUserId(User);
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();

            var displayPlan = (from process in _applicationContext.Process
                               join subProcess in _applicationContext.SubProcess on process.ProcessId equals subProcess.ProcessId into processgroup
                               from a in processgroup.DefaultIfEmpty()
                               join processProgress in _applicationContext.ProcessProgress on a.SubProcessId equals processProgress.SubProcessId into processProgressGroup
                               from b in processProgressGroup.DefaultIfEmpty()
                               join School in _applicationContext.School on b.SchoolId equals School.SchoolId into schoolGroup
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

            var school = _applicationContext.School.Where(p => p.CreatedBy == UserId).FirstOrDefault();
            ViewBag.school = school;
            if (school != null)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                        }
                    }
                    return RedirectToAction("NoEdit", new { schoolId = school.SchoolId });
                }
                else
                {
                    return RedirectToAction("Edit", new { schoolId = school.SchoolId });
                }
             }


            return RedirectToAction("Create");
        }

        [HttpGet]
        [Route("NavigateDoc")]
        public IActionResult NavigateDoc()
        {
            var UserId = _userManager.GetUserId(User);
            var id = _applicationContext.Process.Where(p => p.ProcessId == Guid.Parse("88A9020D-D188-417C-9B11-7FDA9613B197")).Select(p => p.ProcessId).FirstOrDefault();
            var schoolid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();

            var displayPlan = (from process in _applicationContext.Process
                               join subProcess in _applicationContext.SubProcess on process.ProcessId equals subProcess.ProcessId into processgroup
                               from a in processgroup.DefaultIfEmpty()
                               join processProgress in _applicationContext.ProcessProgress on a.SubProcessId equals processProgress.SubProcessId into processProgressGroup
                               from b in processProgressGroup.DefaultIfEmpty()
                               join School in _applicationContext.School on b.SchoolId equals School.SchoolId into schoolGroup
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
            var PartDocument = _applicationContext.PartyDocument.Where(p => p.PartyId == _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault()
            && p.DocCategoryId==Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
            );
            if (PartDocument.Count() != 0)
            {
                if (displayPlan.Count > 0)
                {
                    for (int i = 0; i < displayPlan.Count; i++)
                    {
                        if (displayPlan[i].OrderNumber == 1 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit");
                        }
                        else if (displayPlan[i].OrderNumber == 2 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit");
                        }
                        else if (displayPlan[i].OrderNumber == 3 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit"); ;
                        }
                        else if (displayPlan[i].OrderNumber == 5 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit");
                        }
                        else if (displayPlan[i].OrderNumber == 4 && displayPlan[i].CompletionFlag == 0)
                        {

                            return RedirectToAction("UploadDocumentsEdit");
                        }
                    }
                    return RedirectToAction("NoUploadDocumentsEdit");
                }
                else
                {
                    return RedirectToAction("UploadDocumentsEdit");
                }
                
            }

            return RedirectToAction("UploadDocuments");
        }


        [HttpGet]
        [Route("UploadDocuments")]
        public IActionResult UploadDocuments() {

            var DocumentList = (from zDocType in _applicationContext.ZDocType
                                where zDocType.DocCategoryId== Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
                               orderby zDocType.OrderNumber
                               select new DocTypeViewModel
                               {
                                   DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                   DocTypeNameEng=zDocType.DocTypeName,
                                   DocTypeId = zDocType.DocTypeId,


                               }).ToList();
            return View(DocumentList);

        }

        [HttpGet]
        [Route("NoUploadDocumentsEdit")]
        public IActionResult NoUploadDocumentsEdit()
        {

            var DocumentList = (from zDocType in _applicationContext.ZDocType
                                join partyDocument in _applicationContext.PartyDocument on zDocType.DocTypeId equals partyDocument.DocTypeId
                                where zDocType.DocCategoryId == Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
                                && partyDocument.PartyId == _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault()
                                orderby zDocType.OrderNumber
                                select new DocTypeViewModelEdit
                                {
                                    DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                    DocTypeNameEng = zDocType.DocTypeName,
                                    DocTypeId = zDocType.DocTypeId,
                                    DocPath = partyDocument.DocPath,
                                    SchoolId = partyDocument.PartyId,
                                    DocTypeNameDari = zDocType.DocTypeNameDari,
                                }).ToList();

            return View(DocumentList);

        }


        [HttpGet]
        [Route("UploadDocumentsEdit")]
        public IActionResult UploadDocumentsEdit()
        {

            var DocumentList = (from zDocType in _applicationContext.ZDocType
                                join partyDocument in _applicationContext.PartyDocument on zDocType.DocTypeId equals partyDocument.DocTypeId
                                where zDocType.DocCategoryId == Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
                                && partyDocument.PartyId == _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault()
                                orderby zDocType.OrderNumber
                                select new DocTypeViewModelEdit
                                {
                                    DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                    DocTypeNameEng = zDocType.DocTypeName,
                                    DocTypeId = zDocType.DocTypeId,
                                    DocPath = partyDocument.DocPath,
                                    SchoolId=partyDocument.PartyId,
                                    DocTypeNameDari=zDocType.DocTypeNameDari,
                                }).ToList();
            
            return View(DocumentList);

        }


        [HttpPost]
        [Route("UploadDocumentsEdit")]
        public IActionResult UploadDocumentsEdit(IList<DocTypeViewModelEdit> docTypeViewModels)
        {
            var DocumentList = (from zDocType in _applicationContext.ZDocType
                                join partyDocument in _applicationContext.PartyDocument on zDocType.DocTypeId equals partyDocument.DocTypeId
                                where zDocType.DocCategoryId == Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
                                && partyDocument.PartyId == _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault()
                                orderby zDocType.OrderNumber
                                select new DocTypeViewModelEdit
                                {
                                    DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                    DocTypeNameEng = zDocType.DocTypeName,
                                    DocTypeId = zDocType.DocTypeId,
                                    DocPath = partyDocument.DocPath,
                                    SchoolId = partyDocument.PartyId,
                                    DocTypeNameDari = zDocType.DocTypeNameDari,
                                }).ToList();
           
            if (ModelState.IsValid)
            {

                IList<PartyDocument> partyDocuments = new List<PartyDocument>();

                for (int i = 0; i < docTypeViewModels.Count; i++)
                {
                    string FilePath = "";
                    string DocPath = "";

                    Guid? Pid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
                    if (docTypeViewModels[i].Document != null)
                    {

                        string UploadsFolder = Path.Combine(_env.WebRootPath, "PartyDocuments", Pid.ToString());
                        if (!Directory.Exists(Path.Combine(UploadsFolder)))
                        {
                            Directory.CreateDirectory(UploadsFolder);
                        }
                        string fileName = Pid.ToString() + "-" + docTypeViewModels[i].DocTypeNameEng + ".pdf";
                        FilePath = Path.Combine(UploadsFolder, fileName);
                        using(var filestream = new FileStream(FilePath, FileMode.Create)){
                            docTypeViewModels[i].Document.CopyTo(filestream);
                        }
                        DocPath = "/PartyDocuments/" + Pid.ToString() + "/" + fileName;
                    }
                    
                }
               // ViewBag.Message = "Documents Uploaded Successfully!";
                return RedirectToAction("Notification");
            }
            else
            {
                
                return View(DocumentList);
            }

        }

        [HttpPost]
        [Route("UploadDocuments")]
        public IActionResult UploadDocuments(IList<DocTypeViewModel> docTypeViewModels)
        {

            var DocumentList = (from zDocType in _applicationContext.ZDocType
                                where zDocType.DocCategoryId == Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A")
                                orderby zDocType.OrderNumber
                                select new DocTypeViewModel
                                {
                                    DocTypeName = zDocType.DocTypeNameDari + "/" + zDocType.DocTypeNamePashto + "/" + zDocType.DocTypeName,
                                    DocTypeNameEng = zDocType.DocTypeName,
                                    DocTypeId = zDocType.DocTypeId,


                                }).ToList();
            if (ModelState.IsValid)
            {

                IList<PartyDocument> partyDocuments = new List<PartyDocument>();

                for (int i = 0; i < docTypeViewModels.Count; i++)
                {
                    string FilePath = "";
                    string DocPath = "";

                    Guid? Pid = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault();
                    if (docTypeViewModels[i].Document != null)
                    {

                        string UploadsFolder = Path.Combine(_env.WebRootPath, "PartyDocuments", Pid.ToString());
                        if (!Directory.Exists(Path.Combine(UploadsFolder)))
                        {
                            Directory.CreateDirectory(UploadsFolder);
                        }
                        string fileName = Pid.ToString() +"-"+ docTypeViewModels[i].DocTypeNameEng + ".pdf";
                        FilePath = Path.Combine(UploadsFolder, fileName);
                        using(var filestream= new FileStream(FilePath, FileMode.Create))
                        {
                            docTypeViewModels[i].Document.CopyTo(filestream);
                        }
                        DocPath = "/PartyDocuments/" + Pid.ToString() + "/" + fileName;
                    }


                    PartyDocument partyDocument = new PartyDocument
                    {
                        PartyDocumentId = Guid.NewGuid(),
                        PartyId = _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault(),

                        DocCategoryId =Guid.Parse("39C691C9-E88C-4F1F-A431-C0C7F723348A"),
                        DocTypeId = docTypeViewModels[i].DocTypeId,
                        DocPath= DocPath,

                    };
                    partyDocuments.Add(partyDocument);
                }
                var processprogress = _applicationContext.ProcessProgress.Where(p => p.SchoolId== _applicationContext.School.Where(p => p.CreatedBy == _userManager.GetUserId(User)).Select(p => p.SchoolId).FirstOrDefault()
                 && p.SubProcessId==Guid.Parse("FBF66EB9-E2E8-46D6-8CE9-5703E4B6D2C3")).FirstOrDefault();
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
                processprogress.ProcessStatusId = Guid.Parse("0BBD5B1E-BF84-4339-A748-C25BA6852741");
                processprogress.StatusDate = DateTime.Now;
                processprogress.Remarks = null;
                processprogress.UpdatedBy = _userManager.GetUserId(User);
                processprogress.UpdatedAt = DateTime.Now;
                _applicationContext.Add(processHistory);
                _applicationContext.Update(processprogress);
                _applicationContext.AddRange(partyDocuments);
                _applicationContext.SaveChanges();

               // ViewBag.Message = "Documents Uploaded Successfully!";
                return RedirectToAction("Notification");
            }
            else
            {

                return View(DocumentList);
            }

        }
        [Route("FindDistrict/{ProvinceId}")]
        public IActionResult FindDistrict(Guid ProvinceId)
        {
            var districts = _applicationContext.ZDistrict.Where(district => district.ProvinceId == ProvinceId).Select(distict => new {
                Id = distict.DistrictId,
                DistNameEng = distict.DistNaEng,
                DistNameDar = distict.DistNaDar

            }).ToList();
            return new JsonResult(districts);
        }
        [Route("FindVillagNahia/{DistrictId}")]
        public IActionResult FindVillagNahia(Guid DistrictId)
        {
            var VillageNahias = _applicationContext.ZVillageNahia.Where(villigaeNahia => villigaeNahia.DistrictId == DistrictId)
                .Select(villageNahia => new {
                    Vid = villageNahia.VillageNahiaId,
                    VNameEng = villageNahia.VillageNameEng

                }).ToList();
            return new JsonResult(VillageNahias);
        }


        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNrooms")]
        public async Task<IActionResult> ValidatoinNrooms(Guid SchoolLevelId, int Nrooms)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (Nrooms >= 6) {
                    return Json(true); }
                else
                {
                    return Json($"تعداد اطاق باید بالاتر از 5 باشد /د خونو شمېر باید له ۵ څخه ډېر وي / Number of Rooms must be greater than 5.");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (Nrooms >= 9)
                {
                    return Json(true);
                }
                else
                {
                    return Json($" د خونو شمېر باید له 8 څخه ډېر وي/تعداد اطاق باید بالاتر از 8 باشد/ Number of Rooms must be greater than 8.");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (Nrooms >= 12)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"د خونو شمېر باید له 11 څخه ډېر وي/تعداد اطاق باید بالاتر از 11 باشد/ Number of Rooms must be greater than 11.");
                }
            }
            else {

                return Json($"*****");
            }


        }


        [AcceptVerbs("Get", "Post")]
        [Route("ValidateDistancefromPuSchool")]
        public async Task<IActionResult> ValidatoinDistancefromPuSchool(int DistancefromPuSchool)
        {

            if (DistancefromPuSchool >= 500)
            {

                return Json(true);


            }
            else {
                return Json($" لږ تر لږه باید 500 متر وي /حد اقل باید 500 متر باشد/ Minimum shoud be 500m");


            }



        }
        [AcceptVerbs("Get", "Post")]
        [Route("ValidateDistanceFromPrSchool")]
        public async Task<IActionResult> ValidateDistanceFromPrSchool(int DistanceFromPrSchool)
        {

            if (DistanceFromPrSchool >= 200)
            {

                return Json(true);


            }
            else
            {
                return Json($"لږ تر لږه باید 200 متر وي /حد اقل باید 200 متر باشد/ Minimum shoud be 200m");


            }



        }

        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNteachDeskChair")]
        public async Task<IActionResult> ValidateNteachDeskChair(Guid SchoolLevelId, int NteachDeskChair)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (NteachDeskChair >= 8)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 8 وي /حد اقل باید 8 باشد/ Minimum shoud be 8");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (NteachDeskChair >= 13)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 13 وي /حد اقل باید 13 باشد/ Minimum shoud be 13");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (NteachDeskChair >= 18)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 18 وي /حد اقل باید 18 باشد/ Minimum shoud be 18");
                }
            }
            else
            {

                return Json($"*****");
            }


        }


        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNstudentDeskChair")]
        public async Task<IActionResult> ValidateNstudentDeskChair(Guid SchoolLevelId, int NstudentDeskChair)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (NstudentDeskChair >= 60)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 60 وي /حد اقل باید 60 باشد/ Minimum shoud be 60");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (NstudentDeskChair >= 90)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 90 وي /حد اقل باید 90 باشد/ Minimum shoud be 90");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (NstudentDeskChair >= 120)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 120 وي /حد اقل باید 120 باشد/ Minimum shoud be 120");
                }
            }
            else
            {

                return Json($"*****");
            }


        }
        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNbooks")]
        public async Task<IActionResult> ValidateNbooks(Guid SchoolLevelId, int Nbooks)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (Nbooks >= 500)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 500 وي /حد اقل باید 500 باشد/ Minimum shoud be 500");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (Nbooks >= 800)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 800 وي /حد اقل باید 800 باشد/ Minimum shoud be 800");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (Nbooks >= 1000)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 1000 وي /حد اقل باید 1000 باشد/ Minimum shoud be 1000");
                }
            }
            else
            {

                return Json($"*****");
            }


        }
        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNboards")]
        public async Task<IActionResult> ValidateNboards(Guid SchoolLevelId, int Nboards)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (Nboards >= 6)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 6 وي /حد اقل باید 6 باشد/ Minimum shoud be 6");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (Nboards >= 9)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 9 وي /حد اقل باید 9 باشد/ Minimum shoud be 9");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (Nboards >= 12)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 12 وي /حد اقل باید 12 باشد/ Minimum shoud be 12");
                }
            }
            else
            {

                return Json($"*****");
            }


        }

        [AcceptVerbs("Get", "Post")]
        [Route("ValidateNcomputers")]
        public async Task<IActionResult> ValidateNcomputers(Guid SchoolLevelId, int Ncomputers)
        {

            if (SchoolLevelId == Guid.Parse("D14C62C3-C9D9-46A4-9FDB-42D4A12AF7F9"))
            {
                if (Ncomputers >= 10)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 10 وي /حد اقل باید 10 باشد/ Minimum shoud be 10");
                }
            }

            if (SchoolLevelId == Guid.Parse("DCF64DF8-818E-478E-9873-600687F4AE08"))
            {
                if (Ncomputers >= 15)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 15 وي /حد اقل باید 15 باشد/ Minimum shoud be 15");
                }
            }
            if (SchoolLevelId == Guid.Parse("FF48CD0D-914F-44A6-B7F0-A66064ACC6CE"))
            {
                if (Ncomputers >= 20)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"لږ تر لږه باید 20 وي /حد اقل باید 20 باشد/ Minimum shoud be 20");
                }
            }
            else
            {

                return Json($"*****");
            }


        }

        [Route("NoEdit/{schoolId}")]
        [HttpGet]
        public IActionResult NoEdit(Guid schoolId)
        {
            if (schoolId != null)
            {

                var province = _applicationContext.ZProvince;
                ViewBag.Province = province;

                var SchoolLevel = _applicationContext.ZSchoolLevel.OrderBy(o => o.OrderNumber);
                ViewBag.SchoolLevel = SchoolLevel;

                var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
                ViewBag.SchoolGenderType = SchoolGenderType;

                var LabMaterialType = _applicationContext.ZLaboratoryMaterialType.OrderBy(o => o.OrderNumber);
                ViewBag.LabMaterialType = LabMaterialType;
                School school = _applicationContext.School.Find(schoolId);
                PartyAddress schoolAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == school.SchoolId).FirstOrDefault();
                SchoolViewModel schoolViewModel = new SchoolViewModel
                {
                    SchoolId = school.SchoolId,
                    SchoolLevelId = school.SchoolLevelId,
                    SchoolName = school.SchoolName,
                    SchoolEnglishName = school.SchoolEnglishName,
                    SchoolLongitude = school.SchoolLongitude,
                    SchoolLatitude = school.SchoolLatitude,
                    SchoolGenderTypeId = school.SchoolGenderTypeId,
                    Nrooms = school.Nrooms,
                    DistancefromPuSchool = school.DistancefromPuSchool,
                    DistanceFromPrSchool = school.DistanceFromPrSchool,
                    HasTeachingBooks = school.HasTeachingBooks,
                    HasTeachingAids = school.HasTeachingAids,
                    NteachDeskChair = school.NteachDeskChair,
                    NstudentDeskChair = school.NstudentDeskChair,
                    HasLibrary = school.HasLibrary,
                    Nbooks = school.Nbooks,
                    Nboards = school.Nboards,
                    LaboratoryMaterialTypeId = school.LaboratoryMaterialTypeId,
                    HasComputerLab = school.HasComputerLab,
                    Ncomputers = school.Ncomputers,
                    HasDrinkingWater = school.HasDrinkingWater,
                    NwashRooms = school.NwashRooms,
                    HasFirstAid = school.HasFirstAid,
                    HasFireDistinguisher = school.HasFireDistinguisher,
                    HasTransportation = school.HasTransportation,
                    HasSportFacilities = school.HasTransportation,
                    ProvinceId = schoolAddress.ProvinceId,
                    DistrictId = schoolAddress.DistrictId,
                    Nahia = schoolAddress.Nahia
                };

                var district = _applicationContext.ZDistrict.Where(d => d.ProvinceId == schoolAddress.ProvinceId);
                ViewBag.district = district;
                var villageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == schoolAddress.DistrictId);
                ViewBag.villageNahia = villageNahia;
                HttpContext.Session.SetString("SchoolID", schoolViewModel.SchoolId.ToString());
                return View(schoolViewModel);
            }


            return View();
        }

        [Route("Edit/{schoolId}")]
        [HttpGet]
        public IActionResult Edit(Guid schoolId)
        {
            if (schoolId != null) {
              
                    var province = _applicationContext.ZProvince;
                    ViewBag.Province = province;                    
                    
                    var SchoolLevel = _applicationContext.ZSchoolLevel.OrderBy(o => o.OrderNumber);
                    ViewBag.SchoolLevel = SchoolLevel;

                    var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
                    ViewBag.SchoolGenderType = SchoolGenderType;

                    var LabMaterialType = _applicationContext.ZLaboratoryMaterialType.OrderBy(o => o.OrderNumber);
                    ViewBag.LabMaterialType = LabMaterialType;
                    School school = _applicationContext.School.Find(schoolId);
                    PartyAddress schoolAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == school.SchoolId).FirstOrDefault();
                    SchoolViewModel schoolViewModel = new SchoolViewModel
                    {
                        SchoolId = school.SchoolId,
                        SchoolLevelId = school.SchoolLevelId,
                        SchoolName = school.SchoolName,
                        SchoolEnglishName=school.SchoolEnglishName,
                        SchoolLongitude=school.SchoolLongitude,
                        SchoolLatitude=school.SchoolLatitude,
                        SchoolGenderTypeId = school.SchoolGenderTypeId,
                        Nrooms = school.Nrooms,
                        DistancefromPuSchool = school.DistancefromPuSchool,
                        DistanceFromPrSchool = school.DistanceFromPrSchool,
                        HasTeachingBooks = school.HasTeachingBooks,
                        HasTeachingAids = school.HasTeachingAids,
                        NteachDeskChair = school.NteachDeskChair,
                        NstudentDeskChair = school.NstudentDeskChair,
                        HasLibrary = school.HasLibrary,
                        Nbooks = school.Nbooks,
                        Nboards = school.Nboards,
                        LaboratoryMaterialTypeId = school.LaboratoryMaterialTypeId,
                        HasComputerLab = school.HasComputerLab,
                        Ncomputers = school.Ncomputers,
                        HasDrinkingWater = school.HasDrinkingWater,
                        NwashRooms = school.NwashRooms,
                        HasFirstAid = school.HasFirstAid,
                        HasFireDistinguisher = school.HasFireDistinguisher,
                        HasTransportation = school.HasTransportation,
                        HasSportFacilities = school.HasTransportation,
                        ProvinceId=schoolAddress.ProvinceId,
                        DistrictId = schoolAddress.DistrictId,
                        Nahia=schoolAddress.Nahia
                    };

                        var district = _applicationContext.ZDistrict.Where(d => d.ProvinceId == schoolAddress.ProvinceId);
                        ViewBag.district = district;
                        var villageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == schoolAddress.DistrictId);
                        ViewBag.villageNahia = villageNahia;
                        HttpContext.Session.SetString("SchoolID", schoolViewModel.SchoolId.ToString());
                return View(schoolViewModel);
                }          


            return View();
        }

        [Route("Edit/{schoolId}")]
        [HttpPost]
        public IActionResult Edit(SchoolViewModel schoolViewModel)
        {
            if (ModelState.IsValid)
            {


                var SchoolLevel = _applicationContext.ZSchoolLevel.OrderBy(o => o.OrderNumber);
                ViewBag.SchoolLevel = SchoolLevel;

                var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
                ViewBag.SchoolGenderType = SchoolGenderType;

                var LabMaterialType = _applicationContext.ZLaboratoryMaterialType.OrderBy(o => o.OrderNumber);
                ViewBag.LabMaterialType = LabMaterialType;
               
                School school = _applicationContext.School.Find(schoolViewModel.SchoolId);
             
                school.SchoolLevelId=schoolViewModel.SchoolLevelId;
                school.SchoolName=schoolViewModel.SchoolName;
                school.SchoolEnglishName=schoolViewModel.SchoolEnglishName;
                school.SchoolLongitude = schoolViewModel.SchoolLongitude;
                school.SchoolLatitude= schoolViewModel.SchoolLatitude;
                school.SchoolGenderTypeId=schoolViewModel.SchoolGenderTypeId;
                school.Nrooms=schoolViewModel.Nrooms;
                school.DistancefromPuSchool=schoolViewModel.DistancefromPuSchool;
                school.DistanceFromPrSchool=schoolViewModel.DistanceFromPrSchool;
                school.HasTeachingBooks=schoolViewModel.HasTeachingBooks;
                school.HasTeachingAids=schoolViewModel.HasTeachingAids;
                school.NteachDeskChair=schoolViewModel.NteachDeskChair;
                school.NstudentDeskChair=schoolViewModel.NstudentDeskChair;
                school.HasLibrary=schoolViewModel.HasLibrary;
                school.Nbooks=schoolViewModel.Nbooks;
                school.Nboards=schoolViewModel.Nboards;
                school.LaboratoryMaterialTypeId=schoolViewModel.LaboratoryMaterialTypeId;
                school.HasComputerLab=schoolViewModel.HasComputerLab;
                school.Ncomputers=schoolViewModel.Ncomputers;
                school.HasDrinkingWater=schoolViewModel.HasDrinkingWater;
                school.NwashRooms=schoolViewModel.NwashRooms;
                school.HasFirstAid=schoolViewModel.HasFirstAid;
                school.HasFireDistinguisher=schoolViewModel.HasFireDistinguisher;
                school.HasTransportation=schoolViewModel.HasTransportation;
                school.HasSportFacilities = schoolViewModel.HasSportFacilities;
                school.UpdatedBy = _userManager.GetUserId(User);
                school.UpdatedAt = DateTime.Now;

                PartyAddress schoolAddress = _applicationContext.PartyAddress.Where(p => p.PartyId == school.SchoolId).FirstOrDefault();
                schoolAddress.ProvinceId = schoolViewModel.ProvinceId;
                schoolAddress.DistrictId = schoolViewModel.DistrictId;
                schoolAddress.Nahia = schoolViewModel.Nahia;

                var province = _applicationContext.ZProvince;
                ViewBag.Province = province;
                var district = _applicationContext.ZDistrict.Where(d => d.ProvinceId == schoolAddress.ProvinceId);
                ViewBag.district = district;
                var villageNahia = _applicationContext.ZVillageNahia.Where(v => v.DistrictId == schoolAddress.DistrictId);
                ViewBag.villageNahia = villageNahia;

                _applicationContext.Update(school);
                _applicationContext.Update(schoolAddress);
                _applicationContext.SaveChanges();
               
                HttpContext.Session.SetString("SchoolID", schoolViewModel.SchoolId.ToString());
                ViewBag.Message = "معلومات موفقانه تصحیح گردید / معلومات په بریالیتوب سره اصلاح شول / Record Successfully updated ";
                return View(schoolViewModel);

            }

            return View();

         }
        [Route("Create")]
        public IActionResult Create()
        {

        
            var province = _applicationContext.ZProvince;
            ViewBag.Province = province;

            var SchoolLevel = _applicationContext.ZSchoolLevel.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolLevel = SchoolLevel;

            var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolGenderType = SchoolGenderType;

            var LabMaterialType = _applicationContext.ZLaboratoryMaterialType.OrderBy(o => o.OrderNumber);
            ViewBag.LabMaterialType = LabMaterialType;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(SchoolViewModel schoolModel)
        {
            var province = _applicationContext.ZProvince;
            ViewBag.Province = province;

            var SchoolLevel = _applicationContext.ZSchoolLevel.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolLevel = SchoolLevel;

            var SchoolGenderType = _applicationContext.ZSchoolGenderType.OrderBy(o => o.OrderNumber);
            ViewBag.SchoolGenderType = SchoolGenderType;

            var LabMaterialType = _applicationContext.ZLaboratoryMaterialType.OrderBy(o => o.OrderNumber);
            ViewBag.LabMaterialType = LabMaterialType;
            Guid pid   = new Guid(HttpContext.Session.GetString("FounderID"));
            Guid schoolId = Guid.NewGuid();
            if (ModelState.IsValid) {

                Party schoolParty = new Party {
                    PartyId = schoolId
                };
                School school = new School {
                    SchoolId = schoolId,
                    SchoolLevelId = schoolModel.SchoolLevelId,
                    SchoolName = schoolModel.SchoolName,
                    SchoolEnglishName = schoolModel.SchoolEnglishName,
                    SchoolLongitude = schoolModel.SchoolLongitude,
                    SchoolLatitude = schoolModel.SchoolLatitude,
                    SchoolGenderTypeId = schoolModel.SchoolGenderTypeId,
                    Nrooms = schoolModel.Nrooms,
                    DistancefromPuSchool=schoolModel.DistancefromPuSchool,
                    DistanceFromPrSchool=schoolModel.DistanceFromPrSchool,
                    HasTeachingBooks = schoolModel.HasTeachingBooks,
                    HasTeachingAids=schoolModel.HasTeachingAids,
                    NteachDeskChair=schoolModel.NteachDeskChair,
                    NstudentDeskChair=schoolModel.NstudentDeskChair,
                    HasLibrary=schoolModel.HasLibrary,
                    Nbooks =schoolModel.Nbooks,
                    Nboards=schoolModel.Nboards,
                    LaboratoryMaterialTypeId=schoolModel.LaboratoryMaterialTypeId,
                    HasComputerLab=schoolModel.HasComputerLab,
                    Ncomputers=schoolModel.Ncomputers,
                    HasDrinkingWater=schoolModel.HasDrinkingWater,
                    NwashRooms=schoolModel.NwashRooms,
                    HasFirstAid=schoolModel.HasFirstAid,
                    HasFireDistinguisher=schoolModel.HasFireDistinguisher,
                    HasTransportation=schoolModel.HasTransportation,
                    HasSportFacilities=schoolModel.HasTransportation,
                    CreatedBy = _userManager.GetUserId(User),
                    CreatedAt = DateTime.Now,
            };

                Guid AddressId = Guid.NewGuid();
                PartyAddress schoolAddaress = new PartyAddress()
                {
                    PartyAddressId = AddressId,
                    PartyId = schoolId,
                    AddressTypeId = Guid.Parse("28048D3E-BF94-4068-9735-6E798BA9FD52"),
                    ProvinceId = schoolModel.ProvinceId,
                    DistrictId = schoolModel.DistrictId,
                    Nahia = schoolModel.Nahia,
                };

                Person founder = _applicationContext.Person.Where(p => p.PersonId == pid).FirstOrDefault();
                founder.SchoolId = schoolId;
                _applicationContext.Add(schoolParty);
                _applicationContext.Add(school);
                _applicationContext.Add(schoolAddaress);
                _applicationContext.Update(founder);
                _applicationContext.SaveChanges();
                HttpContext.Session.SetString("SchoolID",schoolId.ToString());
                ViewBag.Message = "معلومات ثبت گردید";
                return RedirectToAction("Edit", new { schoolId=schoolId });
            }
            return View(schoolModel);
        }

        [HttpGet]
        [Route("Notification")]
        public IActionResult Notification()
        {
            ViewBag.Message = "Documents Uploaded Successfully!";
            return View();
        }

    }
}