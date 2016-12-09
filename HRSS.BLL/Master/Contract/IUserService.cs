using HRSS.BLL.Common;
using HRSS.DAL.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.BLL.Master.Contract
{
    #region Definition

    public interface IUserService
    {
        /// <summary>
        /// Get All Data User
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        GetAllDataUserResponse GetAllDataUser();

        /// <summary>
        /// Get Data User By UserId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetDataUserByUserIdResponse GetDataUserByUserId(GetDataUserByUserIdRequest request);

        /// <summary>
        /// Insert Data User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        InsertDataUserResponse InsertDataUser(InsertDataUserRequest request);

        /// <summary>
        /// Update Data User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        UpdateDataUserResponse UpdateDataUser(UpdateDataUserRequest request);

        /// <summary>
        /// Delete Data User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteDataUserResponse DeleteDataUser(DeleteDataUserRequest request);
    }

    #endregion

    #region Message

    public class GetAllDataUserRequest
    {

    }

    public class GetAllDataUserResponse : ResponseBase
    {
        private List<User> _userList;

        public List<User> UserList
        {
            get { return _userList ?? (_userList = new List<User>()); }
        }
    }

    public class GetDataUserByUserIdRequest
    {
        public string userId { get; set; }
    }

    public class GetDataUserByUserIdResponse : ResponseBase
    {
        public User User { get; set; }
    }

    public class InsertDataUserRequest
    {
        public User User { get; set; }
    }

    public class InsertDataUserResponse : ResponseBase
    {

    }

    public class UpdateDataUserRequest
    {
        public User User { get; set; }
    }

    public class UpdateDataUserResponse : ResponseBase
    {
        public User User { get; set; }
    }

    public class DeleteDataUserRequest
    {
        public User User { get; set; }
    }

    public class DeleteDataUserResponse : ResponseBase
    {

    }

    #endregion
}
