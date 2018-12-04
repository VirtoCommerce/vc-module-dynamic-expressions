using VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //For [] items of entry [] in every [] items of entry [] get [] % off no more than [] not to exceed $ []
    public class RewardItemForEveryNumOtherItemInGetOfRel : RewardItemForEveryNumInGetOfRel
    {
        public ProductContainer ConditionalProduct { get; set; } = new ProductContainer();

        #region IRewardExpression Members

        public new PromotionReward[] GetRewards()
        {
            return new PromotionReward[] {
                new CatalogItemAmountReward
                {
                    Amount = Amount,
                    AmountType = RewardAmountType.Relative,
                    Quantity = ItemLimit,
                    ForNthQuantity = ForNthQuantity,
                    InEveryNthQuantity = InEveryNthQuantity,
                    ProductId = Product?.ProductId,
                    MaxLimit = MaxLimit,
                    ConditionalProductId = ConditionalProduct?.ProductId,
                }
            };
        }

        #endregion
    }
}
