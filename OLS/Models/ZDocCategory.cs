using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZDocCategory
    {
        public ZDocCategory()
        {
            PartyDocument = new HashSet<PartyDocument>();
            ZDocType = new HashSet<ZDocType>();
        }

        public Guid DocCategoryId { get; set; }
        public string CatagoryName { get; set; }
        public string CatagoryNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<PartyDocument> PartyDocument { get; set; }
        public virtual ICollection<ZDocType> ZDocType { get; set; }
    }
}
