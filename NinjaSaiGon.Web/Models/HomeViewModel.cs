using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Detection;

namespace NinjaSaiGon.Web.Models
{
    public class HomeViewModel
    {
        public List<DrinkCategory> DrinkCategories { get; set; }
        public DeviceType DeviceType { get; set; }
        public List<Topping> Toppings { get; set; }
    }
}
