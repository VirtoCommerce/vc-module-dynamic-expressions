using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
	//[] [] items of category are in shopping cart
	public class ConditionAtNumItemsInCategoryAreInCart : ConditionBase, IConditionExpression
	{
		public int NumItem { get; set; }

        public int NumItemSecond { get; set; }

        public string CompareCondition { get; set; }

        public bool Exactly { get; set; }

		public string CategoryId { get; set; }

		public string CategoryName { get; set; }

        public ConditionAtNumItemsInCategoryAreInCart()
        {
            CompareCondition = "AtLeast";
        }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).GetCartItemsOfCategoryQuantity(CategoryId, ExcludingCategoryIds, ExcludingProductIds) > NumItem
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
		{
			var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
			var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
			var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("GetCartItemsOfCategoryQuantity");
			var methodCall = linq.Expression.Call(null, methodInfo, castOp, linq.Expression.Constant(CategoryId), GetNewArrayExpression(ExcludingCategoryIds),
																	  GetNewArrayExpression(ExcludingProductIds));
			var numItem = linq.Expression.Constant(NumItem);
            var numItemSecond = linq.Expression.Constant(NumItemSecond);
            var binaryOp = CompareCondition == "Exactly" ? linq.Expression.Equal(methodCall, numItem) :
                CompareCondition == "Between" ? linq.Expression.And(linq.Expression.GreaterThanOrEqual(methodCall, numItem),
                    linq.Expression.LessThanOrEqual(methodCall, numItemSecond)) :
                CompareCondition == "AtLeast" ? linq.Expression.GreaterThanOrEqual(methodCall, numItem) :
                CompareCondition == "IsLessThanOrEqual" ? linq.Expression.LessThanOrEqual(methodCall, numItem) : null;

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(binaryOp, paramX);

			return retVal;
		}

		#endregion
	}
}
