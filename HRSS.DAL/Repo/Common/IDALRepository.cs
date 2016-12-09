using HRSS.DAL.Validations.Common.Validations;
using System.Collections.Generic;

namespace HRSS.DAL.Repo.Common
{
    public interface IDALRepository<T> where T : class
    {
        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>List of entities</returns>
        List<T> GetAll();

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity Object</param>
        void Add(T entity);        

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity Object</param>
        void Update(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity Ojbect</param>
        void Delete(T entity);
    }
}
