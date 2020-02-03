using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class DayReportFilterResult
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<DayReportItem> Days { get; set; }
    }
}
