using VirtoCommerce.Domain.Marketing.Model;

namespace VirtoCommerce.DynamicExpressionsModule.Data.Promotion
{
    //For [] items of entry [] in every [] items of entry [] get [] % off no more than [] not to exceed $ []
    public class RewardItemForEveryNumOtherItemInGetOfRel : RewardItemForEveryNumInGetOfRel
    {
        public ProductContainer ConditionalProduct { get; set; } = new ProductContainer();

        #region IRewardExpression Members

        protected override void FillAmountReward(CatalogItemAmountReward reward)
        {
            base.FillAmountReward(reward);
            reward.ConditionalProductId = ConditionalProduct?.ProductId;
        }

        #endregion
    }
}
