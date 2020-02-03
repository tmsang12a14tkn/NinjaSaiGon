using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DistrictPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }

    public class Ward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictPlaceId { get; set; }
        public virtual DistrictPlace DistrictPlace { get; set; }
    }
}
