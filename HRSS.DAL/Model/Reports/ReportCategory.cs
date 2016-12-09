using HRSS.DAL.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Reports
{
    public class ReportCategory : DTOBase
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Remarks { get; set; }
    }
}
