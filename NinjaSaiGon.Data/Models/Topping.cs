using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string OtherName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quota { get; set; }
        public int? UnitId { get; set; }
        public bool IsShared { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public int Position { get; set; }
        public bool OutOfStock { get; set; }
        public int QuickCreateIndex { get; set; }
        public DateTime? OutOfStockFrom { get; set; }
        public DateTime? OutOfStockTo { get; set; }
        public virtual ToppingCategory Category { get; set; }
        public virtual DrinkUnit Unit { get; set; }
        public virtual ICollection<DrinkTopping> DrinkToppings { get; set; }
        public virtual ICollection<OrderDetailTopping> OrderDetailToppings { get; set; }
        public virtual ICollection<ToppingPhoto> Photos { get; set; }

        [NotMapped]
        public ToppingPhoto PrimaryPhoto { get { return Photos?.OrderByDescending(t => t.IsPrimary).FirstOrDefault(); } }
    }
    public class ToppingPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public int ToppingId { get; set; }

        public virtual Topping Topping { get; set; }
    }
}
