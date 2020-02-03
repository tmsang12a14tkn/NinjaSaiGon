using NinjaSaiGon.Admin.Models.DrinkViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.PromotionViewModels
{
    public class PromotionDrinkItemView
    {
        public int Index { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PromotionId { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int DrinkSizeId { get; set; }
        public string DrinkSize { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public bool IsPromoDiscount { get; set; }
        public int FullPrice { get; set; }
        public string Note { get; set; }

        public List<PromotionDrinkToppingView> PromotionDrinkToppings { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class PromotionDrinkToppingView
    {
        public int Id { get; set; }
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public int? ToppingPrice { get; set; }
        public int Quantity { get; set; }
        public int PromotionDrinkId { get; set; }
    }
}
