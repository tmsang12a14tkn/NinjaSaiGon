using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NinjaSaiGon.Order.Models.ShoppingCartViewModels
{
    public class CreateOrderModel
    {
        public OrderStatus Status { get; set; }
        public string Code { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        //thong tin khách hàng:

        [Display(Name = "Tên")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "Đệm")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Display(Name = "Họ")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Mã thẻ")]
        [StringLength(50)]
        public string CardCode { get; set; } //ma the khach hang

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string AddressLine { get; set; }

        
        public int DistrictId { get; set; }
        
        //public int CityId { get; set; }
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        public string PromotionCode { get; set; }
        public DiscountType PromotionDiscountType { get; set; }
        public int PromotionDiscountValue { get; set; }
        public int _dt_am { get; set; } //DiscountAmount
        public int _se_am { get; set; } //ShipFee
        public int _de_lg { get; set; } //Distance

        [ScaffoldColumn(false)]
        public int OrderTotal { get; set; } //tong so tien

        public  bool IsDeliveryNow { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDeliveried { get; set; } //thoi gian giao hang

        public string CustomerNote { get; set; } //ghi chu cua khach hang
    
        public string OrderSource { get; set; } //nguon don hang
    }
}
