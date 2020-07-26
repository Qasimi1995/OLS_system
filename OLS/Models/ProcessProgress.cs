using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ProcessProgress
    {
        public Guid ProcessProgressId { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? SubProcessId { get; set; }
        public Guid SchoolId { get; set; }
        public string UserId { get; set; }
        public Guid? ProcessStatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? StatusDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Process Process { get; set; }
        public virtual ZProcessStatus ProcessStatus { get; set; }
        public virtual School School { get; set; }
        public virtual SubProcess SubProcess { get; set; }
    }
}
