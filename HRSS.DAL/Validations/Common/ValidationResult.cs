﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSS.DAL.Validations.Common.Validations
{
    /// <summary>
    /// ValidationResult adalah class yang berisi hasil validasi , properti validasi , dll
    /// </summary>
    public class ValidationResult
    {
        private readonly List<ValidationError> _erros;
        private string Message { get; set; }
        public bool IsValid { get { return !_erros.Any(); } }
        public IEnumerable<ValidationError> Errors { get { return _erros; } }

        public ValidationResult()
        {
            _erros = new List<ValidationError>();
        }

        public ValidationResult Add(string errorMessage)
        {
            _erros.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            _erros.Add(error);
            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null) return this;

            foreach (var result in validationResults.Where(x => x != null))
                _erros.AddRange(result.Errors);

            return this;
        }

        public ValidationResult Remove(ValidationError error)
        {
            if (_erros.Contains(error))
                _erros.Remove(error);
            return this;
        }
    }
}
