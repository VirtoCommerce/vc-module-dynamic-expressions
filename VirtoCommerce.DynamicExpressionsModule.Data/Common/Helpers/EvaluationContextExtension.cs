using System;
using System.Linq;
using VirtoCommerce.Domain.Common;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
    public static class EvaluationContextExtension
    {
        #region Dynamic expression evaluation helper methods
        public static bool UserGroupsContains(this EvaluationContextBase context, string group)
        {
            var retVal = context.UserGroups != null;
            if (retVal)
            {
                retVal = context.UserGroups.Any(x => string.Equals(x, group, StringComparison.InvariantCultureIgnoreCase));
            }
            return retVal;
        }
        #endregion

    }
}
