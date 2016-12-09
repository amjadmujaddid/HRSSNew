using HRSS.DAL.Model.Reports;
using HRSS.DAL.Repo.Reports.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSS.DAL.Validations.Common.Validations;

namespace HRSS.DAL.Repo.Reports
{
    public class ReportViewerRepository : IReportViewerRepository
    {

        #region Declare Variable

        private string spName;
        private string spType = string.Empty;
        private Dictionary<string, string> spParams = new Dictionary<string, string>();

        #endregion

        #region IDAL Repository

        public List<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Report entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Report entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Report entity)
        {
            throw new NotImplementedException();

        }

        #endregion

        #region IReportViewerRepository Implementation

        public Report GetDataById(string reportId)
        {
            using (HRSSContext<Report> context = new HRSSContext<Report>())
            {
                return context.DBEntities.Where(i => i.ReportId == Convert.ToInt16(reportId)).FirstOrDefault();
            }
        }
        #endregion

    }
}
