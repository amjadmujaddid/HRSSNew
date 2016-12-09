using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Repo.Master.Interface
{
    public interface IUserRepository : IDALRepository<User>
    {
        /// <summary>
        /// Get Data By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetDataByUserId(string userId);
    }
}
