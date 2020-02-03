using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Order.Models.ShoppingCartViewModels
{
    public class CartDrinkView
    {
        public int Index { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int SugarOptionId { get; set; }
        public string SugarOption { get; set; }
        public int IceOptionId { get; set; }
        public string IceOption { get; set; }
        public int DrinkSizeId { get; set; }
        public string DrinkSize { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int DrinkMaxSizeId { get; set; }
        public string DrinkMaxSize { get; set; }
        public int DrinkMaxSizePrice { get; set; }
        public int DrinkMinSizeId { get; set; }
        public string DrinkMinSize { get; set; }
        public int DrinkMinSizePrice { get; set; }
        public int Amount { get; set; }
        public bool IsPromoDiscount { get; set; }
        public bool IsNewDrink { get; set; }

        public List<CartToppingView> Toppings { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class CartToppingView
    {
        public int ToppingId { get; set; }
        public string Topping { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
