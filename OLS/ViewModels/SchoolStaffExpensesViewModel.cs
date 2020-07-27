using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class SchoolStaffExpensesViewModel
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid? PartyRoleTypeId { get; set; }
        public string PartyRoleType { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = "*")]
        public int? Amount { get; set; }
    }
}
