using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IDeliveryPartnerRepository
    {
        IEnumerable<DeliveryPartner> DeliveryPartners { get; }
        DeliveryPartner GetById(int id);
        void Add(DeliveryPartner deliveryPartner);
        void Update(DeliveryPartner deliveryPartner);
        bool Exists(int id);
    }
}
