using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class Drink
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
        public int NewOrderSort { get; set; } //thứ tự món mới

        public bool IsActive { get; set; }

        public bool DisplaySizeOption { get; set; }
        public bool DisplayIceOption { get; set; }
        public bool DisplaySugarOption { get; set; }

        public bool RequireSizeOption { get; set; }
        public bool RequireIceOption { get; set; }
        public bool RequireSugarOption { get; set; }

        public int Position { get; set; }
        public bool OutOfStock { get; set; }
        public DateTime? OutOfStockFrom { get; set; }
        public DateTime? OutOfStockTo { get; set; }

        public virtual DrinkCategory Category { get; set; }
        public virtual IList<DrinkSize> DrinkSizes { get; set; }
        public virtual IList<IceOption> IceOptions { get; set; }
        public virtual IList<SugarOption> SugarOptions { get; set; }
        public virtual IList<DrinkTopping> DrinkToppings { get; set; }
        public virtual IList<DrinkToppingCategory> DrinkToppingCategories { get; set; }
        public virtual IList<DrinkPhoto> Photos { get; set; }

        [NotMapped]
        public DrinkSize PrimarySize { get { return DrinkSizes?.FirstOrDefault(t => t.IsPrimary) ?? new DrinkSize(); } }
        [NotMapped]
        public DrinkPhoto PrimaryPhoto { get { return Photos?.OrderByDescending(t => t.IsPrimary).FirstOrDefault(); } }
    }

    public class DrinkToppingCategory
    {
        public int DrinkId { get; set; }
        public int ToppingCategoryId { get; set; }
        public int? Min { get; set; } // So loai topping toi thieu trong nhom bat buoc chon
        public int? Max { get; set; } // So loai topping toi da trong nhom duoc phep chon
        public virtual Drink Drink { get; set; }
        public virtual ToppingCategory ToppingCategory { get; set; }
    }

    public class DrinkPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public int DrinkId { get; set; }

        public virtual Drink Drink { get; set; }
    }
    public class IceOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public int DrinkId { get; set; }
        public int? UnitId { get; set; } //don vi
        public float Quota { get; set; } //dinh muc
        public int Order { get; set; } //vi tri sap xep
        public bool IsShowOrder { get; set; } //hien thi o trang order
        public virtual Drink Drink { get; set; }
        public virtual DrinkUnit Unit { get; set; }

    }
    public class SugarOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public int DrinkId { get; set; }
        public int? UnitId { get; set; } //don vi
        public float Quota { get; set; } //dinh muc
        public int Order { get; set; } //vi tri sap xep
        public bool IsShowOrder { get; set; } //hien thi o trang order
        public virtual Drink Drink { get; set; }
        public virtual DrinkUnit Unit { get; set; }

    }
    public class DrinkSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public int Price { get; set; } //gia ban
        public int DrinkId { get; set; }
        public int? UnitId { get; set; } //don vi
        public float Quota { get; set; } //dinh muc
        public int Order { get; set; } //vi tri sap xep
        public bool IsShowOrder { get; set; } //hien thi o trang order
        public virtual Drink Drink { get; set; }
        public virtual DrinkUnit Unit { get; set; }
    }

    public class DrinkUnit {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
