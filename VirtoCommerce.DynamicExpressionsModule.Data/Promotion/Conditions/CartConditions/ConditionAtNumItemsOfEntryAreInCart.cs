using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
	//[] [] items of entry are in shopping cart
	public class ConditionAtNumItemsOfEntryAreInCart : ConditionBase, IConditionExpression
	{
		public int NumItem { get; set; }

        public int NumItemSecond { get; set; }

        public string CompareCondition { get; set; }

#pragma warning disable 612, 618
        [Obsolete("Obsolete, only for backwards compatibility", false)]
#pragma warning restore 612, 618
        public bool Exactly { get; set; }

		public string ProductId { get; set; }

		public string ProductName { get; set; }

        public ConditionAtNumItemsOfEntryAreInCart()
        {
            CompareCondition = "AtLeast";
        }

        #region IConditionExpression Members
        /// <summary>
        /// ((PromotionEvaluationContext)x).GetCartItemsOfProductQuantity(ProductId, ExcludingCategoryIds, ExcludingProductIds) > NumItem
        /// </summary>
        /// <returns></returns>
        public linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
		{
			var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
			var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(PromotionEvaluationContext));
			var methodInfo = typeof(PromotionEvaluationContextExtension).GetMethod("GetCartItemsOfProductQuantity");
			var methodCall = linq.Expression.Call(null, methodInfo, castOp, linq.Expression.Constant(ProductId));
			var numItem = linq.Expression.Constant(NumItem);
            var numItemSecond = linq.Expression.Constant(NumItemSecond);
            var binaryOp = CompareCondition == "Exactly" ? linq.Expression.Equal(methodCall, numItem) :
                CompareCondition == "Between" ? linq.Expression.And(linq.Expression.GreaterThanOrEqual(methodCall, numItem), linq.Expression.LessThanOrEqual(methodCall, numItemSecond)) :
                CompareCondition == "AtLeast" ? linq.Expression.GreaterThanOrEqual(methodCall, numItem) :
                CompareCondition == "IsLessThanOrEqual" ? linq.Expression.GreaterThanOrEqual(methodCall, numItem) : null;

            var retVal = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(binaryOp, paramX);

			return retVal;
		}

		#endregion
	}
}
