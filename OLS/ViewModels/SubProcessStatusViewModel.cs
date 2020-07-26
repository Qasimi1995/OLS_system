using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class SubProcessStatusViewModel
    {
        public Guid ProcessProgressId { get; set; }
        public Guid SubProcessStatusId { get; set; }
        public Guid? SubProcessId { get; set; }
        public Guid? ProcessStatusId { get; set; }
        public string StatusNameDari { get; set; }
        public string StatusName { get; set; }
        public byte? CompletionFlag { get; set; }
        public int? OrderNumber { get; set; }


    }
}
