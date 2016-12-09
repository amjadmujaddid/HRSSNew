using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRSS.BLL.Master;
using HRSS.DAL.Model.Master;
using HRSS.BLL.Master.Contract;

namespace UnitTestKNSS.Master
{
    [TestClass]
    public class MenuUT
    {
        #region Class Attribute and Property

        private IMenuService _menuService = new MenuService();

        #endregion

        #region Test Data

        Menu _menuValidData = new Menu()
        {
            MenuId = "1",
            MenuName = "Test",
            ParentId = "1",
            Url = "Test URL Insert",
            Sort = 1,
            Icon = "Test Icon Insert",
            SiteMap = "Test Site",
            TypeMenu = "A",
            IsPublic = "1",
            CreateBy = "Amjad UT",
            CreateDate = DateTime.Now,
            EditBy = "Amjad UT",
            EditDate = DateTime.Now
        };

        Menu _menuUpdate = new Menu()
        {
            MenuId = "1",
            MenuName = "Test Update",
            ParentId = "1",
            Url = "Test URL Update",
            Sort = 1,
            Icon = "Test Icon Update",
            SiteMap = "Test Update",
            TypeMenu = "B",
            IsPublic = "2",
            EditBy = "Amjad UT",
            EditDate = DateTime.Now
        };

        Menu _menuGetDataById = new Menu()
        {
            MenuId = "1"
        };

        Menu _menuDeleteData = new Menu()
        {
            MenuId = "1"
        };
        #endregion

        #region Test Method

        [TestMethod]
        public void InsertDataMenuTest()
        {
            InsertDataMenuRequest request = new InsertDataMenuRequest();
            request.Menu = _menuValidData;
            InsertDataMenuResponse response = _menuService.InsertDataMenu(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed insert data");
        }

        [TestMethod]
        public void UpdateDataMenuTest()
        {
            UpdateDataMenuRequest request = new UpdateDataMenuRequest();
            request.Menu = _menuUpdate;
            UpdateDataMenuResponse response = _menuService.UpdateDataMenu(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed update data");
        }

        [TestMethod]
        public void GetDataByIdMenuTest()
        {
            GetDataMenuByIdRequest request = new GetDataMenuByIdRequest();
            request.menuId = _menuGetDataById.MenuId;
            GetDataMenuByIdResponse response = _menuService.GetDataMenuById(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed get all data");
        }

        [TestMethod]
        public void GetAllDataMenuTest()
        {
            GetAllDataMenuResponse response = _menuService.GetAllDataMenu();

            Assert.IsTrue(response.Messages.Count == 0, "Failed get all data");
        }

        [TestMethod]
        public void DeleteDataMenuTest()
        {
            GetDataMenuByIdRequest getRequest = new GetDataMenuByIdRequest();
            getRequest.menuId = _menuGetDataById.MenuId;

            GetDataMenuByIdResponse getResponse = _menuService.GetDataMenuById(getRequest);

            if (getResponse.Messages.Count != 0)
            {
                throw new Exception("Delete failed!");
            }
            else
            {
                DeleteDataMenuRequest request = new DeleteDataMenuRequest();

                request.Menu = getResponse.Menu;
                DeleteDataMenuResponse response = _menuService.DeleteDataMenu(request);
            }

        }
        #endregion
    }

}
