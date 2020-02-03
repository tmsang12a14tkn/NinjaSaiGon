using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class DeliveryPartnerRepository : IDeliveryPartnerRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        
        public DeliveryPartnerRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<DeliveryPartner> DeliveryPartners => _appDbContext.DeliveryPartners;

        public DeliveryPartner GetById(int id) => _appDbContext.DeliveryPartners.SingleOrDefault(m => m.Id == id);

        public void Add(DeliveryPartner deliveryPartner)
        {
            _appDbContext.DeliveryPartners.Add(deliveryPartner);
            _appDbContext.SaveChanges();
        }

        public void Update(DeliveryPartner deliveryPartner)
        {
            _appDbContext.Update(deliveryPartner);
            _appDbContext.SaveChanges();
        }

        public bool Exists(int id) => _appDbContext.DeliveryPartners.Any(e => e.Id == id);
    }
}
