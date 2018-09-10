using System;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Platform.Core.Common;
using linq = System.Linq.Expressions;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Common
{
    public abstract class CompareConditionBase : DynamicExpression, IConditionExpression
    {

        [Obsolete]
        public bool? Exactly { get; }

        private string _compareCondition = ModuleConstants.ConditionOperation.AtLeast;

        public virtual string CompareCondition
        {
            get
            {
                //Backward compatibility support
#pragma warning disable 612, 618
                return Exactly.HasValue && Exactly.Value ? ModuleConstants.ConditionOperation.Exactly : _compareCondition;
#pragma warning restore 612, 618
            }

            set
            {
                _compareCondition = value;
            }
        }

        #region IConditionExpression Members

        public abstract linq.Expression<Func<IEvaluationContext, bool>> GetConditionExpression();

        #endregion

        public linq.BinaryExpression GetConditionExpression(linq.Expression leftOperandExpression, linq.Expression rightOperandExpression, linq.Expression rightSecondOperandExpression = null)
        {
            linq.BinaryExpression binaryOp;

            if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsMatching) || CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.Exactly))
            {
                binaryOp = linq.Expression.Equal(leftOperandExpression, rightOperandExpression);
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsNotMatching))
            {
                binaryOp = linq.Expression.NotEqual(leftOperandExpression, rightOperandExpression);
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsGreaterThan))
            {
                binaryOp = linq.Expression.GreaterThan(leftOperandExpression, rightOperandExpression);
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsLessThan))
            {
                binaryOp = linq.Expression.LessThan(leftOperandExpression, rightOperandExpression);
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.Between))
            {
                binaryOp = linq.Expression.And(linq.Expression.GreaterThanOrEqual(leftOperandExpression, rightOperandExpression),
                    linq.Expression.LessThanOrEqual(leftOperandExpression, rightSecondOperandExpression));
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.AtLeast) || CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsGreaterThanOrEqual))
            {
                binaryOp = linq.Expression.GreaterThanOrEqual(leftOperandExpression, rightOperandExpression);
            }
            else if (CompareCondition.EqualsInvariant(ModuleConstants.ConditionOperation.IsLessThanOrEqual))
            {
                binaryOp = linq.Expression.LessThanOrEqual(leftOperandExpression, rightOperandExpression);
            }
            else
                binaryOp = linq.Expression.LessThanOrEqual(leftOperandExpression, rightOperandExpression);

            return binaryOp;
        }

    }
}
