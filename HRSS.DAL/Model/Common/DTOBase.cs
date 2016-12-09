using System;

namespace HRSS.DAL.Model.Common
{
    /// <summary>
    /// Provides base class for DTO
    /// </summary>
    [Serializable]
    public class DTOBase
    {
        /// <summary>
        /// Global Variable
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public string EditBy { get; set; }

        /// <summary>
        /// DTO for GridSetting Controller
        /// </summary>
        public string sortColumn { get; set; }
        public string sortOrder { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public int totalRecords { get; set; }
    }
}
