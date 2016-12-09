using HRSS.DAL.Model.Common;
using HRSS.DAL.Validations.Common.Validations;
using HRSS.DAL.Validations.Common.Validations.Interface;
using HRSS.DAL.Validations.Master;

namespace HRSS.DAL.Model.Master
{
    public class Group : DTOBase, ISelfValidation
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                var fiscal = new GroupIsValidation();
                ValidationResult = fiscal.Valid(this);
                return ValidationResult.IsValid;
            }
        }

    }
}
