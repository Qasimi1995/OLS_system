using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class searchreportviewModel
    {
        public string schoolname { get; set; }
        public string schoollevel { get; set; }
        public string provincename { get; set; }
        public string status { get; set; }
        public string generalreport { get; set; }
        public string schoolgender { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }


    }
}
