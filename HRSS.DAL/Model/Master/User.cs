using HRSS.DAL.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Model.Master
{
    public class User : DTOBase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Images { get; set; }
        public string GroupMenuId { get; set; }
        public string IsActive { get; set; }
        public string ThemeId { get; set; }
        public string SessionID { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public Nullable<System.DateTime> PasswordExpired { get; set; }
    }
}
