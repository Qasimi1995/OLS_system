using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class PartyDocument
    {
        public Guid PartyDocumentId { get; set; }
        public Guid PartyId { get; set; }
        public Guid? DocCategoryId { get; set; }
        public Guid? DocTypeId { get; set; }
        public string DocPath { get; set; }

        public virtual ZDocCategory DocCategory { get; set; }
        public virtual ZDocType DocType { get; set; }
        public virtual Party Party { get; set; }
    }
}
