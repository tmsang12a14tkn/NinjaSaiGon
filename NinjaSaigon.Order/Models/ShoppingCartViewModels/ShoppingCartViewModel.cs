using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Order.Models.ShoppingCartViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
