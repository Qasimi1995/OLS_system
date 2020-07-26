using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OLS.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace OLS.ViewModels
{
    public class DocTypeViewModel
    {
        public Guid DocTypeId { get; set; }
        public Guid? DocCategoryId { get; set; }
        public Guid? SchoolId { get; set; }
        public string DocTypeName { get; set; }
        public string DocTypeNameEng { get; set; }
        public string DocTypeNameDari { get; set; }
        public string DocTypeNamePashto { get; set; }
        public string DocPath { get; set; }
        public string OrderNumber { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1024 * 1024, ErrorMessage = "Max 1mb file is allowed")]
        [AllowedExtensions(new string[] { ".pdf"}, ErrorMessage = "only pdf format is allowed")]
        public IFormFile Document { get; set; }
    }
}
