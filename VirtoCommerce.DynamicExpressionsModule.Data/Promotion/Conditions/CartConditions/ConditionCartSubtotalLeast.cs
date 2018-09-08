using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
	//Cart subtotal is []
	public class ConditionCartSubtotalLeast : ConditionBase, IConditionExpression
	{
		public decimal SubTotal { get; set; }

        public decimal SubTotalSecond { get; set; }

        public bool Exactly { get; set; }

        public string CompareCondition { get; set; }

        public ConditionCartSubtotalLeast()
        {
            CompareCondition = "";
        }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).GetCartTotalWithExcludings(ExcludingCategoryIds, ExcludingProductIds) > SubTotal
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
		{
            if (CompareCondition == "")
                CompareCondition = "AtLeast";

            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
			var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
			var subTotal = linq.Expression.Constant(SubTotal);
            var subTotalSecond = linq.Expression.Constant(SubTotalSecond);
            var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("GetCartTotalWithExcludings");

			var methodCall = linq.Expression.Call(null, methodInfo, castOp, GetNewArrayExpression(ExcludingCategoryIds),
																	  GetNewArrayExpression(ExcludingProductIds));

            var binaryOp = CompareCondition == "Exactly" ? linq.Expression.Equal(methodCall, subTotal) :
                CompareCondition == "Between" ? linq.Expression.And(linq.Expression.GreaterThanOrEqual(methodCall, subTotal), linq.Expression.LessThanOrEqual(methodCall, subTotalSecond)) :
                CompareCondition == "AtLeast" ? linq.Expression.GreaterThanOrEqual(methodCall, subTotal) :
                CompareCondition == "IsLessThanOrEqual" ? linq.Expression.LessThanOrEqual(methodCall, subTotal) : null;

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(binaryOp, paramX);

			return retVal;
		}

		#endregion
	}
}
