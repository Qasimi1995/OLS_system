using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLS.Models
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RowId { get; set; }
        public string UserName { get; set; }
        public string Operation { get; set; }
        public string TableName { get; set; }
        public string InsertedFieldsValues { get; set; }
        public string OldFieldsValues { get; set; }
        public string UpdatedFieldsValue { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }


    }
}
