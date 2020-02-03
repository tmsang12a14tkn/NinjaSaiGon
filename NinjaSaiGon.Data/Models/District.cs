using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

    }
}
