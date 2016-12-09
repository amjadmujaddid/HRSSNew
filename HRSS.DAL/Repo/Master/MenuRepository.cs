using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master.Interface;
using HRSS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using HRSS.DAL.Validations.Common.Validations;

namespace HRSS.DAL.Repo.Master
{
    public class MenuRepository : IMenuRepository
    {
        #region Declare Variable

        private string spName;
        private string spType = string.Empty;
        private Dictionary<string, string> spParams = new Dictionary<string, string>();

        #endregion

        #region IDAL Repository

        public void Add(Menu entity)
        {
            using (var context = new HRSSContext<Menu>())
            {
                Menu data = context.DBEntities.Where(x => x.MenuId == entity.MenuId).FirstOrDefault();

                if (data != null)
                {
                    throw new Exception("Data Already Exist");
                }
                else
                {
                    context.DBEntities.Add(entity);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Menu entity)
        {
            using (var context = new HRSSContext<Menu>())
            {
                Menu data = context.DBEntities.Where(x => x.MenuId == entity.MenuId).FirstOrDefault();

                if (data == null)
                {
                    throw new Exception("Data Not Exist");
                }
                else
                {
                    context.DBEntities.Remove(data);
                    context.SaveChanges();
                }
            }
        }

        public List<Menu> GetAll()
        {
            using (var context = new HRSSContext<Menu>())
            {
                return context.Set<Menu>().ToList();
            }
        }

        public void Update(Menu entity)
        {
            using (var context = new HRSSContext<Menu>())
            {
                Menu data = context.DBEntities.Where(i => i.MenuId == entity.MenuId).FirstOrDefault();

                if (data == null)
                {
                    throw new Exception("Data Not Exist");
                }
                else
                {
                    data.MenuName = entity.MenuName;
                    data.ParentId = entity.ParentId;
                    data.Url = entity.Url;
                    data.Sort = entity.Sort;
                    data.Icon = entity.Icon;
                    data.SiteMap = entity.SiteMap;
                    data.TypeMenu = entity.TypeMenu;
                    data.IsPublic = entity.IsPublic;
                    data.EditBy = entity.EditBy;
                    data.EditDate = entity.EditDate;
                    context.SaveChanges();
                }
            }
        }

        #endregion

        #region IMenuRepository Implementation
        
        public Menu GetDataById(string menuId)
        {
            using (HRSSContext<Menu> context = new HRSSContext<Menu>())
            {
                return context.DBEntities.Where(i => i.MenuId == menuId).FirstOrDefault();
            }
        }

        #endregion
    }
}