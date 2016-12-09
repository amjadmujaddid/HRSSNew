using HRSS.BLL.Master;
using HRSS.BLL.Master.Contract;
using HRSS.DAL.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace HRSS.UI.Controllers
{
    [RoutePrefix("navmenu")]
    public class AppMenuController : ApiController
    {
     
        private IMenuService _menuService = new MenuService();

        [Route("getmenu")]
        [AcceptVerbs("GET")]
        public List<Menu> MainMenu()
        {
            List<Menu> listMenu = new List<Menu>();
            GetAllDataMenuResponse response = _menuService.GetAllDataMenu();
            listMenu.AddRange(response.GroupList);

            return listMenu.ToList();
        }
    }
}
