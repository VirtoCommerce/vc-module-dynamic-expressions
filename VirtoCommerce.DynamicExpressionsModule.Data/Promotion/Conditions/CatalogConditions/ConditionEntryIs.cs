using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;
using VirtoCommerce.DynamicExpressionsModule.Data.Common.Extensions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //Product is []
    public class ConditionEntryIs : DynamicExpression, IConditionExpression
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string[] ProductIds { get; set; }
        public string[] ProductNames { get; set; }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).IsItemInProduct(ProductId)
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
        {
            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
            linq.MethodCallExpression methodCall = null;
            if (ProductIds != null)
            {
                var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("IsItemInProducts");
                methodCall = linq.Expression.Call(null, methodInfo, castOp, ProductIds.GetNewArrayExpression());
            }
            else if (!string.IsNullOrEmpty(ProductId))
            {
                var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("IsItemInProduct");
                methodCall = linq.Expression.Call(null, methodInfo, castOp, linq.Expression.Constant(ProductId));
            }

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(methodCall, paramX);

            return retVal;
        }

        #endregion
    }
}
