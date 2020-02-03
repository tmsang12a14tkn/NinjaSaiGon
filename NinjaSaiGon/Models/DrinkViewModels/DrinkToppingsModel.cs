using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DrinkViewModels
{
    public class DrinkToppingModel
    {
        public int ToppingId { get; set; }
        public bool Selected { get; set; }
        public bool IsPrimary { get; set; }
        public int PriceForSale { get; set; }
        public int PriceForExtra { get; set; }
    }

    public class DrinkToppingCategoryModel
    {
        public int ToppingCategoryId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
    public class EditDrinkToppingsModel
    {
        public int DrinkId { get; set; }
        public DrinkToppingModel[] Toppings { get; set; }
        public DrinkToppingCategoryModel[] ToppingCategories { get; set; }
    }
}
