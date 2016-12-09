using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Master
{
    public class GroupMenu
    {
        public string GroupId { get; set; }
        public string MenuId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime EditDate { get; set; }
        public string EditBy { get; set; }

        public virtual Group Group { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
