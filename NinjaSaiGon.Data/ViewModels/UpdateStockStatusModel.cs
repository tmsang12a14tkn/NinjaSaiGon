using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.ViewModels
{
    public class UpdateStockStatusModel
    {
        public int Id { get; set; }
        public bool OutOfStock { get; set; }
        public DateTime? OutOfStockFrom { get; set; }
        public DateTime? OutOfStockTo { get; set; }
    }
}
