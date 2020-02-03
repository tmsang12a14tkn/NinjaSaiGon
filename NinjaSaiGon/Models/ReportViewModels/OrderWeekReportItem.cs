using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class OrderWeekReportItem
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        /// <summary>
        /// Tổng số lượng các món
        /// </summary>
        public int AllDrinkCount { get; set; }
        /// <summary>
        /// Số ly gốc
        /// </summary>
        public int BaseDrinkCount { get; set; }
        /// <summary>
        /// Số ly khuyến mãi
        /// </summary>
        public int DiscountDrinkCount { get; set; }
        /// <summary>
        /// Số ly khuyến mại
        /// </summary>
        public int FreeDrinkCount { get; set; }
       
        /// <summary>
        /// Số món cafe
        /// </summary>
        public int CafeCount { get; set; }
        /// <summary>
        /// Số món Trà
        /// </summary>
        public int TeaCount { get; set; }
        /// <summary>
        /// Số món Trà sữa
        /// </summary>
        public int MilkTeaCount { get; set; }
        /// <summary>
        /// Số món Thanh nhiệt
        /// </summary>
        public int HerbalCount { get; set; }
        /// <summary>
        /// Số món Nước ép
        /// </summary>
        public int FruitCount { get; set; }
        /// <summary>
        /// Số món Macchito
        /// </summary>
        public int MacchiatoCount { get; set; }
        /// <summary>
        /// Số món Soda
        /// </summary>
        public int SodaCount { get; set; }

        /// <summary>
        /// Tổng tiền các món
        /// </summary>
        public int DrinksTotal { get; set; }
        /// <summary>
        /// Tổng tiền giảm giá
        /// </summary>
        public int MoneyDiscount { get; set; }
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
    }
}
