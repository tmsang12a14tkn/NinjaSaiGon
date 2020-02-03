using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class ProductReportFilterResult
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }

        public int SumSizeSQuantity { get => ProductGroups.Sum(p => p.SumSizeSQuantity); }
        public int SumSizeMQuantity { get => ProductGroups.Sum(p => p.SumSizeMQuantity); }
        public int SumSizeLQuantity { get => ProductGroups.Sum(p => p.SumSizeLQuantity); }
        public int SumTotalQuantity { get => SumSizeSQuantity + SumSizeMQuantity + SumSizeLQuantity; }

        public int SumSizeSAmount { get => ProductGroups.Sum(p => p.SumSizeSAmount); }
        public int SumSizeMAmount { get => ProductGroups.Sum(p => p.SumSizeMAmount); }
        public int SumSizeLAmount { get => ProductGroups.Sum(p => p.SumSizeLAmount); }
        public int SumTotalAmount { get => SumSizeSAmount + SumSizeMAmount + SumSizeLAmount; }

        public List<ProductCategoryGroupReport> ProductGroups { get; set; }
    }

    public class ProductCategoryGroupReport
    {
        public string CategoryName { get; set; }

        public int SumSizeSQuantity { get => Products.Sum(p => p.SizeSQuantity); }
        public int SumSizeMQuantity { get => Products.Sum(p => p.SizeMQuantity); }
        public int SumSizeLQuantity { get => Products.Sum(p => p.SizeLQuantity); }
        public int SumTotalQuantity { get => SumSizeSQuantity + SumSizeMQuantity + SumSizeLQuantity; }

        public int SumSizeSAmount { get => Products.Sum(p => p.SizeSAmount); }
        public int SumSizeMAmount { get => Products.Sum(p => p.SizeMAmount); }
        public int SumSizeLAmount { get => Products.Sum(p => p.SizeLAmount); }
        public int SumTotalAmount { get => SumSizeSAmount + SumSizeMAmount + SumSizeLAmount; }

        public List<ProductReportItem> Products { get; set; }
    }

    public class ProductReportItem
    {
        /// <summary>
        /// Mã món
        /// </summary>
        public string DrinkCode { get; set; }
        /// <summary>
        /// Tên món
        /// </summary>
        public string DrinkName { get; set; }
        /// <summary>
        /// SL Size S
        /// </summary>
        public int SizeSQuantity { get; set; }
        /// <summary>
        /// SL Size M
        /// </summary>
        public int SizeMQuantity { get; set; }
        /// <summary>
        /// SL Size L
        /// </summary>
        public int SizeLQuantity { get; set; }
        /// <summary>
        /// Tổng số
        /// </summary>
        public int TotalQuantity { get => SizeSQuantity + SizeMQuantity + SizeLQuantity; }
        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public string DrinkUnit { get; set; }
        /// <summary>
        /// Tiền Size S
        /// </summary>
        public int SizeSAmount { get; set; }
        /// <summary>
        /// Tiền Size M
        /// </summary>
        public int SizeMAmount { get; set; }
        /// <summary>
        /// Tiền Size L
        /// </summary>
        public int SizeLAmount { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public int TotalAmount { get => SizeSAmount + SizeMAmount + SizeLAmount; }
    }
}
