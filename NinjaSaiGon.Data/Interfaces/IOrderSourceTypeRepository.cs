using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IOrderSourceTypeRepository
    {
        IEnumerable<OrderSourceType> OrderSourceTypes { get; }
        OrderSourceType GetById(int id);
        void Add(OrderSourceType orderSourceType);
        void Update(OrderSourceType orderSourceType);
        bool Exists(int id);
    }
}
