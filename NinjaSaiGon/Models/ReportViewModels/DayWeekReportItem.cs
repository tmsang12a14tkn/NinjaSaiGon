using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class DayWeekReportItem
    {
        public DateTime Date { get; set; }
        public string DateString { get; set; }

        public int SumAllDrinkCount { get => Orders.Sum(o => o.AllDrinkCount); }
        public int SumBaseDrinkCount { get => Orders.Sum(o => o.BaseDrinkCount); }
        public int SumDiscountDrinkCount { get => Orders.Sum(o => o.DiscountDrinkCount); }
        public int SumFreeDrinkCount { get => Orders.Sum(o => o.FreeDrinkCount); }

        /// <summary>
        /// Số món cafe
        /// </summary>
        public int SumCafeCount { get => Orders.Sum(o => o.CafeCount); }
        /// <summary>
        /// Số món Trà
        /// </summary>
        public int SumTeaCount { get => Orders.Sum(o => o.TeaCount); }
        /// <summary>
        /// Số món Trà sữa
        /// </summary>
        public int SumMilkTeaCount { get => Orders.Sum(o => o.MilkTeaCount); }
        /// <summary>
        /// Số món Thanh nhiệt
        /// </summary>
        public int SumHerbalCount { get => Orders.Sum(o => o.HerbalCount); }
        /// <summary>
        /// Số món Nước ép
        /// </summary>
        public int SumFruitCount { get => Orders.Sum(o => o.FruitCount); }
        /// <summary>
        /// Số món Macchito
        /// </summary>
        public int SumMacchiatoCount { get => Orders.Sum(o => o.MacchiatoCount); }
        /// <summary>
        /// Số món Soda
        /// </summary>
        public int SumSodaCount { get => Orders.Sum(o => o.SodaCount); }

        /// <summary>
        /// Tổng tiền các món
        /// </summary>
        public int DrinksTotal { get => Orders.Sum(o => o.DrinksTotal); }
        /// <summary>
        /// Tổng tiền giảm giá
        /// </summary>
        public int MoneyDiscount { get => Orders.Sum(o => o.MoneyDiscount); }
        /// <summary>
        /// Tổng tiền phụ thu
        /// </summary>
        public int MoneySurcharge { get => Orders.Sum(o => o.MoneySurcharge); }
        /// <summary>
        /// Phí ship (Ninja)
        /// </summary>
        public int ShipFee { get => Orders.Sum(o => o.ShipFee); } //phí giao hàng Ninja tính
        /// <summary>
        /// Tên đơn vị giao hàng
        /// </summary>
        public string Partner { get; set; } //đơn vị giao hàng
        /// <summary>
        /// Phí ship (đơn vị giao hàng)
        /// </summary>
        public int PartnerShipFee { get => Orders.Sum(o => o.PartnerShipFee); }

        /// <summary>
        /// Thành tiền (Ninja)
        /// </summary>
        public int NinjaDayTotal { get => Orders.Sum(o => o.NinjaOrderTotal); }

        /// <summary>
        /// Thành tiền (thực thu)
        /// </summary>
        public int RealDayTotal { get => NinjaDayTotal - PartnerShipFee; }


        public IEnumerable<OrderWeekReportItem> Orders { get; set; }
    }
}
