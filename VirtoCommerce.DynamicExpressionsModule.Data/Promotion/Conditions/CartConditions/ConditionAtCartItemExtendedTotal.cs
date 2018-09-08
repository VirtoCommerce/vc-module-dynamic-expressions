using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //Line item subtotal is []
    public class ConditionAtCartItemExtendedTotal : ConditionBase, IConditionExpression
    {
        public decimal LineItemTotal { get; set; }

        public decimal LineItemTotalSecond { get; set; }

        public string CompareCondition { get; set; }

        public bool Exactly { get; set; }

        public ConditionAtCartItemExtendedTotal()
        {
            CompareCondition = "";
        }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).IsAnyLineItemTotal(LineItemTotal, LineItemTotalSecond, CompareCondition,  ExcludingCategoryIds, ExcludingProductIds)
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
        {
            if (CompareCondition == "")
                CompareCondition = "AtLeast";

            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
            var lineItemTotal = linq.Expression.Constant(LineItemTotal);
            var lineItemTotalSecond = linq.Expression.Constant(LineItemTotalSecond);
            var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("IsAnyLineItemExtendedTotalNew");
            var compareCondition = linq.Expression.Constant(CompareCondition);

            var methodCall = linq.Expression.Call(null, methodInfo, castOp, lineItemTotal, lineItemTotalSecond, compareCondition, GetNewArrayExpression(ExcludingCategoryIds),
                                                                      GetNewArrayExpression(ExcludingProductIds));

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(methodCall, paramX);

            return retVal;
        }

        #endregion
    }
}
