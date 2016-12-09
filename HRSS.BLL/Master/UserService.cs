using HRSS.BLL.Master.Contract;
using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master;
using HRSS.DAL.Repo.Master.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.Entity.Validation;

namespace HRSS.BLL.Master
{

    public class UserService : IUserService
    {
        #region Attribute Class

        IUserRepository _userRepo;

        #endregion

        #region Constructor

        public UserService()
        {
            _userRepo = new UserRepository();
        }

        #endregion

        #region IUserService Implementation

        public GetAllDataUserResponse GetAllDataUser()
        {
            GetAllDataUserResponse response = new GetAllDataUserResponse();
            try
            {
                List<User> _listUser = _userRepo.GetAll();
                response.UserList.AddRange(_listUser);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public GetDataUserByUserIdResponse GetDataUserByUserId(GetDataUserByUserIdRequest request)
        {
            GetDataUserByUserIdResponse response = new GetDataUserByUserIdResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    response.User = _userRepo.GetDataByUserId(request.userId);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public InsertDataUserResponse InsertDataUser(InsertDataUserRequest request)
        {
            InsertDataUserResponse response = new InsertDataUserResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _userRepo.Add(request.User);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public UpdateDataUserResponse UpdateDataUser(UpdateDataUserRequest request)
        {
            UpdateDataUserResponse response = new UpdateDataUserResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _userRepo.Update(request.User);
                    transScope.Complete();
                }
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public DeleteDataUserResponse DeleteDataUser(DeleteDataUserRequest request)
        {
            DeleteDataUserResponse response = new DeleteDataUserResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _userRepo.Delete(request.User);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }
        #endregion
    }
}
