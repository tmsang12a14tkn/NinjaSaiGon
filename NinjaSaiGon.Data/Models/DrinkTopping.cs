using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class DrinkTopping
    {
        public int DrinkId { get; set; }
        public int ToppingId { get; set; }
        public bool IsPrimary { get; set; }
        public int PriceForSale { get; set; }
        public int PriceForExtra { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
