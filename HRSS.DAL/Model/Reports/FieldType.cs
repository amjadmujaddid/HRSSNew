using HRSS.DAL.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Reports
{
    public class FieldType : DTOBase
    {
        public int FieldTypeId { get; set; }
        public string FieldTypeCode { get; set; }
        public string FieldTypeName { get; set; }
        public Nullable<int> Sort { get; set; }
        public string Remarks { get; set; }
    }
}
