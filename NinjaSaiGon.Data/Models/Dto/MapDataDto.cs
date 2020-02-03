using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models.Dto
{
    public class MapDataDto
    {
        public string Country { get; set; }
        public IEnumerable<CityDto> Cities { get; set; }
       
    }

    public class CityDto
    {
        public int Id { get; set; }
        public string[] Name { get; set; }
        public IEnumerable<DistrictDto> Districts { get; set; }

    }
    public class DistrictDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
