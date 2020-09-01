using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Marketing.Model;
using VirtoCommerce.Domain.Common;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //Get []% off recurring [ select product ] not to exceed $ [ 500 ]
    public class RewardRecurringItemGetOfRel : DynamicExpression, IRewardExpression
    {
        public decimal Amount { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal MaxLimit { get; set; }

        #region IRewardExpression Members

        public PromotionReward[] GetRewards()
        {
            var retVal = new CatalogRecurringItemAmountReward
            {
                Amount = Amount,
                AmountType = RewardAmountType.Relative,
                ProductId = ProductId,
                MaxLimit = MaxLimit
            };
            return new PromotionReward[] { retVal };
        }

        #endregion
    }
}
