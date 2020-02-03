using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class ProductReportFilterInput
    {
        public ProductReportFilterInput()
        {
            IsShowDrinkOnMenu = true;
        }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IsShowDrinkOnMenu { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
