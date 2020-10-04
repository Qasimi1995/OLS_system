using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class SchoolFinancialValidationViewModel
    {
        IList<SchoolFinancialPlanViewModel> schoolFinancialPlans { get; set; }
        public string getdropdown { get; set; }
    }
}
