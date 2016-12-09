using HRSS.DAL.Model.Master;
using HRSS.DAL.Validations.Common.Validations;

namespace HRSS.DAL.Validations.Master
{
    /// <summary>
    /// GroupIsValidation is Implement validations
    /// </summary>
    public class GroupIsValidation : Validation<Group>
    {
        public GroupIsValidation()
        {
            base.AddRule(new ValidationRule<Group>(new GroupSpecification(1), ValidationMessages.fldGroupName));
            base.AddRule(new ValidationRule<Group>(new GroupSpecification(2), ValidationMessages.fldGroupId));
        }
    }
}
