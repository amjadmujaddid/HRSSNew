using HRSS.DAL.Validations.Common.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.BLL.Common
{
    public class ResponseBase
    {
        /// <summary>
        /// Simple class for message.
        /// </summary>
        //[DataContract(IsReference = true)]
        private List<string> _messages;

        public List<string> Messages
        {
            get { return _messages ?? (_messages = new List<string>()); }
        }
    }
}
