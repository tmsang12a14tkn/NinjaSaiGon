using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using NinjaSaiGon.Data.Models.Dto;
using System.Linq;
using System;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IOrderRepository
    {
        bool Create(Order order);
        bool Edit(Order order);
        IQueryable<Order> All { get; }
        IQueryable<Order> GetAll { get; }
        IQueryable<Order> List { get; }
        IEnumerable<Order> TodayList { get; }
        Order GetById(int id);
        IEnumerable<OrderDetail> GetDetailsByOrderId(int id);
        IEnumerable<Order> Query(string searchBy,int take,int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        void Delete(int id);
        bool Exists(int orderId);
        Order Normalize(Order order);
        Order UpdatePromotionFreeDrinkCount(Order order);
        CheckPromotionResult CheckPromotionCode(string code, DateTime orderPlaced, bool isSameCode);
        List<string> GetHtml(int id);
    }
}
