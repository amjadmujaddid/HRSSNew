using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations.Interface
{
    /// <summary>
    /// ISelfValidation untuk validasi di class tersebut
    /// </summary>
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}
