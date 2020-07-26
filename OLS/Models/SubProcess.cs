using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SubProcess
    {
        public SubProcess()
        {
            ProcessProgress = new HashSet<ProcessProgress>();
            SubProcessStatus = new HashSet<SubProcessStatus>();
        }

        public Guid SubProcessId { get; set; }
        public Guid? ProcessId { get; set; }
        public string SubProcesName { get; set; }
        public string SubProcesNameDari { get; set; }
        public int? OrderNumber { get; set; }
        public int? TimelineInDays { get; set; }
        public string RoleId { get; set; }

        public virtual Process Process { get; set; }
        public virtual ICollection<ProcessProgress> ProcessProgress { get; set; }
        public virtual ICollection<SubProcessStatus> SubProcessStatus { get; set; }
    }
}
