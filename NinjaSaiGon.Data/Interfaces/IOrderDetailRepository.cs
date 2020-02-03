using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using NinjaSaiGon.Data.Models.Dto;
using System.Linq;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IOrderDetailRepository
    {
        bool Create(OrderDetail orderDetail);
        bool Edit(OrderDetail order);
        IQueryable<OrderDetail> List { get; }
        OrderDetail GetById(int id);
        bool Delete(int id);
        bool Exists(int orderDetailId);
    }
}
