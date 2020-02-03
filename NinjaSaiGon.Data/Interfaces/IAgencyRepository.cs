using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IAgencyRepository
    {
        IEnumerable<AgencyGroup> AgencyGroups { get; }
        IEnumerable<AgencyBusiness> AgencyBusinesses { get; }
        IEnumerable<CurrencyType> CurrencyTypes { get; }
        IEnumerable<PaymentType> PaymentTypes { get; }
        IEnumerable<PaymentTermType> PaymentTermTypes { get; }
        IEnumerable<AgencyDiscountType> AgencyDiscountTypes { get; }
        IEnumerable<PickupType> PickupTypes { get; }
        IEnumerable<AgencyDiscountCustomerType> AgencyDiscountCustomerTypes { get; }

        IEnumerable<Agency> Agencies { get; }
        Agency GetById(int id);
        void Add(Agency agency);
        void Update(Agency agency);
        bool Exists(int id);
    }
}
