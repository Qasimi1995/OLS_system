using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZProcessStatus
    {
        public ZProcessStatus()
        {
            ProcessProgress = new HashSet<ProcessProgress>();
            SubProcessStatus = new HashSet<SubProcessStatus>();
        }

        public Guid ProcessStatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusNameDari { get; set; }
        public string StatusNamePast { get; set; }
        public string StatusNameDariPast { get; set; }
        public string StatusNameDash { get; set; }
        public string StatusNameDashDari { get; set; }

        public virtual ICollection<ProcessProgress> ProcessProgress { get; set; }
        public virtual ICollection<SubProcessStatus> SubProcessStatus { get; set; }
    }
}
