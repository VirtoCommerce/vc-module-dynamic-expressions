using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //For [] in every [] items of entry [] get [] % off no more than [] not to exceed $ []
    public class RewardItemForEveryNumInGetOfRel : DynamicExpression, IRewardExpression
    {
        public decimal Amount { get; set; }
        public ProductContainer Product { get; set; } = new ProductContainer();
        public int ForNthQuantity { get; set; }
        public int InEveryNthQuantity { get; set; }
        public int ItemLimit { get; set; }
        public decimal MaxLimit { get; set; }

        #region IRewardExpression Members

        public PromotionReward[] GetRewards()
        {
            var reward = new CatalogItemAmountReward();
            FillAmountReward(reward);
            return new PromotionReward[] { reward };
        }

        protected virtual void FillAmountReward(CatalogItemAmountReward reward)
        {
            reward.Amount = Amount;
            reward.AmountType = RewardAmountType.Relative;
            reward.Quantity = ItemLimit;
            reward.ForNthQuantity = ForNthQuantity;
            reward.InEveryNthQuantity = InEveryNthQuantity;
            reward.ProductId = Product?.ProductId;
            reward.MaxLimit = MaxLimit;
        }

        #endregion
    }
}
