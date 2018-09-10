using System;
using System.Reflection;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Platform.Core.Common;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
    public abstract class MatchedConditionBase : DynamicExpression, IConditionExpression
    {
        public string Value { get; set; }
        public string MatchCondition { get; set; } = ModuleConstants.ConditionOperation.Contains;

        #region IConditionExpression Members

        public abstract linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression();

        #endregion

        public linq.Expression GetConditionExpression(linq.Expression leftOperandExpression)
        {
            MethodInfo method;
            linq.Expression resultExpression = null;

            if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.Contains))
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var toLowerMethod = typeof(string).GetMethod("ToLowerInvariant");
                var toLowerExp = linq.Expression.Call(leftOperandExpression, toLowerMethod);
                resultExpression = linq.Expression.Call(toLowerExp, method, linq.Expression.Constant(Value.ToLowerInvariant()));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.Matching))
            {
                method = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                var toLowerMethod = typeof(string).GetMethod("ToLowerInvariant");
                var toLowerExp = linq.Expression.Call(leftOperandExpression, toLowerMethod);
                resultExpression = linq.Expression.Call(toLowerExp, method, linq.Expression.Constant(Value.ToLowerInvariant()));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.ContainsCase))
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                resultExpression = linq.Expression.Call(leftOperandExpression, method, linq.Expression.Constant(Value));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.MatchingCase))
            {
                method = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                resultExpression = linq.Expression.Call(leftOperandExpression, method, linq.Expression.Constant(Value));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.NotContains))
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var toLowerMethod = typeof(string).GetMethod("ToLowerInvariant");
                var toLowerExp = linq.Expression.Call(leftOperandExpression, toLowerMethod);
                resultExpression = linq.Expression.Not(linq.Expression.Call(toLowerExp, method, linq.Expression.Constant(Value.ToLowerInvariant())));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.NotMatching))
            {
                method = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                var toLowerMethod = typeof(string).GetMethod("ToLowerInvariant");
                var toLowerExp = linq.Expression.Call(leftOperandExpression, toLowerMethod);
                resultExpression = linq.Expression.Not(linq.Expression.Call(toLowerExp, method, linq.Expression.Constant(Value.ToLowerInvariant())));
            }
            else if (MatchCondition.EqualsInvariant(ModuleConstants.ConditionOperation.NotContainsCase))
            {
                method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                resultExpression = linq.Expression.Not(linq.Expression.Call(leftOperandExpression, method, linq.Expression.Constant(Value)));
            }
            else
            {
                method = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                resultExpression = linq.Expression.Not(linq.Expression.Call(leftOperandExpression, method, linq.Expression.Constant(Value)));
            }
            return resultExpression;
        }

    }
}
