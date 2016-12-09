using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Common;

namespace HRSS.DAL.Repo.Master.Interface
{
    public interface IMenuRepository : IDALRepository<Menu>
    {
        /// <summary>
        /// Get Data By Id Menu
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Menu GetDataById(string menuId);
    }
}
