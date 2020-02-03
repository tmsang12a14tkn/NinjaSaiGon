using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Order.Models.ShoppingCartViewModels
{
    public class CartDrinkViewModel
    {
        public int Index { get; set; }
        public int DrinkId { get; set; }
        public int SugarOptionId { get; set; }
        public int IceOptionId { get; set; }
        public int DrinkSizeId { get; set; }
        public int Quantity { get; set; }
        public bool IsPromoDiscount { get; set; }
        public bool IsFreeDrink { get; set; }
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
