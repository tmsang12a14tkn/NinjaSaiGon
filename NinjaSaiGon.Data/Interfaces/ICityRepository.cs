using NinjaSaiGon.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<CityDto> All { get; }
    }
}
