using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations.Interface
{
    /// <summary>
    /// Interface Validation is Base Class for Everything need validation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}
