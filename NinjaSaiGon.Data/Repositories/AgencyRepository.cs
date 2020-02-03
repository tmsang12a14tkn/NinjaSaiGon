using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaSaiGon.Data.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public AgencyRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<AgencyGroup> AgencyGroups => _appDbContext.AgencyGroups;
        public IEnumerable<AgencyBusiness> AgencyBusinesses => _appDbContext.AgencyBusinesses;
        public IEnumerable<CurrencyType> CurrencyTypes => _appDbContext.CurrencyTypes;
        public IEnumerable<PaymentType> PaymentTypes => _appDbContext.PaymentTypes;
        public IEnumerable<PaymentTermType> PaymentTermTypes => _appDbContext.PaymentTermTypes;
        public IEnumerable<AgencyDiscountType> AgencyDiscountTypes => _appDbContext.AgencyDiscountTypes;
        public IEnumerable<PickupType> PickupTypes => _appDbContext.PickupTypes;
        public IEnumerable<AgencyDiscountCustomerType> AgencyDiscountCustomerTypes => _appDbContext.AgencyDiscountCustomerTypes;

        public IEnumerable<Agency> Agencies => _appDbContext.Agencies.Include(p => p.AgencyContactInfo).Include("AgencyContactInfo.Phones").Include("AgencyContactInfo.Emails");

        public Agency GetById(int id) => _appDbContext.Agencies
                                                    .Include(p => p.AgencyContactInfo).Include("AgencyContactInfo.Phones")
                                                    .Include("AgencyContactInfo.Emails").Include("AgencyContactInfo.Addresses")
                                                    .Include("AgencyContactInfo.Socials").Include("AgencyContactInfo.OTTs")
                                                    .Include(p => p.AgencyCares).Include("AgencyCares.Employee")
                                                    .Include(p => p.AgencyDeliveries).Include("AgencyDeliveries.PickupType")
                                                    .Include("AgencyDeliveries.AgencyDiscountCustomerType")
                                                    .Include(p => p.AgencyBanks).Include("AgencyBanks.AgencyBankCards")
                                                    .Include(p => p.AgencyPayments)
                                                    .Include("AgencyPayments.PaymentType").Include("AgencyPayments.CurrencyType")
                                                    .Include("AgencyPayments.PaymentTermType").Include("AgencyPayments.AgencyDiscountType")
                                                    .Include(p => p.AgencyCares).Include("AgencyCares.Employee")
                                                    .Include(p => p.AgencyRepresentatives).ThenInclude(ar => ar.Person)
                                                    .ThenInclude(arp => arp.PersonCustomerWorkings)
                                                    .SingleOrDefault(m => m.Id == id);

        public void Add(Agency agency)
        {
            agency.Code += "A" + HiResDateTime.UtcNowTicks;

            if (agency.AgencyContactInfo.Phones != null)
            {
                for (int i = agency.AgencyContactInfo.Phones.Count - 1; i >= 0; i--)
                {
                    var phone = agency.AgencyContactInfo.Phones.ElementAt(i);
                    if (phone.IsDeleted)
                    {
                        agency.AgencyContactInfo.Phones.Remove(phone);
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Emails != null)
            {
                for (int i = agency.AgencyContactInfo.Emails.Count - 1; i >= 0; i--)
                {
                    var email = agency.AgencyContactInfo.Emails.ElementAt(i);
                    if (email.IsDeleted)
                    {
                        agency.AgencyContactInfo.Emails.Remove(email);
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Addresses != null)
            {
                for (int i = agency.AgencyContactInfo.Addresses.Count - 1; i >= 0; i--)
                {
                    var address = agency.AgencyContactInfo.Addresses.ElementAt(i);
                    if (address.IsDeleted)
                    {
                        agency.AgencyContactInfo.Addresses.Remove(address);
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Socials != null)
            {
                for (int i = agency.AgencyContactInfo.Socials.Count - 1; i >= 0; i--)
                {
                    var social = agency.AgencyContactInfo.Socials.ElementAt(i);
                    if (social.IsDeleted)
                    {
                        agency.AgencyContactInfo.Socials.Remove(social);
                    }
                }
            }
            
            if (agency.AgencyContactInfo.OTTs != null)
            {
                for (int i = agency.AgencyContactInfo.OTTs.Count - 1; i >= 0; i--)
                {
                    var ott = agency.AgencyContactInfo.OTTs.ElementAt(i);
                    if (ott.IsDeleted)
                    {
                        agency.AgencyContactInfo.OTTs.Remove(ott);
                    }
                }
            }
            
            if (agency.AgencyBanks != null)
            {
                for (int i = agency.AgencyBanks.Count - 1; i >= 0; i--)
                {
                    var bank = agency.AgencyBanks.ElementAt(i);
                    if (bank.IsDeleted)
                    {
                        agency.AgencyBanks.Remove(bank);
                    }
                    else
                    {
                        if (bank.AgencyBankCards != null)
                        {
                            for (int j = bank.AgencyBankCards.Count - 1; j >= 0; j--)
                            {
                                var card = bank.AgencyBankCards.ElementAt(j);
                                if (card.IsDeleted)
                                {
                                    bank.AgencyBankCards.Remove(card);
                                }
                            }
                        }                        
                    }
                }
            }

            if (agency.AgencyCares != null)
            {
                for (int i = agency.AgencyCares.Count - 1; i >= 0; i--)
                {
                    var aCare = agency.AgencyCares.ElementAt(i);
                    if (aCare.IsDeleted)
                    {
                        agency.AgencyCares.Remove(aCare);
                    }
                }
            }

            if (agency.AgencyRepresentatives != null)
            {
                for (int i = agency.AgencyRepresentatives.Count - 1; i >= 0; i--)
                {
                    var aRepresentative = agency.AgencyRepresentatives.ElementAt(i);
                    if (aRepresentative.IsDeleted)
                    {
                        agency.AgencyRepresentatives.Remove(aRepresentative);
                    }
                }
            }

            if (agency.AgencyPayments != null)
            {
                for (int i = agency.AgencyPayments.Count - 1; i >= 0; i--)
                {
                    var aPayments = agency.AgencyPayments.ElementAt(i);
                    if (aPayments.IsDeleted)
                    {
                        agency.AgencyPayments.Remove(aPayments);
                    }
                }
            }

            if (agency.AgencyDeliveries != null)
            {
                for (int i = agency.AgencyDeliveries.Count - 1; i >= 0; i--)
                {
                    var aDelivery = agency.AgencyDeliveries.ElementAt(i);
                    if (aDelivery.IsDeleted)
                    {
                        agency.AgencyDeliveries.Remove(aDelivery);
                    }
                }
            }

            _appDbContext.Agencies.Add(agency);
            _appDbContext.SaveChanges();
        }

        public void Update(Agency agency)
        {
            _appDbContext.Entry(agency.AgencyContactInfo).State = EntityState.Modified;
            
            if (agency.AgencyContactInfo.Phones != null)
            {
                for (int i = agency.AgencyContactInfo.Phones.Count - 1; i >= 0; i--)
                {
                    var phone = agency.AgencyContactInfo.Phones.ElementAt(i);
                    if (phone.IsDeleted)
                    {
                        if (phone.Id > 0)
                            _appDbContext.Entry(phone).State = EntityState.Deleted;
                        else
                            agency.AgencyContactInfo.Phones.Remove(phone);
                    }
                    else
                    {
                        _appDbContext.Entry(phone).State = phone.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Emails != null)
            {
                for (int i = agency.AgencyContactInfo.Emails.Count - 1; i >= 0; i--)
                {
                    var email = agency.AgencyContactInfo.Emails.ElementAt(i);
                    if (email.IsDeleted)
                    {
                        if (email.Id > 0)
                            _appDbContext.Entry(email).State = EntityState.Deleted;
                        else
                            agency.AgencyContactInfo.Emails.Remove(email);
                    }
                    else
                    {
                        _appDbContext.Entry(email).State = email.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Addresses != null)
            {
                for (int i = agency.AgencyContactInfo.Addresses.Count - 1; i >= 0; i--)
                {
                    var address = agency.AgencyContactInfo.Addresses.ElementAt(i);
                    if (address.IsDeleted)
                    {
                        if (address.Id > 0)
                            _appDbContext.Entry(address).State = EntityState.Deleted;
                        else
                            agency.AgencyContactInfo.Addresses.Remove(address);
                    }
                    else
                    {
                        _appDbContext.Entry(address).State = address.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }
            
            if (agency.AgencyContactInfo.Socials != null)
            {
                for (int i = agency.AgencyContactInfo.Socials.Count - 1; i >= 0; i--)
                {
                    var social = agency.AgencyContactInfo.Socials.ElementAt(i);
                    if (social.IsDeleted)
                    {
                        if (social.Id > 0)
                            _appDbContext.Entry(social).State = EntityState.Deleted;
                        else
                            agency.AgencyContactInfo.Socials.Remove(social);
                    }
                    else
                    {
                        _appDbContext.Entry(social).State = social.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }
            
            if (agency.AgencyContactInfo.OTTs != null)
            {
                for (int i = agency.AgencyContactInfo.OTTs.Count - 1; i >= 0; i--)
                {
                    var ott = agency.AgencyContactInfo.OTTs.ElementAt(i);
                    if (ott.IsDeleted)
                    {
                        if (ott.Id > 0)
                            _appDbContext.Entry(ott).State = EntityState.Deleted;
                        else
                            agency.AgencyContactInfo.OTTs.Remove(ott);
                    }
                    else
                    {
                        _appDbContext.Entry(ott).State = ott.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (agency.AgencyBanks != null)
            {
                for (int i = agency.AgencyBanks.Count - 1; i >= 0; i--)
                {
                    var bank = agency.AgencyBanks.ElementAt(i);
                    if (bank.IsDeleted)
                    {
                        if (bank.Id > 0)
                            _appDbContext.Entry(bank).State = EntityState.Deleted;
                        else
                            agency.AgencyBanks.Remove(bank);
                    }
                    else
                    {
                        _appDbContext.Entry(bank).State = bank.Id <= 0 ? EntityState.Added : EntityState.Modified;

                        if (bank.AgencyBankCards != null)
                        {
                            for (int j = bank.AgencyBankCards.Count - 1; j >= 0; j--)
                            {
                                var card = bank.AgencyBankCards.ElementAt(j);
                                if (card.IsDeleted)
                                {
                                    if (card.Id > 0)
                                        _appDbContext.Entry(card).State = EntityState.Deleted;
                                    else
                                        bank.AgencyBankCards.Remove(card);
                                }
                                else
                                {
                                    _appDbContext.Entry(card).State = card.Id <= 0 ? EntityState.Added : EntityState.Modified;
                                }
                            }
                        }                        
                    }
                }
            }
            
            if (agency.AgencyCares != null)
            {
                for (int i = agency.AgencyCares.Count - 1; i >= 0; i--)
                {
                    var aCare = agency.AgencyCares.ElementAt(i);
                    if (aCare.IsDeleted)
                    {
                        if (aCare.Id > 0)
                            _appDbContext.Entry(aCare).State = EntityState.Deleted;
                        else
                            agency.AgencyCares.Remove(aCare);
                    }
                    else
                    {
                        _appDbContext.Entry(aCare).State = aCare.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (agency.AgencyRepresentatives != null)
            {
                for (int i = agency.AgencyRepresentatives.Count - 1; i >= 0; i--)
                {
                    var aRepresentative = agency.AgencyRepresentatives.ElementAt(i);
                    if (aRepresentative.IsDeleted)
                    {
                        if (aRepresentative.AgencyId > 0)
                            _appDbContext.Entry(aRepresentative).State = EntityState.Deleted;
                        else
                            agency.AgencyRepresentatives.Remove(aRepresentative);
                    }
                    else
                    {
                        _appDbContext.Entry(aRepresentative).State = aRepresentative.AgencyId <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (agency.AgencyPayments != null)
            {
                for (int i = agency.AgencyPayments.Count - 1; i >= 0; i--)
                {
                    var aPayments = agency.AgencyPayments.ElementAt(i);
                    if (aPayments.IsDeleted)
                    {
                        if (aPayments.AgencyId > 0)
                            _appDbContext.Entry(aPayments).State = EntityState.Deleted;
                        else
                            agency.AgencyPayments.Remove(aPayments);
                    }
                    else
                    {
                        _appDbContext.Entry(aPayments).State = aPayments.AgencyId <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (agency.AgencyDeliveries != null)
            {
                for (int i = agency.AgencyDeliveries.Count - 1; i >= 0; i--)
                {
                    var aDelivery = agency.AgencyDeliveries.ElementAt(i);
                    if (aDelivery.IsDeleted)
                    {
                        if (aDelivery.AgencyId > 0)
                            _appDbContext.Entry(aDelivery).State = EntityState.Deleted;
                        else
                            agency.AgencyDeliveries.Remove(aDelivery);
                    }
                    else
                    {
                        _appDbContext.Entry(aDelivery).State = aDelivery.AgencyId <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            _appDbContext.Update(agency);
            _appDbContext.SaveChanges();
        }

        public bool Exists(int id) => _appDbContext.Agencies.Any(e => e.Id == id);
    }
}
