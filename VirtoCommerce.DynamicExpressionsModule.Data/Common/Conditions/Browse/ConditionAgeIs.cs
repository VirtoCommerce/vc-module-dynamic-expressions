using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Platform.Core.Common;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
    //Age is []
    public class ConditionAgeIs : CompareConditionBase
    {
        public int Value { get; set; }
        public int SecondValue { get; set; }

        public override linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
        {
            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(EvaluationContextBase));
            var leftOperandExpression = linq.Expression.Property(castOp, typeof(EvaluationContextBase).GetProperty(ReflectionUtility.GetPropertyName<EvaluationContextBase>(x => x.ShopperAge)));
            var rightOperandExpression = linq.Expression.Constant(Value);
            var rightSecondOperandExpression = linq.Expression.Constant(SecondValue);

            var result = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(GetConditionExpression(leftOperandExpression, rightOperandExpression, rightSecondOperandExpression), paramX);
            return result;
        }
    }
}
