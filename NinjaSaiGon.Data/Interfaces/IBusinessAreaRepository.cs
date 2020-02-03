using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IBusinessAreaRepository
    {
        IEnumerable<BusinessArea> BusinessAreas { get; }
        BusinessArea GetById(int id);
        void Add(BusinessArea businessArea);
        void Update(BusinessArea businessArea);
        bool Exists(int id);
    }
}
