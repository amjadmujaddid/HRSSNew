using HRSS.BLL.Master.Contract;
using HRSS.BLL.Reports.Contract;
using HRSS.DAL.Model.Master;
using HRSS.DAL.Repo.Master;
using HRSS.DAL.Repo.Master.Interface;
using HRSS.DAL.Repo.Reports;
using HRSS.DAL.Repo.Reports.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HRSS.BLL.Reports
{
    public class ReportViewerService : IReportViewerService
    {
        #region Attribute Class

        IReportViewerRepository _reportViewerRepo;

        #endregion

        #region Constructor

        public ReportViewerService()
        {
            _reportViewerRepo = new ReportViewerRepository();
        }


        #endregion

        #region IReportService Implementation

        public GetAllDataReportViewerResponse GetAllDataReport()
        {
            var response = new GetAllDataReportViewerResponse();
            try
            {
                var _listReport = _reportViewerRepo.GetAll();
                response.ReportViewerList.AddRange(_listReport);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.ToString());
            }

            return response;
        }

        public GetAllDataReportViewerResponse GetAllDataReportViewer()
        {
            throw new NotImplementedException();
        }

        public GetDataReportViewerByIdResponse GetDataReportViewerById(GetDataReportViewerByIdRequest request)
        {
            var response = new GetDataReportViewerByIdResponse();
            try
            {
                using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                {
                    response.ReportViewer = _reportViewerRepo.GetDataById(request.ReportViewerId);
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
