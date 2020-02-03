using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DrinkViewModels
{
    public class CartDrinkView
    {
        public int Index { get; set; }
        public int OrderDetailId { get; set; }
        public int CategoryId { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int SugarOptionId { get; set; }
        public string SugarOption { get; set; }
        public int IceOptionId { get; set; }
        public string IceOption { get; set; }
        public int DrinkSizeId { get; set; }
        public string DrinkSize { get; set; }
        public int DrinkMaxSizeId { get; set; }
        public string DrinkMaxSize { get; set; }
        public int DrinkMaxSizePrice { get; set; }
        public int DrinkMinSizeId { get; set; }
        public string DrinkMinSize { get; set; }
        public int DrinkMinSizePrice { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public bool IsPromoDiscount { get; set; }
        public int FullPrice { get; set; }
        public string Note { get; set; }
        public int DiscountType { get; set; }
        public int DiscountPercentValue { get; set; }
        public int DiscountMoneyValue { get; set; }
        public int? FreeDrinkReasonId { get; set; }
        public string DiscountReason { get; set; }
        public bool IsFreeDrink { get; set; }
        public bool IsNew { get; set; }

        public List<CartToppingView> Toppings { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class CartToppingView
    {
        public int OrderDetailId { get; set; }
        public int ToppingId { get; set; }
        public string Topping { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
