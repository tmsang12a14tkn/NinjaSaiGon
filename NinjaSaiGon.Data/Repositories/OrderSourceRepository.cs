using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class OrderSourceRepository : IOrderSourceRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        
        public OrderSourceRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<OrderSource> OrderSources => _appDbContext.OrderSources.Include(o => o.OrderSourceType);

        public OrderSource GetById(int id) => _appDbContext.OrderSources.SingleOrDefault(m => m.Id == id);

        public void Add(OrderSource orderSource)
        {
            _appDbContext.OrderSources.Add(orderSource);
            _appDbContext.SaveChanges();
        }

        public void Update(OrderSource orderSource)
        {
            _appDbContext.Update(orderSource);
            _appDbContext.SaveChanges();
        }

        public bool Exists(int id) => _appDbContext.OrderSources.Any(e => e.Id == id);
    }
}
