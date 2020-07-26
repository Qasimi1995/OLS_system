using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.Models
{
    public class DashboardStatuses
    {
        public Guid SubProcessID { get; set; }
        public Guid ProcessStatusID { get; set; }
        public string StatusNameDash { get; set; }
        public string StatusNameDashDari { get; set; }
        public int OrderNumber { get; set; }
        public byte CompletionFlag { get; set; }
        public int Count { get; set; }
    }
}
