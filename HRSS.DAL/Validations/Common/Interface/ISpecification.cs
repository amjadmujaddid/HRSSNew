using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations.Interface
{
    /// <summary>
    /// Interface untuk Spesifikasi validasi
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISpecification<in TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}
