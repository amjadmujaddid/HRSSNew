using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Common;
using System.Collections.Generic;

namespace HRSS.DAL.Repo.Master.Interface
{
    public interface IGroupRepository : IDALRepository<Group>
    {
        /// <summary>
        /// Get Data By Id Group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Group GetDataById(string groupId);

        /// <summary>
        /// Get List Data Group By Filter
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        List<Group> GetDataByFilter(string groupId, string groupName);

        /// <summary>
        /// Get Data From Grid Setting
        /// </summary>
        /// <param name="gridSetting"></param>
        /// <returns></returns>
        List<Group> GetDataFromGridSetting(Group gridSetting);
    }
}
