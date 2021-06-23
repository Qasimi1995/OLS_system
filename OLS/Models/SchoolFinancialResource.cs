using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OLS.Models
{
    public partial class SchoolFinancialResource
    {
        public Guid SchoolId { get; set; }
        [Required]
        public Guid? SchoolBussinessTypeId { get; set; }
        [Required]
        public string FundingSourceName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual School School { get; set; }
        public virtual ZSchoolBussinessType SchoolBussinessType { get; set; }
    }
}
