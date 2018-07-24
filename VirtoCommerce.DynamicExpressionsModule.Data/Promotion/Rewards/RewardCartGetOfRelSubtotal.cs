using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Domain.Marketing.Model;
using VirtoCommerce.Domain.Common;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //Get [] % off cart subtotal not to exceed $ [ 500 ]
    public class RewardCartGetOfRelSubtotal : DynamicExpression, IRewardExpression
    {
        public decimal Amount { get; set; }
        public decimal MaxLimit { get; set; }
        #region IRewardsExpression Members

        public PromotionReward[] GetRewards()
        {
            var retVal = new CartSubtotalReward
            {
                Amount = Amount,
                AmountType = RewardAmountType.Relative,
                MaxLimit = MaxLimit
            };
            return new PromotionReward[] { retVal };
        }

        #endregion
    }
}
