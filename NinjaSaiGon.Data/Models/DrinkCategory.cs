using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class DrinkCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int? ParentId { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; } //vị trí
        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual DrinkCategory Parent { get; set; }
        public virtual DrinkCategoryType Type { get; set; }
        public virtual ICollection<CategoryIceOption> CategoryIceOptions { get; set; }
        public virtual ICollection<CategorySugarOption> CategorySugarOptions { get; set; }
        public virtual IList<CategoryPhoto> Photos { get; set; }
    }
    public class DrinkCategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryIceOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public int DrinkCategoryId { get; set; }
        public virtual DrinkCategory DrinkCategory { get; set; }

    }
    public class CategorySugarOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public int DrinkCategoryId { get; set; }
        public virtual DrinkCategory DrinkCategory { get; set; }
    }
    public class CategoryPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public int DrinkCategoryId { get; set; }

        public virtual DrinkCategory DrinkCategory { get; set; }
    }
    //public class DrinkCategorySize
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsPrimary { get; set; }
    //    public bool IsHidden { get; set; }
    //    public int Prize { get; set; }
    //    public int DrinkCategoryId { get; set; }
    //    public virtual DrinkCategory DrinkCategory { get; set; }
    //}
}
