using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MatchingNames { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
