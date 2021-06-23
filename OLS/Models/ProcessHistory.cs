using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ProcessHistory
    {
        public Guid HistoryId { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? SubProcessId { get; set; }
        public string UserId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? ProcessStatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
