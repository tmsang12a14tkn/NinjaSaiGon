using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public enum RangeType : int
    {
        All = 1,
        Day = 2,
        Week = 3,
        OneMonth = 4,
        ThreeMonth = 5,
        SixMonth = 6,
        Custom = 7
    }

    public class WeekReportFilterResult
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }


        /// <summary>
        /// Tổng
        /// </summary>
        public int SumOrder { get; set; }
        public int SumAllDrinkCount { get; set; }
        public int SumBaseDrinkCount { get; set; }
        public int SumDiscountDrinkCount { get; set; }
        public int SumFreeDrinkCount { get; set; }
        public int SumCafeCount { get; set; }
        public int SumTeaCount { get; set; }
        public int SumMilkTeaCount { get; set; }
        public int SumHerbalCount { get; set; }
        public int SumFruitCount { get; set; }
        public int SumMacchiatoCount { get; set; }
        public int SumSodaCount { get; set; }
        public int SumDrinksTotal { get; set; }
        public int SumMoneyDiscount { get; set; }
        public int SumMoneySurcharge { get; set; }
        public int SumShipFee { get; set; }
        public int SumPartnerShipFee { get; set; }
        public int SumNinjaDayTotal { get; set; }
        public int SumRealDayTotal { get => SumNinjaDayTotal - SumPartnerShipFee; }

        public IEnumerable<DayWeekReportItem> Days { get; set; }
    }

    public class OrderDetailFilterResult
    {
        public string CategoryName { get; set; }
        public int DrinkCount { get; set; }
    }

    public class OrderTabFilterView
    {
        public DateTime Begin { get; set; }
        public bool IsCurrent { get; set; }

    }

    public class OrderFilterView
    {
        public DateTime DateSelected { get; set; }
       
        public DateTime? End { get; set; }
        public DateTime? Begin { get; set; }
        public List<OrderTabFilterView> WeekTabs { get; set; }

    }
}
