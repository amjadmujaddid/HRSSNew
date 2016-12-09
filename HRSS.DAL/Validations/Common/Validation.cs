using HRSS.DAL.Validations.Common.Validations.Interface;
using System;
using System.Collections.Generic;

namespace HRSS.DAL.Validations.Common.Validations
{
    /// <summary>
    /// Class Validation is Base Class for Everything need validation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Validation<TEntity> : IValidation<TEntity> where TEntity : class
    {
        private readonly Dictionary<string, IValidationRule<TEntity>> _validationsRules;

        public Validation()
        {
            _validationsRules = new Dictionary<string, IValidationRule<TEntity>>();
        }

        /// <summary>
        /// AddRule for add Validation
        /// </summary>
        /// <param name="validationRule"></param>
        protected virtual void AddRule(IValidationRule<TEntity> validationRule)
        {
            var ruleName = validationRule.GetType() + Guid.NewGuid().ToString("D");
            _validationsRules.Add(ruleName, validationRule);
        }

        /// <summary>
        /// RemoveRule for remove Validation
        /// </summary>
        /// <param name="ruleName"></param>
        protected virtual void RemoveRule(string ruleName)
        {
            _validationsRules.Remove(ruleName);
        }

        /// <summary>
        /// valid untuk mengambil validation key
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual ValidationResult Valid(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var key in _validationsRules.Keys)
            {
                var rule = _validationsRules[key];
                if (!rule.Valid(entity))
                    result.Add(new ValidationError(rule.ErrorMessage));
            }
            return result;
        }
    }
}
