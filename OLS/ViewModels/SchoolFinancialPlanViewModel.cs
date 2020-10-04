using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class SchoolFinancialPlanViewModel
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }

        public Guid? SchoolSubLevelId { get; set; }
        public string SchoolSubLevelName { get; set; }
        [Required(ErrorMessage = "*")]
        public int? NfreeStudents { get; set; }
        [Required(ErrorMessage = "*")]
        public int? NpaidStudents { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:0}",ApplyFormatInEditMode =true)]
        public decimal? FeeAmount { get; set; }
        
        public string Year { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal? AdmissionFee { get; set; }

    }


   
}
