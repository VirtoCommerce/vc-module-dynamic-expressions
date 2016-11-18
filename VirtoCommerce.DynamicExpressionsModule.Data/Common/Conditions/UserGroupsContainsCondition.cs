using System;
using System.Linq;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
	//User groups contains condition
	public class UserGroupsContainsCondition : DynamicExpression, IConditionExpression
	{
        public string Group { get; set; }

        #region IConditionExpression Members
        /// <summary>
        ///  ((EvaluationContextBase)x).UserGroupsContains
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
		{
            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(EvaluationContextBase));
            var methodInfo = typeof(EvaluationContextExtension).GetMethod("UserGroupsContains");

            var methodCall = linq.Expression.Call(null, methodInfo, castOp, linq.Expression.Constant(Group));

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(methodCall, paramX);

            return retVal;
        }

		#endregion
	}
}
