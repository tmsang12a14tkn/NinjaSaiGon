using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DrinkViewModels
{
    public class DrinkOrderingViewModel
    {
        public int Id { get; set; }
        public bool IsShowOrder { get; set; }
        public int Order { get; set; }
    }

    public class DrinkOrderOptionsViewModel
    {
        public List<DrinkOrderingViewModel> Sizes { get; set; }
        public List<DrinkOrderingViewModel> Sugars { get; set; }
        public List<DrinkOrderingViewModel> Ices { get; set; }
    }
}
