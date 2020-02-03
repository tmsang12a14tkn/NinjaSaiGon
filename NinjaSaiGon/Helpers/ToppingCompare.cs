using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Helpers
{
    public class ToppingCompare : IEqualityComparer<Topping>
    {
        public bool Equals(Topping x, Topping y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Topping obj)
        {
           return 1;
        }
    }
}
