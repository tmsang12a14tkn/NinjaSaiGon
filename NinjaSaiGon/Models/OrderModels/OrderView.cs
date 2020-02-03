using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.OrderModels
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string Code { get; set; }
        public List<OrderDetailView> OrderDetails { get; set; }

        //thong tin khách hàng:
        [Display(Name = "Tên khách hàng")]
        public string FullName { get; set; }

        [Display(Name = "Mã thẻ")]
        public string CardCode { get; set; } //ma the khach hang

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string AddressLine { get; set; }

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mạng xã hội")]
        public string SocialNetwork { get; set; }

        [Display(Name = "Ghi chú của nhân viên")]
        public string EmployeeNote { get; set; } //ghi chu cua nhan vien

        public string Email { get; set; }
        public string PromotionCode { get; set; }
        public string PromotionName { get; set; }
        public DiscountType PromotionDiscountType { get; set; }
        public int PromotionDiscountValue { get; set; } //tien khuyen mai khi nhap coupon
        public bool IsAutoDiscount { get; set; } //tu nhap tien giam gia
        public int DiscountAmount { get; set; } //giam gia khac (vd: khai truong 1 tang 1)
        public bool IsAutoSurcharge { get; set; } // tu nhap tien phu thu
        public int SurchargeAmount { get; set; } //phu thu
        public bool IsAutoShipFee { get; set; } //tu nhap tien ship
        public int ShipFee { get; set; }
        public int Distance { get; set; }

        public int OrderTotal { get; set; } //tong so tien

        public bool IsDeliveryNow { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}")]
        public DateTime OrderPlaced { get; set; } //thoi gian dat hang

        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}")]
        public DateTime OrderDeliveried { get; set; } //thoi gian giao hang

        public string CustomerNote { get; set; } //ghi chu cua khach hang

        public string StatusNote { get; set; } //ghi chu trang thai

        public string EmployeeName { get; set; } //nhan vien ban hang

    }
    public class OrderDetailView
    {
        public int OrderDetailId { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public string DrinkSize { get; set; }
        public string IceOption { get; set; }
        public string SugarOption { get; set; }
        public int Amount { get; set; } //tong so tien = gia bao gom topping * so luong
        public int Price { get; set; } //gia cua drink
        public int FullPrice { get; set; } //gia da bao gom topping
        public int Quantity { get; set; } //so luong
        public string Note { get; set; }
        public bool IsPromoDiscount { get; set; } //mon duoc khuyen mai
        public IEnumerable<OrderDetailTopping> OrderDetailToppings { get; set; }

        public int DrinkSizeId { get; set; }
    }
}
