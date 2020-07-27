using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class SchoolOtherExpensesViewModel
    {
        public Guid OtherExpenseId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? OtherExpenseTypeId { get; set; }
        
        public string OtherExpenseTypeName { get; set; }
         [Required(ErrorMessage ="*")]
        public decimal? ExpensePerMonth { get; set; }
    }
}
