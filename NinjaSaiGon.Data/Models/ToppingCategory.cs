using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Models
{
    public class ToppingCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; } //vị trí

        public virtual ToppingCategory Parent { get; set; }
        public virtual ICollection<Topping> Toppings { get; set; }

        public virtual IList<DrinkToppingCategory> DrinkToppingCategories { get; set; }
    }
}
