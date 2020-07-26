using System;
using System.Collections.Generic;

namespace OLS.Models
{
    public partial class ZLaboratoryMaterialType
    {
        public ZLaboratoryMaterialType()
        {
            School = new HashSet<School>();
        }

        public Guid LaboratoryMaterialTypeId { get; set; }
        public string LaboratoryMaterialTypeName { get; set; }
        public string LaboratoryMaterialTypeNameDari { get; set; }
        public int? OrderNumber { get; set; }

        public virtual ICollection<School> School { get; set; }
    }
}
