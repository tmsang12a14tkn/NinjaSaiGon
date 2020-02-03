using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public string DrinkSize { get; set; }
        public bool IsNewDrink { get; set; }
        public string IceOption { get; set; }
        public string SugarOption { get; set; }
        public int Amount { get; set; } //tong so tien = gia bao gom topping * so luong
        public int Price { get; set; } //gia cua drink
        public int FullPrice { get; set; } //gia da bao gom topping
        public int Quantity { get; set; } //so luong
        public bool IsPromoDiscount { get; set; } //mon duoc huong khuyen mai
        public string Note { get; set; }
        public bool IsFreeDrink { get; set; } //mon duoc khuyen mai them (price = 0)

        public virtual ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
        public virtual Order Order { get; set; }

        public int DrinkSizeId { get; set; }

        //Giảm giá cho món
        public int DiscountType { get; set; } //1: giảm giá theo %, 2: giảm giá theo tiền, 3: tặng món
        public int DiscountPercentValue { get; set; }
        public int DiscountMoneyValue { get; set; }
        public int? FreeDrinkReasonId { get; set; }
        public string DiscountReason { get; set; }


        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual FreeDrinkReason FreeDrinkReason { get; set; }
        
    }

    public class OrderDetailTopping
    {
        public int OrderDetailId { get; set; }
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public int Price { get; set; } //gia cua topping
        public int Amount { get; set; } //tong so tien
        public int Quantity { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
        //public virtual Topping Topping { get; set; }
    }
}
