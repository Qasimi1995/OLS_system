using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.ViewModels
{
    public class SubProcessViewModel
    {
        public Guid SubProcessId { get; set; }
        public Guid? ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string SubProcesName { get; set; }
        public string SubProcesNameDari { get; set; }
        public int? OrderNumber { get; set; }
        public int? TimelineInDays { get; set; }
        public string ProcessStatusName { get; set; }
        public string ProcessStatusNameDari { get; set; }
        public string StatusNamePast { get; set; }
        public string StatusNameDariPast { get; set; }

        public string Remarks { get; set; }
        public byte? CompletionFlag { get; set; }
        public DateTime? StatusDate { get; set; }

    }
}
