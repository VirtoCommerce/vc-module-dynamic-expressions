using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Platform.Core.Common;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
    //Browsing from zip/postal code []
    public class ConditionGeoZipCode : MatchedConditionBase
    {
        public override linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression()
        {
            var paramX = linq.Expression.Parameter(typeof(IEvaluationContext), "x");
            var castOp = linq.Expression.MakeUnary(linq.ExpressionType.Convert, paramX, typeof(EvaluationContextBase));
            var propertyValue = linq.Expression.Property(castOp, typeof(EvaluationContextBase).GetProperty(ReflectionUtility.GetPropertyName<EvaluationContextBase>(x => x.GeoZipCode)));

            var result = linq.Expression.Lambda<Func<IEvaluationContext, bool>>(GetConditionExpression(propertyValue), paramX);
            return result;
        }
    }
}
