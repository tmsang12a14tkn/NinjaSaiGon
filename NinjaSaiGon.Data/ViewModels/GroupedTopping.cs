using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.ViewModels
{
    public class GroupedTopping
    {
        public  int ToppingCategoryId { get; set; }
        public string Name { get; set; }
        public int? Min { get; set; } //toi thieu yeu cau
        public int? Max { get; set; } //toi da dc chon
        public List<Topping> Toppings { get; set; }

        public GroupedTopping GetMinMax(DrinkToppingCategory dtc)
        {
            this.Min = dtc.Min;
            this.Max = dtc.Max;
            return this;
        }
    }
}
