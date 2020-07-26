using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OLS.CustomValidation;

namespace OLS.ViewModels
{
    public class ProcessProgressViewModel
    {
        [Required(ErrorMessage = "*")]
        public Guid ProcessProgressId { get; set; }
        public Guid? ProcessId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? SubProcessId { get; set; }
        [Required(ErrorMessage = "*")]
        public int? OrderNumber { get; set; }
        public byte CompletionFlag { get; set; }
        public Guid SchoolId { get; set; }

        public string UserId { get; set; }
        [Required(ErrorMessage = "*")]
        public Guid? ProcessStatusId { get; set; }
        public Guid? ProcessStatusName { get; set; }

        [Required(ErrorMessage = "*")]
        public string Remarks { get; set; }

        [DataType(DataType.Upload)]
        [ValidateRport(new string[] { ".pdf" }, ErrorMessage = " only .pdf format is allowed")]
        //[Remote(action: "ValidateReport", controller: "DPERep", AdditionalFields = "Report,ProcessStatusId")]
        public IFormFile Report { get; set; }
        public Guid? ProcessStatusIdNav { get; set; }
    }
}
