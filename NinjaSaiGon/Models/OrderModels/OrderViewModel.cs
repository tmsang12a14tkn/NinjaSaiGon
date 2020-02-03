using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.OrderModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string Code { get; set; }
        public OrderCustomerType OrderCustomerType { get; set; }
        public int? AgencyId { get; set; }
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AddressLine { get; set; }
        public string PhoneNumber { get; set; }
        public int? DistrictId { get; set; }
        public string CardCode { get; set; }
        public string EmployeeNote { get; set; }
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
        public int OrderTotal { get; set; }
        public bool IsDeliveryNow { get; set; }
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderPlaced { get; set; } //thoi gian dat hang

        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDeliveried { get; set; } //thoi gian giao hang

        public string CustomerNote { get; set; } //ghi chu cua khach hang

        public string StatusNote { get; set; } //ghi chu trang thai

        public int? OrderSourceId { get; set; } //nguon don hang
        public string EmployeeName { get; set; } //nhan vien ban hang
        public bool IsDeleted { get; set; }
        [NotMapped]
        public bool IsSpecialPromo { get; set; }
        public List<CartDrinkView> OrderDetails { get; set; }
        public virtual OrderDeliveryViewModel OrderDelivery { get; set; }
    }

    public class OrderDeliveryViewModel
    {
        public int OrderId { get; set; }
        public int Partner { get; set; }
        public int? DeliveryPartnerId { get; set; }
        public float PartnerDistance { get; set; }
        public int PartnerShipFee { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? AcceptedTime { get; set; } //thoi gian nhan giao
        public DateTime? BeginTime { get; set; } //thoi gian bat dau di giao
        public DateTime? EndTime { get; set; } //thoi gian giao hang xong
        public string Address { get; set; } //dia chi giao hang
        public string Note { get; set; }
    }

    public class OrderCustomerFilterViewModel
    {
        public OrderCustomerType OrderCustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }

    public class OrderCustomerFilterResult
    {
        public int Id { get; set; }
        public OrderCustomerType OrderCustomerType { get; set; }
        public string PrimaryName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public List<string> Phones { get; set; }
    }
    
}
