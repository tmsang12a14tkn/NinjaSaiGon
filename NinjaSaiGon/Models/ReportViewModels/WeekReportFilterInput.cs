using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.ReportViewModels
{
    public class WeekReportFilterInput
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int Page { get; set; }
        public int Count { get; set; } //0: get all
        public bool LoadAll { get; set; }
    }
}
