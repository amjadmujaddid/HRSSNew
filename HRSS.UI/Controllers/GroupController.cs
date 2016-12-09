using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRSS.BLL.Master;
using HRSS.DAL.Model.Master;
using HRSS.Utility;
using System.IO;
using HRSS.BLL.Master.Contract;
using static HRSS.Utility.SecurityHelper;
using MvcJqGrid;

namespace HRSS.UI.Controllers
{
    [RoutePrefix("group")]

    public class GroupController : Controller
    {
        #region Class Attribute and Property

        private IGroupService _groupService = new GroupService();


        #endregion
        public ActionResult List()
        {
            return View();
        }

        [InitializeAntiCSRF]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }



        //[Route("grouplist")]
        //public JsonResult GetAllData(Group group)
        //{

        //    //Group groupTest = new Group();
        //    //groupTest.GroupId = "11";
        //    //groupTest.pageSize = 10;
        //    //groupTest.pageIndex = 1;
        //    //groupTest.totalRecords = 4;

        //    List<Group> listGroup = new List<Group>();

        //    GetDataFromGridSettingRequest request = new GetDataFromGridSettingRequest();
        //    request.Group.GroupId = group.GroupId;
        //    request.Group.pageSize = group.pageSize;
        //    request.Group.pageIndex = group.pageIndex;
        //    request.Group.totalRecords = group.totalRecords;
        //    GetDataFromGridSettingResponse response = _groupService.GetDataFromGridSetting(request);
        //    listGroup.AddRange(response.GroupList);
        //    int totalRecords = request.Group.totalRecords;


        //    //var jsonData = new
        //    //{
        //    //    total = totalRecords / group.pageSize + 1,
        //    //    page = group.pageSize,
        //    //    records = totalRecords,
        //    //    rows = (
        //    //    from g in listGroup
        //    //    select new
        //    //    {
        //    //        id = g.GroupId,
        //    //        GroupName = g.GroupName,
        //    //    })
        //    //};
        //    return Json(listGroup, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// For Get Group Data
        /// </summary>
        /// <returns></returns>
        [Route("grouplist")]
        public JsonResult GetAllData()
        {
            List<Group> listGroup = new List<Group>();
            GetAllDataGroupResponse response = _groupService.GetAllDataGroup();
            listGroup.AddRange(response.GroupList);

            var jsonData = new
            {
                total = 1,
                page = 1,
                records = listGroup.Count,
                rows = (
                  from list in listGroup
                  select new
                  {
                      id = list.GroupId,
                      cell = new string[] {
                        list.GroupId.ToString(), list.GroupName
                    }
                  }).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Insert New Group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [Route("save")]
        [HttpPost]        
        public JsonResult SaveGroup(Group group, string mode)
        {
            if (mode == "add")
            {
                InsertDataGroupRequest request = new InsertDataGroupRequest();
                request.Group = group;
                InsertDataGroupResponse response = _groupService.InsertDataGroup(request);

                return Json(response.Messages.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                UpdateDataGroupRequest request = new UpdateDataGroupRequest();
                request.Group = group;
                UpdateDataGroupResponse response = _groupService.UpdateDataGroup(request);

                return Json(response.Messages.ToList(), JsonRequestBehavior.AllowGet);
            }

        }


        [Route("getbyid/{groupId}")]
        [HttpPost]
        public JsonResult GetById(Group Group)
        {
            GetDataGroupByIdRequest request = new GetDataGroupByIdRequest();
            request.groupId = Group.GroupId;
            GetDataGroupByIdResponse response = _groupService.GetDataGroupById(request);

            return Json(response.Group, JsonRequestBehavior.AllowGet);

        }

        [Route("delete/{groupId}")]
        [HttpPost]
        public JsonResult DeleteDataGroup(Group group)
        {
            GetDataGroupByIdRequest getRequest = new GetDataGroupByIdRequest();
            getRequest.groupId = group.GroupId;

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
                return Json(response.Messages.ToList(), JsonRequestBehavior.AllowGet);
            }

        }


        /// <summary>
        /// Combobox Group
        /// </summary>
        /// <returns></returns>
        [Route("cbogroup")]
        public JsonResult ComboboxGroup()
        {

            List<Group> listGroup = new List<Group>();
            GetAllDataGroupResponse response = _groupService.GetAllDataGroup();
            listGroup.AddRange(response.GroupList);

            return Json(listGroup.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}