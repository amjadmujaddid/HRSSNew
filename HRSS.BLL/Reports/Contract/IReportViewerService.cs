using HRSS.BLL.Common;
using HRSS.DAL.Model.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.BLL.Reports.Contract
{
    #region Service Definition

    public interface IReportViewerService
    {
        /// <summary>
        /// Get All Data ReportViewer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetAllDataReportViewerResponse GetAllDataReportViewer();

        /// <summary>
        /// Get Data ReportViewer By Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetDataReportViewerByIdResponse GetDataReportViewerById(GetDataReportViewerByIdRequest request);

    }

    #endregion

    #region Message

    public class GetAllDataReportViewerRequest
    {

    }

    public class GetAllDataReportViewerResponse : ResponseBase
    {
        private List<Report> _ReportViewerList;

        public List<Report> ReportViewerList
        {
            get { return _ReportViewerList ?? (_ReportViewerList = new List<Report>()); }
        }
    }

    public class GetDataReportViewerByIdRequest
    {
        public string ReportViewerId { get; set; }
    }

    public class GetDataReportViewerByIdResponse : ResponseBase
    {
        public Report ReportViewer { get; set; }
    }
    #endregion
}
