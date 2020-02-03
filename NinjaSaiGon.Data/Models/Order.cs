using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Mới")]
        New,
        [Display(Name = "Đã xác nhận")]
        Confirmed,
        [Display(Name = "Hủy")]
        Canceled,
        [Display(Name = "Đang làm")]
        Processing,
        [Display(Name = "Hoàn thành/Chờ đi giao")]
        Made,
        [Display(Name = "Đã nhận giao")]
        DeliveryAccepted,
        [Display(Name = "Đang đi giao")]
        Delivering,
        [Display(Name = "Thất bại")]
        Failed,
        [Display(Name = "Đang chuyển trả")]
        Returning,
        [Display(Name = "Đã chuyển trả")]
        Returned,
        [Display(Name = "Thành công")]
        Succeeded
    }

    public enum QuickOrderStatus
    {
        [Display(Name = "Mới")]
        New,
        [Display(Name = "Đã xác nhận")]
        Confirmed,
        [Display(Name = "Thành công")]
        Succeeded = 10,
        [Display(Name = "Hủy")]
        Canceled = 2,
        [Display(Name = "Thất bại")]
        Failed = 7
    }

    public enum OrderCustomerType
    {
        [Display(Name = "Khách hàng")]
        Customer,
        [Display(Name = "Đại lý")]
        Agency
    }

    public static class OrderStatusMethods
    {
        public static String GetTab(this OrderStatus s)
        {
            switch (s)
            {
                case OrderStatus.New:
                    return "#order_waiting";
                case OrderStatus.Confirmed:
                case OrderStatus.Processing:
                    return "#order_inshop";
                case OrderStatus.Made:
                case OrderStatus.Delivering:
                case OrderStatus.DeliveryAccepted:
                case OrderStatus.Returning:
                    return "#order_onway";
                case OrderStatus.Returned:
                case OrderStatus.Canceled:
                case OrderStatus.Succeeded:
                case OrderStatus.Failed:
                    return "#order_end";
                default:
                    return "";
            }
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string Code { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        
        //thong tin khách hàng:
        public OrderCustomerType OrderCustomerType { get; set; }
        public int? AgencyId { get; set; }
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
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

        public int? DistrictId { get; set; }
        
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Mạng xã hội")]
        public string SocialNetwork { get; set; }

        [Display(Name = "Ghi chú của nhân viên")]
        public string EmployeeNote { get; set; } //ghi chu cua nhan vien
        
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
        //    ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; }
        public string PromotionCode { get; set; }
        public DiscountType PromotionDiscountType { get; set; }
        public int PromotionDiscountValue { get; set; } //tien khuyen mai khi nhap coupon
        public bool IsAutoDiscount { get; set; } //tu nhap tien giam gia
        public int DiscountAmount { get; set; } //giam gia khac (vd: khai truong 1 tang 1)
        public bool IsAutoSurcharge { get; set; } // tu nhap tien phu thu
        public int SurchargeAmount { get; set; } //phu thu
        public bool IsAutoShipFee { get; set; } //tu nhap tien ship
        public int ShipFee { get; set; }
        public int Distance { get; set; }

        [ScaffoldColumn(false)]
        public int OrderTotal { get; set; } //tong so tien

        public bool IsDeliveryNow { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderPlaced { get; set; } //thoi gian dat hang

        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDeliveried { get; set; } //thoi gian giao hang

        public string CustomerNote { get; set; } //ghi chu cua khach hang
        
        public string StatusNote { get; set; } //ghi chu trang thai
        
        public string EmployeeName { get; set; } //nhan vien ban hang
        public bool IsDeleted { get; set; }
        public int BaseDrinkCount { get; set; }
        public int DiscountDrinkCount { get; set; } //số lượng món được giảm giá
        public int FreeDrinkCount { get; set; } //số lượng món được miễn phí

        public int? OrderSourceId { get; set; }
        
        [NotMapped]
        public bool IsSpecialPromo { get; set; } //Temp field cho promo Buy1Get1

        public virtual OrderDelivery OrderDelivery { get; set; }
        public virtual District District { get; set; }
        public virtual OrderSource OrderSource { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual Person Customer { get; set; }

        public Order()
        {
            OrderPlaced = DateTime.Now;
            IsAutoDiscount = true;
            IsAutoShipFee = true;
            IsAutoSurcharge = true;
        }
    }

    //Thong tin giao hang
    public class OrderDelivery
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int Partner { get; set; }
        public int? DeliveryPartnerId { get; set; }
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
        public string Note {get;set;}
        
        public virtual Order Order { get; set; }
        public virtual DeliveryPartner DeliveryPartner { get; set; }
    }

    //Don vi giao hang
    public class DeliveryPartner
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }

    //Loai nguon don hang
    public class OrderSourceType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<OrderSource> OrderSources { get; set; }
    }

    //Nguon don hang
    public class OrderSource
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderSourceTypeId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual OrderSourceType OrderSourceType { get; set; }
    }
}
