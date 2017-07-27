using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //InStock quantity is []
    public class ConditionInStockQuantity : ConditionBase, IConditionExpression
    {
        public int Quantity { get; set; }

        public bool Exactly { get; set; }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).IsItemsInStockQuantity(Exactly, Quantity)
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
        {
            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
            var quantity = linq.Expression.Constant(Quantity);
            var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("IsItemsInStockQuantity");

            var equalsOrAtLeast = Exactly ? linq.Expression.Constant(true) : linq.Expression.Constant(false);
            var methodCall = linq.Expression.Call(null, methodInfo, castOp, equalsOrAtLeast, quantity);

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(methodCall, paramX);

            return retVal;
        }

        #endregion
    }
}
