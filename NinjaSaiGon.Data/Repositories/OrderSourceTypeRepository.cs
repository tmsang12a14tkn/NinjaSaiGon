using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class OrderSourceTypeRepository : IOrderSourceTypeRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        
        public OrderSourceTypeRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<OrderSourceType> OrderSourceTypes => _appDbContext.OrderSourceTypes;

        public OrderSourceType GetById(int id) => _appDbContext.OrderSourceTypes.SingleOrDefault(m => m.Id == id);

        public void Add(OrderSourceType orderSourceType)
        {
            _appDbContext.OrderSourceTypes.Add(orderSourceType);
            _appDbContext.SaveChanges();
        }

        public void Update(OrderSourceType orderSourceType)
        {
            _appDbContext.Update(orderSourceType);
            _appDbContext.SaveChanges();
        }

        public bool Exists(int id) => _appDbContext.OrderSourceTypes.Any(e => e.Id == id);
    }
}
