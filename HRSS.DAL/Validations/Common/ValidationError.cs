using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations
{
    /// <summary>
    /// ValidatioError is Attribut Error for Validation 
    /// </summary>
    public class ValidationError
    {
        public string Message { get; set; }
        public ValidationError(string message)
        {
            Message = message;
        }
    }
}
