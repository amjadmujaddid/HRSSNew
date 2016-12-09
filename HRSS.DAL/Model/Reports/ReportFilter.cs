using HRSS.DAL.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Reports
{
    public class ReportFilter : DTOBase
    {
        public int ReportId { get; set; }
        public string FieldName { get; set; }
        public string Caption { get; set; }
        public Nullable<bool> IsParameter { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<int> FieldTypeId { get; set; }
        public string TableSource { get; set; }
        public Nullable<int> Sort { get; set; }
    }
}
