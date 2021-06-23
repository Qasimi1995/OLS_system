using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class Process
    {
        public Process()
        {
            ProcessProgress = new HashSet<ProcessProgress>();
            SubProcess = new HashSet<SubProcess>();
        }

        public Guid ProcessId { get; set; }
        public string ProcessName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<ProcessProgress> ProcessProgress { get; set; }
        public virtual ICollection<SubProcess> SubProcess { get; set; }
    }
}
