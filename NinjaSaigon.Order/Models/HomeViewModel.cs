using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Order.Models
{
    public class HomeViewModel
    {
        public int IsInBuy1Get1 { get; set; } //0: ko co promo, 1: dang co promo
        public NotifyPopup NotifyPopup { get; set; }
        public List<DrinkCategory> DrinkCategories { get; set; }
    }
}
