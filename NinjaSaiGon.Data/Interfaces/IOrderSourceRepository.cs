using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IOrderSourceRepository
    {
        IEnumerable<OrderSource> OrderSources { get; }
        OrderSource GetById(int id);
        void Add(OrderSource orderSource);
        void Update(OrderSource orderSource);
        bool Exists(int id);
    }
}
