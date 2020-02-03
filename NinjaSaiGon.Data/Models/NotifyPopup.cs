using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public class NotifyPopup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAllowOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
