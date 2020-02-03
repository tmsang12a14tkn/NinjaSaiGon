using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Models
{
    public class EditOrderInfoModel
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string Code { get; set; }

        

        [Display(Name = "Ghi chú của nhân viên")]
        public string EmployeeNote { get; set; } //ghi chu cua nhan vien

       
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        public string PromotionCode { get; set; }
        public DiscountType PromotionDiscountType { get; set; }
        public int PromotionDiscountValue { get; set; }

        [ScaffoldColumn(false)]
        public int OrderTotal { get; set; } //tong so tien

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderPlaced { get; set; } //thoi gian dat hang

        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDeliveried { get; set; } //thoi gian giao hang

        public bool IsAutoDiscount { get; set; }
        public int DiscountAmount { get; set; }
        public bool IsAutoSurcharge { get; set; }
        public int SurchargeAmount { get; set; }
        public bool IsAutoShipFee { get; set; }
        public int ShipFee { get; set; }
        public int Distance { get; set; }

        public string StatusNote { get; set; } //ghi chu trang thai

        public string OrderSource { get; set; } //nguon don hang
        public string EmployeeName { get; set; } //nhan vien ban hang

        //public OrderDelivery OrderDelivery { get; set; }

        //public List<EditOrderDetailModel> OrderDetails { get; set; }
    }

    public class EditOrderCustomerModel
    {
        public int OrderId { get; set; }
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

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mạng xã hội")]
        public string SocialNetwork { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string CustomerNote { get; set; } //ghi chu cua khach hang
    }

    public class EditOrderDeliveryModel
    {
        public int OrderId { get; set; }

        public DeliveryPartner Partner { get; set; }
        public float PartnerDistance { get; set; }
        public int PartnerShipFee { get; set; }
        //Thong tin nguoi giao hang
        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }


        [StringLength(50)]
        public string Email { get; set; }

        public DateTime? AcceptedTime { get; set; } //thoi gian nhan giao
        public DateTime? BeginTime { get; set; } //thoi gian bat dau di giao
        public DateTime? EndTime { get; set; } //thoi gian giao hang xong
        public string Address { get; set; } //dia chi giao hang
        public string Note { get; set; }
    }

    public class EditOrderDrinksModel
    {
        public int OrderId { get; set; }
        public List<EditOrderDetailModel> OrderDetails { get; set; }
    }

    public class EditOrderNotesModel
    {
        public int OrderId { get; set; }
        public string CustomerNote { get; set; }
        public string EmployeeNote { get; set; }
    }

    public class EditOrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }  
        public int DrinkId { get; set; }
        public int DrinkSizeId { get; set; }
        public string IceOption { get; set; }
        public string SugarOption { get; set; }
        public int Quantity { get; set; } //so luong
        public string Note { get; set; }

        public List<OrderDetailToppingModel> OrderDetailToppings { get; set; }
        
        public bool IsDeleted { get; set; }

    }

    public class OrderDetailToppingModel
    {
        public int OrderDetailId { get; set; }
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public int Price { get; set; } //gia cua topping
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
    }
}
