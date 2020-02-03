using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models
{
    public class DrinkView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimarySize { get; set; }
        public string PrimaryIce { get; set; }
        public string PrimarySugar { get; set; }
        public IEnumerable<OptionView> SizeOptions { get; set; }
        public IEnumerable<OptionView> SugarOptions { get; set; }
        public IEnumerable<OptionView> IceOptions { get; set; }
    }
    public class OptionView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //public class ToppingView
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    public class DrinkCategoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DrinkView> Drinks { get; set; }

    }


}
