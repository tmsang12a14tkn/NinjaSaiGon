using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.DrinkViewModels
{
    public class EditDrinkPhotoModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsDeleted { get; set; }
        public int DrinkId { get; set; }
    }
    public class EditDrinkModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Price { get; set; }
        public string Barcode { get; set; }
        public bool IsFeatured { get; set; }
        public bool Reenter { get; set; } //Có thể nhập lai kho
        public bool IsSuggested { get; set; }
        public bool IsCombo { get; set; }
        public bool IsOpen { get; set; }
        public bool IsShared { get; set; }
        public bool IsNew { get; set; } // món mới

        public bool IsActive { get; set; }
        
        public virtual IList<EditDrinkPhotoModel> Photos { get; set; }
        
    }
}
