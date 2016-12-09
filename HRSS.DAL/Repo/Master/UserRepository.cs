using HRSS.DAL.Repo.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSS.DAL.Model.Master;

namespace HRSS.DAL.Repo.Master
{
    public class UserRepository : IUserRepository
    {
        #region IDAL Repository

        public void Add(User entity)
        {
            using (var context = new HRSSContext<User>())
            {
                User data = context.DBEntities.Where(x => x.UserId == entity.UserId).FirstOrDefault();

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

        public void Delete(User entity)
        {
            using (var context = new HRSSContext<User>())
            {
                User data = context.DBEntities.Where(x => x.UserId == entity.UserId).FirstOrDefault();

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

        public List<User> GetAll()
        {
            using (var context = new HRSSContext<User>())
            {
                return context.Set<User>().ToList();
            }
        }

        public void Update(User entity)
        {
            using (var context = new HRSSContext<User>())
            {
                User data = context.DBEntities.Where(i => i.UserId == entity.UserId).FirstOrDefault();

                if (data == null)
                {
                    throw new Exception("Data Not Exist");
                }
                else
                {
                    data.UserName = entity.UserName;
                    data.Password = entity.Password;
                    data.Images = entity.Images;
                    data.GroupMenuId = entity.GroupMenuId;
                    data.IsActive = entity.IsActive;
                    data.ThemeId = entity.ThemeId;
                    data.SessionID = entity.SessionID;
                    data.LastLoginTime = entity.LastLoginTime;
                    data.PasswordExpired = entity.PasswordExpired;
                    data.EditBy = entity.EditBy;
                    data.EditDate = entity.EditDate;
                    context.SaveChanges();
                }
            }
        }

        #endregion

        #region IUserRepository Implementation

        public User GetDataByUserId(string userId)
        {
            using (HRSSContext<User> context = new HRSSContext<User>())
            {
                return context.DBEntities.Where(i => i.UserId == userId).FirstOrDefault();
            }
        }

        #endregion
    }
}
