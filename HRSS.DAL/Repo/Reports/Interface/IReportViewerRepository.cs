using HRSS.DAL.Model.Reports;
using HRSS.DAL.Repo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Repo.Reports.Interface
{
    public interface IReportViewerRepository : IDALRepository<Report>
    {
        /// <summary>
        /// Get Data By Id Report
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        Report GetDataById(string reportId);
    }
}
