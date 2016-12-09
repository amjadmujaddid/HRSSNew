using HRSS.DAL.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Master
{
    public class Menu : DTOBase
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public Nullable<int> Sort { get; set; }
        public string Icon { get; set; }
        public string SiteMap { get; set; }
        public string TypeMenu { get; set; }
        public string IsPublic { get; set; }
    }
}
