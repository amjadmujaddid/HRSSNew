using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRSS.BLL.Master;
using HRSS.DAL.Model.Master;
using HRSS.Utility;
using HRSS.BLL.Master.Contract;

namespace HRSS.UI.Controllers
{
    [RoutePrefix("menu")]
    public class MainMenuController : Controller
    {
        private IMenuService _menuService = new MenuService();

        public ActionResult List()
        {
            return View();
        }


        [ChildActionOnly]
        [Route("getmenu")]
        public JsonResult BindMainMenu()
        {
            List<Menu> listMenu = new List<Menu>();
            GetAllDataMenuResponse response = _menuService.GetAllDataMenu();
            listMenu.AddRange(response.GroupList);
            return Json(listMenu.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}