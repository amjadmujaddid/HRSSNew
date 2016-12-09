using HRSS.DAL.Validations.Common.Validations;
using HRSS.DAL.Validations.Common.Validations.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations
{
    /// <summary>
    /// ValidationHelper is class for Initialize Validation 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationHelper
    {
        private readonly ValidationResult _validationResult;
        protected ValidationResult ValidationResult
        {
            get { return _validationResult; }
        }

        public ValidationHelper()
        {
            _validationResult = new ValidationResult();
        }

        /// <summary>
        /// Initialize Validation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Initialize<T> (T entity) where T : class
        {
            var valid = new ValidationResult();
            if (!ValidationResult.IsValid)
                valid = ValidationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.IsValid)
                valid = selfValidationEntity.ValidationResult;

            return valid;
        }
    }
}
