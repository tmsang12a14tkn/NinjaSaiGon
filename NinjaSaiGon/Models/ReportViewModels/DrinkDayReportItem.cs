using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class DrinkDayReportItem
    {
        public string DrinkName { get; set; }
        public string DrinkSize { get; set; }
        public string IceOption { get; set; }
        public string SugarOption { get; set; }
        
        public string[] Toppings { get; set; }
        /// <summary>
        /// Số ly
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Đơn giá đã bao gồm topping
        /// </summary>
        public int FullPrice { get; set; }
        /// <summary>
        /// Tổng tiền = đơn giá bao gồm topping * số lượng
        /// </summary>
        public int Amount { get; set; }

    }
}
