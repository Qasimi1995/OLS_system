using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SubProcessStatus
    {
        public Guid SubProcessStatusId { get; set; }
        public Guid? SubProcessId { get; set; }
        public Guid? ProcessStatusId { get; set; }
        public byte? CompletionFlag { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ZProcessStatus ProcessStatus { get; set; }
        public virtual SubProcess SubProcess { get; set; }
    }
}
