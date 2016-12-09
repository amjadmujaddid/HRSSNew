using HRSS.BLL.Master;
using HRSS.BLL.Master.Contract;
using HRSS.DAL.Model.Master;
using HRSS.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.UnitTest.Master
{
    [TestClass]
    public class UserUT
    {
        #region Class Attribute and Property

        private IUserService _userService = new UserService();

        #endregion

        #region TestData

        User _userValidData = new User()
        {
            UserId = "101",
            UserName = "Amjad",
            Password = "KjsdSfHGGuIwQlKmRvXs",
            Images = "employee.jpg",
            GroupMenuId = "100",
            IsActive = "1",
            ThemeId = "1",
            SessionID = "88ArtYwuIopQkazM",
            LastLoginTime = DateTime.Now,
            PasswordExpired = DateTime.Now.AddMonths(1),
            CreateBy = "SQL",
            CreateDate = DateTime.Now,
            EditBy = "SQL",
            EditDate = DateTime.Now
        };

        User _userUpdateData = new User()
        {
            UserId = "101",
            UserName = "Amjad Update",
            Password = "UpdatePassword",
            Images = "Update.jpg",
            IsActive = "1",
            ThemeId = "1",
            GroupMenuId="200",
            SessionID = "Update",
            LastLoginTime = DateTime.Now,
            PasswordExpired = DateTime.Now.AddMonths(1),
            EditBy = "SQL",
            EditDate = DateTime.Now
        };

        User _userGetDataByUserId = new User()
        {
            UserId = "101"
        };

        User _userDeleteData = new User()
        {
            UserId = "101"
        };

        #endregion

        #region Test Method

        [TestMethod]
        public void InsertDataUserTest()
        {
            string mess = "";
            InsertDataUserRequest request = new InsertDataUserRequest();
            request.User = _userValidData;
            InsertDataUserResponse response = _userService.InsertDataUser(request);

            foreach (var item in response.Messages)
                mess += item + System.Environment.NewLine;

            Assert.IsTrue(response.Messages.Count == 0, mess);
        }

        [TestMethod]
        public void UpdateDataUserTest()
        {
            UpdateDataUserRequest request = new UpdateDataUserRequest();
            request.User = _userUpdateData;
            UpdateDataUserResponse response = _userService.UpdateDataUser(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed update data");
        }

        [TestMethod]
        public void GetDataUserByUserIdTest()
        {
            GetDataUserByUserIdRequest request = new GetDataUserByUserIdRequest();
            request.userId = _userGetDataByUserId.UserId;
            GetDataUserByUserIdResponse response = _userService.GetDataUserByUserId(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed get data by Id");
        }

        [TestMethod]
        public void GetAllDataUser()
        {
            GetAllDataUserResponse response = _userService.GetAllDataUser();
            Assert.IsTrue(response.Messages.Count == 0, "Failed get all data");
        }

        [TestMethod]
        public void DeleteDataUser()
        {
            GetDataUserByUserIdRequest getRequest = new GetDataUserByUserIdRequest();
            getRequest.userId = _userGetDataByUserId.UserId;

            GetDataUserByUserIdResponse getResponse = _userService.GetDataUserByUserId(getRequest);

            if (getResponse.Messages.Count != 0 || getResponse.User == null)
            {
                throw new Exception("Delete failed!");
            }
            else
            {
                DeleteDataUserRequest request = new DeleteDataUserRequest();

                request.User = getResponse.User;
                DeleteDataUserResponse response = _userService.DeleteDataUser(request);
            }
        }

        #endregion
    }
}
