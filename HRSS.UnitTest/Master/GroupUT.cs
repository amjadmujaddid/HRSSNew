using HRSS.BLL.Master;
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
    public class GroupUT
    {
        #region Class Attribute and Property

        private IGroupService _groupService = new GroupService();

        #endregion

        #region TestData

        Group _groupValidData = new Group()
        {
            GroupId = "1001",
            GroupName = "Developer",
            CreateBy = "101",
            CreateDate = DateTime.Now,
            EditBy = "101",
            EditDate = DateTime.Now
        };

        Group _groupUpdateData = new Group()
        {
            GroupId = "1001",
            GroupName = "Developer Update",
            EditBy = "202",
            EditDate = DateTime.Now
        };

        Group _groupGetDataById = new Group()
        {
            GroupId = "1001"
        };

        Group _groupDeleteData = new Group()
        {
            GroupId = "1001"
        };

        Group _groupGetDataByFilter = new Group()
        {
            GroupId = "0001",
            GroupName = "Dev"
        };

        Group _groupGetDataFromGridSetting = new Group()
        {
            GroupId = "11",
            //GroupName = "Software Enginer"
            //sortColumn = "GroupName",
            //sortOrder = "asc",
            pageSize = 10,
            pageIndex = 1,
            totalRecords = 4
        };

        #endregion

        #region Test Method

        [TestMethod]
        public void InsertDataGroupTest()
        {
            string mess = "";
            InsertDataGroupRequest request = new InsertDataGroupRequest();
            request.Group = _groupValidData;
            InsertDataGroupResponse response = _groupService.InsertDataGroup(request);

            foreach (var item in response.Messages)
                mess += item + System.Environment.NewLine;

            Assert.IsTrue(response.Messages.Count == 0, mess);
        }

        [TestMethod]
        public void UpdateDataGroupTest()
        {
            UpdateDataGroupRequest request = new UpdateDataGroupRequest();
            request.Group = _groupUpdateData;
            UpdateDataGroupResponse response = _groupService.UpdateDataGroup(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed update data");
        }

        [TestMethod]
        public void GetDataGroupByIdTest()
        {
            GetDataGroupByIdRequest request = new GetDataGroupByIdRequest();
            request.groupId = _groupGetDataById.GroupId;
            GetDataGroupByIdResponse response = _groupService.GetDataGroupById(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed get data by Id");
        }

        [TestMethod]
        public void GetAllDataGroup()
        {
            GetAllDataGroupResponse response = _groupService.GetAllDataGroup();
            Assert.IsTrue(response.Messages.Count == 0, "Failed get all data");
        }

        [TestMethod]
        public void DeleteDataGroup()
        {
            GetDataGroupByIdRequest getRequest = new GetDataGroupByIdRequest();
            getRequest.groupId = _groupGetDataById.GroupId;

            GetDataGroupByIdResponse getResponse = _groupService.GetDataGroupById(getRequest);

            if (getResponse.Messages.Count != 0 || getResponse.Group == null)
            {
                throw new Exception("Delete failed!");
            }
            else
            {
                DeleteDataGroupRequest request = new DeleteDataGroupRequest();

                request.Group = getResponse.Group;
                DeleteDataGroupResponse response = _groupService.DeleteDataGroup(request);
            }
        }

        [TestMethod]
        public void GetDataGroupByFilterTest()
        {
            GetDataGroupByFilterRequest request = new GetDataGroupByFilterRequest();
            request.groupId = _groupGetDataByFilter.GroupId;
            request.groupName = _groupGetDataByFilter.GroupName;
            GetDataGroupByFilterResponse response = _groupService.GetDataGroupByFilter(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed get data by Filter");

        }

        [TestMethod]
        public void GetDataGroupFromGridSettingTest()
        {
            GetDataFromGridSettingRequest request = new GetDataFromGridSettingRequest();
            request.Group = _groupGetDataFromGridSetting;

            GetDataFromGridSettingResponse response = _groupService.GetDataFromGridSetting(request);

            Assert.IsTrue(response.Messages.Count == 0, "Failed get data from Grid Setting");
        }
        #endregion
    }
}
