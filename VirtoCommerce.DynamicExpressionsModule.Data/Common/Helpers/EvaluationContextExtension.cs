using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using VirtoCommerce.Domain.Pricing.Model;

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
                retVal = context.UserGroups.Any(x => String.Equals(x, group, StringComparison.InvariantCultureIgnoreCase));
            }
            return retVal;
        }
        #endregion

    }
}
