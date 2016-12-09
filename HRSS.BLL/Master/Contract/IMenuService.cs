using HRSS.BLL.Common;
using HRSS.DAL.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.BLL.Master.Contract
{
    #region Service Definition

    public interface IMenuService
    {
        /// <summary>
        /// Get All Data Menu
        /// </summary>
        /// <returns></returns>
        GetAllDataMenuResponse GetAllDataMenu();

        /// <summary>
        /// Get Data Menu By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetDataMenuByIdResponse GetDataMenuById(GetDataMenuByIdRequest request);

        /// <summary>
        /// Insert Data Menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        InsertDataMenuResponse InsertDataMenu(InsertDataMenuRequest request);

        /// <summary>
        /// Update Data Menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UpdateDataMenuResponse UpdateDataMenu(UpdateDataMenuRequest request);

        /// <summary>
        /// Delete Data Menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteDataMenuResponse DeleteDataMenu(DeleteDataMenuRequest request);

    }

    #endregion

    #region Message

    public class GetAllDataMenuRequest
    {

    }

    public class GetAllDataMenuResponse : ResponseBase
    {
        private List<Menu> _groupList;

        public List<Menu> GroupList
        {
            get { return _groupList ?? (_groupList = new List<Menu>()); }
        }
    }

    public class GetDataMenuByIdRequest
    {
        public string menuId { get; set; }
    }

    public class GetDataMenuByIdResponse : ResponseBase
    {
        public Menu Menu { get; set; }
    }

    public class InsertDataMenuRequest
    {
        public Menu Menu { get; set; }
    }

    public class InsertDataMenuResponse : ResponseBase
    {

    }

    public class UpdateDataMenuRequest
    {
        public Menu Menu { get; set; }
    }

    public class UpdateDataMenuResponse : ResponseBase
    {
        public Menu Menu { get; set; }
    }

    public class DeleteDataMenuRequest
    {
        public Menu Menu { get; set; }
    }

    public class DeleteDataMenuResponse : ResponseBase
    {

    }

    #endregion
}
