using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations.Interface
{
    /// <summary>
    /// ValidationRule is Class that function to Add Validation to baseClass
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IValidationRule<in TEntity>
    {
        string ErrorMessage { get; }
        bool Valid(TEntity entity);
    }
}
