using HRSS.DAL.Model.Master;
using HRSS.DAL.Validations.Common.Validations.Interface;
using System;

namespace HRSS.DAL.Validations.Master
{
    /// <summary>
    /// GroupSpecification adalah class yang isinya kumpulan validasi
    /// </summary>
    public class GroupSpecification : ISpecification<Group>
    {
        int index = 0;
        public GroupSpecification(int x)
        {
            index = x;
        }
        public bool IsSatisfiedBy(Group group)
        {
            bool res = false;
            switch (index)
            {
                case 1:
                    res = !String.IsNullOrEmpty(group.GroupName) && !String.IsNullOrWhiteSpace(group.GroupName);
                    break;
                case 2:
                    res = !String.IsNullOrEmpty(group.GroupId) && !String.IsNullOrWhiteSpace(group.GroupId);
                    break;
            }
            return res;
        }
    }
}
