using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Drink Drink { get; set; }
        //public int Quantity { get; set; }
        //public int SizeId { get; set; }
        //public int? IceOptionId { get; set; }
        //public int? SugarOptionId { get; set; }
        //public string Note { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
