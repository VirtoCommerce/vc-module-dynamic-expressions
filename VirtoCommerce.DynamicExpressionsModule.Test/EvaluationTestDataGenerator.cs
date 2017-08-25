using System;
using System.Linq;
using System.Collections.Generic;
using VirtoCommerce.Domain.Common;
using VirtoCommerce.Domain.Marketing.Model;
using VirtoCommerce.DynamicExpressionsModule.Data.Promotion;

namespace VirtoCommerce.DynamicExpressionsModule.Test
{
    public class EvaluationTestDataGenerator
    {
        public static IEnumerable<object[]> GetConditions()
        {
            var inputDataCollection = new IEnumerable<object[]>[]
            {
                ConditionEntryIsInputData(),
                ConditionCurrencyIsInputData(),
                ConditionCodeContainsInputData(),
                ConditionCategoryIsInputData(),
                ConditionInStockQuantityInputData()
            };

            foreach (var data in inputDataCollection.SelectMany(d => d))
                yield return data;
        }

        #region ConditionCurrencyIs
        public static IEnumerable<object[]> ConditionCurrencyIsInputData()
        {
            string currency1 = "USD";
            string currency2 = "EUR";

            IEvaluationContext context = new PromotionEvaluationContext
            {
                Currency = currency1,
                PromoEntries = new List<ProductPromoEntry>
                {
                    new ProductPromoEntry(),
                    new ProductPromoEntry(),
                    new ProductPromoEntry()
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCurrencyIs { Currency = currency1 } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 3,
                    InvalidCount = 0
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCurrencyIs { Currency = currency2 } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 0,
                    InvalidCount = 3
                }
            };
        }
        #endregion

        #region ConditionEntryIs
        private static IEnumerable<object[]> ConditionEntryIsInputData()
        {
            string productId = Guid.NewGuid().ToString();

            IEvaluationContext context = new PromotionEvaluationContext
            {
                PromoEntries = new List<ProductPromoEntry>
                {
                    new ProductPromoEntry { ProductId = productId },
                    new ProductPromoEntry { ProductId = Guid.NewGuid().ToString() },
                    new ProductPromoEntry { ProductId = Guid.NewGuid().ToString() }
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionEntryIs { ProductId = productId } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 1,
                    InvalidCount = 2
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionEntryIs { ProductId = Guid.NewGuid().ToString() } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 0,
                    InvalidCount = 3
                }
            };
        }
        #endregion

        #region ConditionCodeContains
        private static IEnumerable<object[]> ConditionCodeContainsInputData()
        {
            string productCode = Guid.NewGuid().ToString();

            IEvaluationContext context = new PromotionEvaluationContext
            {
                PromoEntries = new List<ProductPromoEntry>
                {
                    new ProductPromoEntry { Code = productCode },
                    new ProductPromoEntry { Code = Guid.NewGuid().ToString() },
                    new ProductPromoEntry { Code = Guid.NewGuid().ToString() }
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCodeContains { Keyword = productCode } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 1,
                    InvalidCount = 2
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCodeContains { Keyword = productCode } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 1,
                    InvalidCount = 2
                }
            };
        }
        #endregion

        #region ConditionCategoryIs
        public static IEnumerable<object[]> ConditionCategoryIsInputData()
        {
            string categoryId = Guid.NewGuid().ToString();
            string productId = Guid.NewGuid().ToString();

            IEvaluationContext context = new PromotionEvaluationContext
            {
                PromoEntries = new List<ProductPromoEntry>
                {
                    new ProductPromoEntry { CategoryId = categoryId, ProductId = Guid.NewGuid().ToString() },
                    new ProductPromoEntry { CategoryId = categoryId, ProductId = productId },
                    new ProductPromoEntry { CategoryId = Guid.NewGuid().ToString(), ProductId = Guid.NewGuid().ToString() }
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCategoryIs { CategoryId = categoryId, ExcludingProductIds = new[] { productId } } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 1,
                    InvalidCount = 2
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCategoryIs { CategoryId = categoryId } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 2,
                    InvalidCount = 1
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionCategoryIs { CategoryId = Guid.NewGuid().ToString() } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 0,
                    InvalidCount = 3
                }
            };
        }
        #endregion

        #region ConditionInStockQuantity
        public static IEnumerable<object[]> ConditionInStockQuantityInputData()
        {
            IEvaluationContext context = new PromotionEvaluationContext
            {
                PromoEntries = new List<ProductPromoEntry>
            {
                new ProductPromoEntry { InStockQuantity = 12 },
                new ProductPromoEntry { InStockQuantity = 10 },
                new ProductPromoEntry { InStockQuantity = 8 }
            }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionInStockQuantity { Quantity = 10 } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 2,
                    InvalidCount = 1
                }
            };

            yield return new object[]
            {
                new IConditionExpression[] { new ConditionInStockQuantity { Quantity = 10, Exactly = true } },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 1,
                    InvalidCount = 2
                }
            };

            yield return new object[]
            {
                new IConditionExpression[]
                {
                    new ConditionInStockQuantity { Quantity = 12 },
                    new ConditionInStockQuantity { Quantity = 10, Exactly = true },
                    new ConditionInStockQuantity { Quantity = 7, Exactly = true }
                },
                new IRewardExpression[] { new RewardItemGetOfRel() },
                context,
                new DynamicPromotionEvaluationResult
                {
                    ValidCount = 2,
                    InvalidCount = 1
                }
            };
        }
        #endregion
    }
}
