using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DrinkViewModels
{
    public class CartDrinkViewModel
    {
        public int Index { get; set; }
        public int OrderDetailId { get; set; }
        public int CategoryId { get; set; }
        public int DrinkId { get; set; }
        public int SugarOptionId { get; set; }
        public int IceOptionId { get; set; }
        public int DrinkSizeId { get; set; }
        public int Quantity { get; set; }
        public bool IsPromoDiscount { get; set; }
        public int DiscountType { get; set; }
        public int DiscountPercentValue { get; set; }
        public int DiscountMoneyValue { get; set; }
        public int? FreeDrinkReasonId { get; set; }
        public string DiscountReason { get; set; }
        public bool IsFreeDrink { get; set; }
        public string Note { get; set; }
        public List<CartToppingViewModel> Toppings { get; set; }
    }
    public class CartToppingViewModel
    {
        public int ToppingId { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
        public int Price { get; set; }
    }
}
