using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master;
using HRSS.DAL.Repo.Master.Interface;
using HRSS.DAL.Validations;
using HRSS.DAL.Validations.Common.Validations;
using HRSS.DAL.Validations.Common.Validations.Interface;
using HRSS.Utility;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace HRSS.BLL.Master
{
    public class GroupService : IGroupService
    {
        #region Attribute Class

        IGroupRepository _groupRepo;
        private DateTime dtStart = DateTime.Now;
        #endregion

        #region Constructor

        public GroupService()
        {
            _groupRepo = new GroupRepository();
        }

        #endregion

        #region IGroupService Implementation

        public GetAllDataGroupResponse GetAllDataGroup()
        {
            GetAllDataGroupResponse response = new GetAllDataGroupResponse();
            try
            {
                List<Group> _listGroup = _groupRepo.GetAll();
                response.GroupList.AddRange(_listGroup);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }

            return response;
        }

        public GetDataGroupByIdResponse GetDataGroupById(GetDataGroupByIdRequest request)
        {
            GetDataGroupByIdResponse response = new GetDataGroupByIdResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    response.Group = _groupRepo.GetDataById(request.groupId);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }

            return response;
        }

        public InsertDataGroupResponse InsertDataGroup(InsertDataGroupRequest request)
        {            
            var valid = new ValidationHelper().Initialize(request.Group);            

            InsertDataGroupResponse response = new InsertDataGroupResponse();
            try
            {
                if (!valid.IsValid)
                {
                    foreach (var error in valid.Errors)
                        response.Messages.Add(error.Message);
                }
                else
                {
                    using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                    {
                        SecurityHelper.InitializeAntiXSS(request.Group);
                        _groupRepo.Add(request.Group);
                        transScope.Complete();
                    }
                }                
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }
            return response;
        }

        public UpdateDataGroupResponse UpdateDataGroup(UpdateDataGroupRequest request)
        {
            UpdateDataGroupResponse response = new UpdateDataGroupResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _groupRepo.Update(request.Group);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }
            return response;
        }

        public DeleteDataGroupResponse DeleteDataGroup(DeleteDataGroupRequest request)
        {
            DeleteDataGroupResponse response = new DeleteDataGroupResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _groupRepo.Delete(request.Group);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }
            return response;
        }

        public GetDataGroupByFilterResponse GetDataGroupByFilter(GetDataGroupByFilterRequest request)
        {
            GetDataGroupByFilterResponse response = new GetDataGroupByFilterResponse();
            try
            {
                List<Group> _listGroup = _groupRepo.GetDataByFilter(request.groupId, request.groupName);
                response.GroupList.AddRange(_listGroup);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }
            return response;
        }

        public GetDataFromGridSettingResponse GetDataFromGridSetting(GetDataFromGridSettingRequest request)
        {
            GetDataFromGridSettingResponse response = new GetDataFromGridSettingResponse();
            try
            {
                List<Group> _listGroup = _groupRepo.GetDataFromGridSetting(request.Group);
                response.GroupList.AddRange(_listGroup);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
                LogFileException.LogError(ex, dtStart);
            }
            return response;
        }

        #endregion
    }
}
