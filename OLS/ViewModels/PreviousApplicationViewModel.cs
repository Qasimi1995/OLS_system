using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class PreviousApplicationViewModel
    {
        public Guid SchoolId { get; set;}
        public string SchoolName { get; set; }
        public string SchoolEnglishName { get; set; }
        public DateTime ApplicationDate { get; set; }

    }
}
