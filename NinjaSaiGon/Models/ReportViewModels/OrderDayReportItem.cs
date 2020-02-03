using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class DayReportItem
    {
        /// <summary>
        /// Tiêu đề ngày
        /// </summary>
        public string Date { get; set; }

        public int DrinksQuantity { get => Orders.Sum(d => d.DrinksQuantity); }


        /// <summary>
        /// Giảm giá món
        /// </summary>
        public int DiscountAmount { get => Orders.Sum(o => o.DiscountAmount); }

        /// <summary>
        /// Giảm giá thêm
        /// </summary>
        public int BonusDiscountAmount { get => Orders.Sum(o => o.BonusDiscountAmount); }

        /// <summary>
        /// Giảm giá tổng
        /// </summary>
        public int TotalDiscountAmount { get => Orders.Sum(o => o.TotalDiscountAmount); }

        /// <summary>
        /// Tổng tiền phụ thu
        /// </summary>
        public int MoneySurcharge { get => Orders.Sum(o => o.MoneySurcharge); }

        /// <summary>
        /// Phí ship (Ninja)
        /// </summary>
        public int ShipFee { get => Orders.Sum(o => o.ShipFee); }

        /// <summary>
        /// Phí ship (đơn vị giao hàng)
        /// </summary>
        public int PartnerShipFee { get => Orders.Sum(o => o.PartnerShipFee); }

        /// <summary>
        /// Thành tiền (Ninja)
        /// </summary>
        public int NinjaOrderTotal { get => Orders.Sum(o => o.NinjaOrderTotal); }

        /// <summary>
        /// Thành tiền (thực thu)
        /// </summary>
        public int RealOrderTotal { get => Orders.Sum(o => o.RealOrderTotal); }

        public int DrinksTotal { get => Orders.Sum(d => d.DrinksTotal); }
        public IEnumerable<OrderDayReportItem> Orders { get; set; }

    }

    public class OrderDayReportItem
    {
        public int OrderId { get; set; }

        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// Thời gian đặt hàng
        /// </summary>
        public DateTime OrderPlaced { get; set; }

        /// <summary>
        /// Thời gian giao
        /// </summary>
        public DateTime OrderDeliveried { get; set; }

        /// <summary>
        /// Mã KM
        /// </summary>
        public string PromotionCode { get; set; }

        /// <summary>
        /// Tổng tiền các món
        /// </summary>
        public int DrinksQuantity { get => Drinks.Sum(d => d.Quantity); }

        /// <summary>
        /// Tổng tiền các món
        /// </summary>
        public int DrinksTotal { get => Drinks.Sum(d => d.Amount); }

        /// <summary>
        /// Giảm giá món
        /// </summary>
        public int DiscountAmount { get; set; }

        /// <summary>
        /// Giảm giá thêm
        /// </summary>
        public int BonusDiscountAmount { get; set; }

        /// <summary>
        /// Giảm giá tổng
        /// </summary>
        public int TotalDiscountAmount { get => DiscountAmount + BonusDiscountAmount; }

        /// <summary>
        /// Tổng tiền phụ thu
        /// </summary>
        public int MoneySurcharge { get; set; }

        /// <summary>
        /// Phí ship (Ninja)
        /// </summary>
        public int ShipFee { get; set; } //phí giao hàng Ninja tính

        /// <summary>
        /// Tên đơn vị giao hàng
        /// </summary>
        public string Partner { get; set; } //đơn vị giao hàng

        /// <summary>
        /// Phí ship (đơn vị giao hàng)
        /// </summary>
        public int PartnerShipFee { get; set; }

        /// <summary>
        /// Thành tiền (Ninja)
        /// </summary>
        public int NinjaOrderTotal { get; set; }

        /// <summary>
        /// Thành tiền (thực thu)
        /// </summary>
        public int RealOrderTotal { get => NinjaOrderTotal - PartnerShipFee; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// SDT khách hàng
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string CustomerAddress
        {
            get; set;
        }
        /// <summary>
        /// Khoảng cách khách hàng
        /// </summary>
        public string CustomerDistance
        {
            get; set;
        }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode
        {
            get; set;
        }
        /// <summary>
        /// Loại khách hàng
        /// </summary>
        public string CustomerType
        {
            get; set;
        }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note
        {
            get; set;
        }
        public IEnumerable<DrinkDayReportItem> Drinks { get; set; }
    }
}
