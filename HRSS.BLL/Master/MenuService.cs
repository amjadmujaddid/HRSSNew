using HRSS.BLL.Master.Contract;
using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master;
using HRSS.DAL.Repo.Master.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace HRSS.BLL.Master
{
    public class MenuService : IMenuService
    {
        #region Attribute Class

        IMenuRepository _menuRepo;

        #endregion

        #region Constructor

        public MenuService()
        {
            _menuRepo = new MenuRepository();
        }

        #endregion

        #region IMenuService Implementation

        public GetAllDataMenuResponse GetAllDataMenu()
        {
            GetAllDataMenuResponse response = new GetAllDataMenuResponse();
            try
            {
                List<Menu> _listMenu = _menuRepo.GetAll();
                response.GroupList.AddRange(_listMenu);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public GetDataMenuByIdResponse GetDataMenuById(GetDataMenuByIdRequest request)
        {
            GetDataMenuByIdResponse response = new GetDataMenuByIdResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    response.Menu = _menuRepo.GetDataById(request.menuId);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public InsertDataMenuResponse InsertDataMenu(InsertDataMenuRequest request)
        {
            InsertDataMenuResponse response = new InsertDataMenuResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _menuRepo.Add(request.Menu);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public UpdateDataMenuResponse UpdateDataMenu(UpdateDataMenuRequest request)
        {
            UpdateDataMenuResponse response = new UpdateDataMenuResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _menuRepo.Update(request.Menu);
                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public DeleteDataMenuResponse DeleteDataMenu(DeleteDataMenuRequest request)
        {
            DeleteDataMenuResponse response = new DeleteDataMenuResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    _menuRepo.Delete(request.Menu);
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
