using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models.Dto
{
    public class CheckPromotionResult
    {
        public bool Success { get; set; }
        public int PromotionId { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountValue { get; set; }
        public ApplyRule ApplyRule { get; set; }
        /// <summary>
        /// kiểu sinh tặng món: true: random, false: theo thứ tự
        /// </summary>
        public bool PromotionDrinkRandom { get; set; }
        public int BuyQuantity { get; set; }
        public int GiveQuantity { get; set; }
        public bool IsPrivatePromotion { get; set; }
    }

    public class PromotionFreeDrinkResult {
        public string DrinkSize { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
    }
}
