using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master.Interface;
using HRSS.DAL.Validations.Common.Validations;
using HRSS.DAL.Validations.Common.Validations.Interface;
using HRSS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Repo.Master
{
    public class GroupRepository : IGroupRepository
    {
        #region Declare Variable

        private string spName;
        private string spType = string.Empty;
        private Dictionary<string, string> spParams = new Dictionary<string, string>();

        #endregion

        #region IDAL Repository

        public void Add(Group entity)
        {
            using (var context = new HRSSContext<Group>())
            {
                Group data = context.DBEntities.Where(x => x.GroupId == entity.GroupId).FirstOrDefault();

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

        public void Delete(Group entity)
        {
            using (var context = new HRSSContext<Group>())
            {
                Group data = context.DBEntities.Where(x => x.GroupId == entity.GroupId).FirstOrDefault();

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

        public List<Group> GetAll()
        {
            using (var context = new HRSSContext<Group>())
            {
                return context.Set<Group>().ToList();
            }
        }

        public void Update(Group entity)
        {
            using (var context = new HRSSContext<Group>())
            {
                Group data = context.DBEntities.Where(i => i.GroupId == entity.GroupId).FirstOrDefault();

                if (data == null)
                {
                    throw new Exception("Data Not Exist");
                }
                else
                {
                    data.GroupName = entity.GroupName;
                    data.EditBy = entity.EditBy;
                    data.EditDate = entity.EditDate;
                    context.SaveChanges();
                }
            }
        }

        #endregion

        #region IGroupRepository Implementation

        public Group GetDataById(string groupId)
        {
            using (HRSSContext<Group> context = new HRSSContext<Group>())
            {
                return context.DBEntities.Where(i => i.GroupId == groupId).FirstOrDefault();
            }
        }

        public List<Group> GetDataByFilter(string groupId, string groupName)
        {
            using (HRSSContext<Group> context = new HRSSContext<Group>())
            {
                return context.DBEntities.Where(i => i.GroupId.Contains(groupId) || i.GroupName.Contains(groupName)).ToList();
            }
        }

        public List<Group> GetDataFromGridSetting(Group gridSetting)
        {
            using (var context = new HRSSContext<Group>())
            {
                var result = context.Set<Group>().ToList();

                //// Filter field DataBind JqGrid
                if (!string.IsNullOrEmpty(gridSetting.GroupId))
                {
                    result = (gridSetting.GroupId != string.Empty) ? result.Where(x => x.GroupId.ToLower().Contains(gridSetting.GroupId.ToLower())).ToList() : result.ToList();
                }
                if (!string.IsNullOrEmpty(gridSetting.GroupName))
                {
                    result = (gridSetting.GroupName != string.Empty) ? result.Where(x => x.GroupName.ToLower().Contains(gridSetting.GroupName.ToLower())).ToList() : result.ToList();
                }

                // SortOrder field DataBind JqGrid
                switch (gridSetting.sortColumn)
                {
                    case "GroupId":
                        result = (gridSetting.sortOrder == "desc") ? result.OrderByDescending(x => x.GroupId).ToList() : result.OrderBy(x => x.GroupId).ToList();
                        break;

                    case "GroupName":
                        result = (gridSetting.sortOrder == "desc") ? result.OrderByDescending(x => x.GroupName).ToList() : result.OrderBy(x => x.GroupName).ToList();
                        break;
                    default:
                        break;
                }

                // Paging total Record
                gridSetting.totalRecords = result.Count();

                //return result;
                return result.Skip((gridSetting.pageIndex - 1) * gridSetting.pageSize).Take(gridSetting.pageSize).ToList();
            }
        }

        #endregion

    }
}
