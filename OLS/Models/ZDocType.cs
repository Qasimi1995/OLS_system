using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZDocType
    {
        public ZDocType()
        {
            PartyDocument = new HashSet<PartyDocument>();
        }

        public Guid DocTypeId { get; set; }
        public Guid? DocCategoryId { get; set; }
        public string DocTypeName { get; set; }
        public string DocTypeNameDari { get; set; }
        public string DocTypeNamePashto { get; set; }
        public string OrderNumber { get; set; }

        public virtual ZDocCategory DocCategory { get; set; }
        public virtual ICollection<PartyDocument> PartyDocument { get; set; }
    }
}
