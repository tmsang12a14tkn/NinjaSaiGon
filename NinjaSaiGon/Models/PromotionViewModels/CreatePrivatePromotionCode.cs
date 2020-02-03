using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.PromotionViewModels
{
    public class CreatePrivatePromotionCode
    {
        public int NumOfCode { get; set; }
        public int MaxUseCode { get; set; }
        public bool IsInfinityUse { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsInfinityTime { get; set; }
        public string CodePrefix { get; set; }
        public string CodeSuffix { get; set; }
        public string CodeComment { get; set; }
        public int PrivatePromotionId { get; set; }
    }
}
