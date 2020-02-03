using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PersonRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //TYPES
        public IEnumerable<PersonalNameType> PersonalNameTypes => _appDbContext.PersonalNameTypes;
        public IEnumerable<Religion> Religions => _appDbContext.Religions;
        public IEnumerable<PersonalDobType> PersonalDobTypes => _appDbContext.PersonalDOBTypes;
        public IEnumerable<MaritalStatusType> MaritalStatusTypes => _appDbContext.MaritalStatusTypes;
        public IEnumerable<Nationality> Nationalities => _appDbContext.Nationalities;
        public IEnumerable<Ethnic> Ethnics => _appDbContext.Ethnics;
        public IEnumerable<PersonalPhotoType> PersonalPhotoTypes => _appDbContext.PersonalPhotoTypes;
        public IEnumerable<AddressType> AddressTypes => _appDbContext.AddressTypes;
        public IEnumerable<PhoneType> PhoneTypes => _appDbContext.PhoneTypes;
        public IEnumerable<EmailType> EmailTypes => _appDbContext.EmailTypes;
        public IEnumerable<SocialType> SocialTypes => _appDbContext.SocialTypes;
        public IEnumerable<OttType> OTTTypes => _appDbContext.OTTTypes;
        public IEnumerable<IDDocumentType> IDDocumentTypes => _appDbContext.IDDocumentTypes;
        public IEnumerable<BankAccountType> BankAccountTypes => _appDbContext.BankAccountTypes;
        public IEnumerable<BankCardType> BankCardTypes => _appDbContext.BankCardTypes;
        public IEnumerable<PersonCustomerSourceType> PersonCustomerSourceTypes => _appDbContext.PersonCustomerSourceTypes;
        public IEnumerable<Ward> Wards => _appDbContext.Wards;
        public IEnumerable<DistrictPlace> DistrictPlaces => _appDbContext.DistrictPlaces;
        public IEnumerable<Province> Provinces => _appDbContext.Provinces;

        
        public Person GetById(int id) => _appDbContext.Persons
                                                    .Include(p => p.Names).Include(p => p.Nationalities)
                                                    .Include(p => p.Vehicles).Include(p => p.Photos)
                                                    .Include(p => p.DOBs).Include(p => p.MaritalStatuses)
                                                    .Include(p => p.IDCards).Include(p => p.Passports)
                                                    .Include(p => p.IDDocuments).Include(p => p.HomeTown)
                                                    .Include(p => p.PersonCustomerWorkings)
                                                    .Include(p => p.PersonEmployeeWorkings)
                                                    .Include(p => p.PersonBanks).Include("PersonBanks.BankCards")
                                                    .Include(p => p.ContactInfo).Include("ContactInfo.Phones")
                                                    .Include("ContactInfo.Emails").Include("ContactInfo.Addresses")
                                                    .Include("ContactInfo.Socials").Include("ContactInfo.OTTs")
                                                    .Include(p => p.CustomerRepresentativeCares)
                                                    .Include("CustomerRepresentativeCares.Employee")
                                                    .Include(p => p.CustomerSourceCus).Include(p => p.CustomerSourceEmp)
                                                    .Include(p => p.Agency)
                                                    .SingleOrDefault(m => m.Id == id);
        public void Add(Person person)
        {
            if (person.PersonType == PersonType.Customer)
                person.Code = "C";
            else if (person.PersonType == PersonType.Employee)
                person.Code = "E";
            else if (person.PersonType == PersonType.Representative)
                person.Code = "AR";
            person.Code += HiResDateTime.UtcNowTicks;

            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).LastName))
                person.PrimaryName += person.Names.ElementAt(0).LastName + " ";

            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).MidName))
                person.PrimaryName += person.Names.ElementAt(0).MidName + " ";

            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).FirstName))
                person.PrimaryName += person.Names.ElementAt(0).FirstName;

            person.PrimaryDOB = $"{person.DOBs.ElementAt(0).Day}/{person.DOBs.ElementAt(0).Month}/{person.DOBs.ElementAt(0).Year}";

            if (person.Nationalities != null)
            {
                for (int i = person.Nationalities.Count - 1; i >= 0; i--)
                {
                    var nation = person.Nationalities.ElementAt(i);
                    if (nation.IsDeleted)
                    {
                        person.Nationalities.Remove(nation);
                    }
                }
            }

            if (person.IDCards != null)
            {
                for (int i = person.IDCards.Count - 1; i >= 0; i--)
                {
                    var idCard = person.IDCards.ElementAt(i);
                    if (idCard.IsDeleted)
                    {
                        person.IDCards.Remove(idCard);
                    }
                }
            }

            if (person.Passports != null)
            {
                for (int i = person.Passports.Count - 1; i >= 0; i--)
                {
                    var passport = person.Passports.ElementAt(i);
                    if (passport.IsDeleted)
                    {
                        person.Passports.Remove(passport);
                    }
                }
            }

            if (person.IDDocuments != null)
            {
                for (int i = person.IDDocuments.Count - 1; i >= 0; i--)
                {
                    var document = person.IDDocuments.ElementAt(i);
                    if (document.IsDeleted)
                    {
                        person.IDDocuments.Remove(document);
                    }
                }
            }

            if (person.ContactInfo.Phones != null)
            {
                for (int i = person.ContactInfo.Phones.Count - 1; i >= 0; i--)
                {
                    var phone = person.ContactInfo.Phones.ElementAt(i);
                    if (phone.IsDeleted)
                    {
                        person.ContactInfo.Phones.Remove(phone);
                    }
                }
            }

            if (person.ContactInfo.Emails != null)
            {
                for (int i = person.ContactInfo.Emails.Count - 1; i >= 0; i--)
                {
                    var email = person.ContactInfo.Emails.ElementAt(i);
                    if (email.IsDeleted)
                    {
                        person.ContactInfo.Emails.Remove(email);
                    }
                }
            }

            if (person.ContactInfo.Addresses != null)
            {
                for (int i = person.ContactInfo.Addresses.Count - 1; i >= 0; i--)
                {
                    var address = person.ContactInfo.Addresses.ElementAt(i);
                    if (address.IsDeleted)
                    {
                        person.ContactInfo.Addresses.Remove(address);
                    }
                }
            }

            if (person.ContactInfo.Socials != null)
            {
                for (int i = person.ContactInfo.Socials.Count - 1; i >= 0; i--)
                {
                    var social = person.ContactInfo.Socials.ElementAt(i);
                    if (social.IsDeleted)
                    {
                        person.ContactInfo.Socials.Remove(social);
                    }
                }
            }

            if (person.ContactInfo.OTTs != null)
            {
                for (int i = person.ContactInfo.OTTs.Count - 1; i >= 0; i--)
                {
                    var ott = person.ContactInfo.OTTs.ElementAt(i);
                    if (ott.IsDeleted)
                    {
                        person.ContactInfo.OTTs.Remove(ott);
                    }
                }
            }

            if (person.PersonBanks != null)
            {
                for (int i = person.PersonBanks.Count - 1; i >= 0; i--)
                {
                    var bank = person.PersonBanks.ElementAt(i);
                    if (bank.IsDeleted)
                    {
                        person.PersonBanks.Remove(bank);
                    }
                    else
                    {
                        if (bank.BankCards != null)
                        {
                            for (int j = bank.BankCards.Count - 1; j >= 0; j--)
                            {
                                var card = bank.BankCards.ElementAt(j);
                                if (card.IsDeleted)
                                {
                                    bank.BankCards.Remove(card);
                                }
                            }
                        }
                    }
                }
            }

            if (person.PersonCustomerWorkings != null)
            {
                for (int i = person.PersonCustomerWorkings.Count - 1; i >= 0; i--)
                {
                    var pWorking = person.PersonCustomerWorkings.ElementAt(i);
                    if (pWorking.IsDeleted)
                    {
                        person.PersonCustomerWorkings.Remove(pWorking);
                    }
                }
            }

            if (person.PersonEmployeeWorkings != null)
            {
                for (int i = person.PersonEmployeeWorkings.Count - 1; i >= 0; i--)
                {
                    var eWorking = person.PersonEmployeeWorkings.ElementAt(i);
                    if (eWorking.IsDeleted)
                    {
                        person.PersonEmployeeWorkings.Remove(eWorking);
                    }
                }
            }

            if (person.CustomerRepresentativeCares != null)
            {
                for (int i = person.CustomerRepresentativeCares.Count - 1; i >= 0; i--)
                {
                    var cusCare = person.CustomerRepresentativeCares.ElementAt(i);
                    if (cusCare.IsDeleted || cusCare.EmployeeId == 0)
                    {
                        person.CustomerRepresentativeCares.Remove(cusCare);
                    }
                }
            }

            _appDbContext.Persons.Add(person);
            _appDbContext.SaveChanges();
        }
        public void Update(Person person)
        {
            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).LastName))
                person.PrimaryName += person.Names.ElementAt(0).LastName + " ";

            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).MidName))
                person.PrimaryName += person.Names.ElementAt(0).MidName + " ";

            if (!string.IsNullOrEmpty(person.Names.ElementAt(0).FirstName))
                person.PrimaryName += person.Names.ElementAt(0).FirstName;

            person.PrimaryDOB = $"{person.DOBs.ElementAt(0).Day}/{person.DOBs.ElementAt(0).Month}/{person.DOBs.ElementAt(0).Year}";
            _appDbContext.Entry(person.DOBs.ElementAt(0)).State = EntityState.Modified;
            _appDbContext.Entry(person.ContactInfo).State = EntityState.Modified;

            if (person.Nationalities != null)
            {
                for (int i = person.Nationalities.Count - 1; i >= 0; i--)
                {
                    var nation = person.Nationalities.ElementAt(i);
                    if (nation.IsDeleted)
                    {
                        if (nation.PersonId > 0)
                            _appDbContext.Entry(nation).State = EntityState.Deleted;
                        else
                            person.Nationalities.Remove(nation);
                    }
                    else
                    {
                        _appDbContext.Entry(nation).State = nation.PersonId <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.IDCards != null)
            {
                for (int i = person.IDCards.Count - 1; i >= 0; i--)
                {
                    var idCard = person.IDCards.ElementAt(i);
                    if (idCard.IsDeleted)
                    {
                        if (idCard.Id > 0)
                            _appDbContext.Entry(idCard).State = EntityState.Deleted;
                        else
                            person.IDCards.Remove(idCard);
                    }
                    else
                    {
                        _appDbContext.Entry(idCard).State = idCard.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.Passports != null)
            {
                for (int i = person.Passports.Count - 1; i >= 0; i--)
                {
                    var passport = person.Passports.ElementAt(i);
                    if (passport.IsDeleted)
                    {
                        if (passport.Id > 0)
                            _appDbContext.Entry(passport).State = EntityState.Deleted;
                        else
                            person.Passports.Remove(passport);
                    }
                    else
                    {
                        _appDbContext.Entry(passport).State = passport.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.IDDocuments != null)
            {
                for (int i = person.IDDocuments.Count - 1; i >= 0; i--)
                {
                    var document = person.IDDocuments.ElementAt(i);
                    if (document.IsDeleted)
                    {
                        if (document.Id > 0)
                            _appDbContext.Entry(document).State = EntityState.Deleted;
                        else
                            person.IDDocuments.Remove(document);
                    }
                    else
                    {
                        _appDbContext.Entry(document).State = document.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.ContactInfo.Phones != null)
            {
                for (int i = person.ContactInfo.Phones.Count - 1; i >= 0; i--)
                {
                    var phone = person.ContactInfo.Phones.ElementAt(i);
                    if (phone.IsDeleted)
                    {
                        if (phone.Id > 0)
                            _appDbContext.Entry(phone).State = EntityState.Deleted;
                        else
                            person.ContactInfo.Phones.Remove(phone);
                    }
                    else
                    {
                        _appDbContext.Entry(phone).State = phone.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.ContactInfo.Emails != null)
            {
                for (int i = person.ContactInfo.Emails.Count - 1; i >= 0; i--)
                {
                    var email = person.ContactInfo.Emails.ElementAt(i);
                    if (email.IsDeleted)
                    {
                        if (email.Id > 0)
                            _appDbContext.Entry(email).State = EntityState.Deleted;
                        else
                            person.ContactInfo.Emails.Remove(email);
                    }
                    else
                    {
                        _appDbContext.Entry(email).State = email.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.ContactInfo.Addresses != null)
            {
                for (int i = person.ContactInfo.Addresses.Count - 1; i >= 0; i--)
                {
                    var address = person.ContactInfo.Addresses.ElementAt(i);
                    if (address.IsDeleted)
                    {
                        if (address.Id > 0)
                            _appDbContext.Entry(address).State = EntityState.Deleted;
                        else
                            person.ContactInfo.Addresses.Remove(address);
                    }
                    else
                    {
                        _appDbContext.Entry(address).State = address.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.ContactInfo.Socials != null)
            {
                for (int i = person.ContactInfo.Socials.Count - 1; i >= 0; i--)
                {
                    var social = person.ContactInfo.Socials.ElementAt(i);
                    if (social.IsDeleted)
                    {
                        if (social.Id > 0)
                            _appDbContext.Entry(social).State = EntityState.Deleted;
                        else
                            person.ContactInfo.Socials.Remove(social);
                    }
                    else
                    {
                        _appDbContext.Entry(social).State = social.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.ContactInfo.OTTs != null)
            {
                for (int i = person.ContactInfo.OTTs.Count - 1; i >= 0; i--)
                {
                    var ott = person.ContactInfo.OTTs.ElementAt(i);
                    if (ott.IsDeleted)
                    {
                        if (ott.Id > 0)
                            _appDbContext.Entry(ott).State = EntityState.Deleted;
                        else
                            person.ContactInfo.OTTs.Remove(ott);
                    }
                    else
                    {
                        _appDbContext.Entry(ott).State = ott.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.PersonBanks != null)
            {
                for (int i = person.PersonBanks.Count - 1; i >= 0; i--)
                {
                    var bank = person.PersonBanks.ElementAt(i);
                    if (bank.IsDeleted)
                    {
                        if (bank.Id > 0)
                            _appDbContext.Entry(bank).State = EntityState.Deleted;
                        else
                            person.PersonBanks.Remove(bank);
                    }
                    else
                    {
                        _appDbContext.Entry(bank).State = bank.Id <= 0 ? EntityState.Added : EntityState.Modified;

                        if (bank.BankCards != null)
                        {
                            for (int j = bank.BankCards.Count - 1; j >= 0; j--)
                            {
                                var card = bank.BankCards.ElementAt(j);
                                if (card.IsDeleted)
                                {
                                    if (card.Id > 0)
                                        _appDbContext.Entry(card).State = EntityState.Deleted;
                                    else
                                        bank.BankCards.Remove(card);
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

            if (person.PersonCustomerWorkings != null)
            {
                for (int i = person.PersonCustomerWorkings.Count - 1; i >= 0; i--)
                {
                    var pWorking = person.PersonCustomerWorkings.ElementAt(i);
                    if (pWorking.IsDeleted)
                    {
                        if (pWorking.Id > 0)
                            _appDbContext.Entry(pWorking).State = EntityState.Deleted;
                        else
                            person.PersonCustomerWorkings.Remove(pWorking);
                    }
                    else
                    {
                        _appDbContext.Entry(pWorking).State = pWorking.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.PersonEmployeeWorkings != null)
            {
                for (int i = person.PersonEmployeeWorkings.Count - 1; i >= 0; i--)
                {
                    var eWorking = person.PersonEmployeeWorkings.ElementAt(i);
                    if (eWorking.IsDeleted)
                    {
                        if (eWorking.Id > 0)
                            _appDbContext.Entry(eWorking).State = EntityState.Deleted;
                        else
                            person.PersonEmployeeWorkings.Remove(eWorking);
                    }
                    else
                    {
                        _appDbContext.Entry(eWorking).State = eWorking.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            if (person.CustomerRepresentativeCares != null)
            {
                for (int i = person.CustomerRepresentativeCares.Count - 1; i >= 0; i--)
                {
                    var pcCare = person.CustomerRepresentativeCares.ElementAt(i);
                    if (pcCare.IsDeleted)
                    {
                        if (pcCare.Id > 0)
                            _appDbContext.Entry(pcCare).State = EntityState.Deleted;
                        else
                            person.CustomerRepresentativeCares.Remove(pcCare);
                    }
                    else
                    {
                        _appDbContext.Entry(pcCare).State = pcCare.Id <= 0 ? EntityState.Added : EntityState.Modified;
                    }
                }
            }

            _appDbContext.Update(person);
            _appDbContext.SaveChanges();
        }
        public bool Exists(int id) => _appDbContext.Persons.Any(e => e.Id == id);


        //CUSTOMER
        public IEnumerable<Person> Customers => _appDbContext.Persons
                                                            .Include(p => p.Orders)
                                                            .Include(p => p.ContactInfo).Include("ContactInfo.Phones")
                                                            .Include("ContactInfo.Emails").Include(p => p.PersonCustomerWorkings)
                                                            .Where(p => p.PersonType == PersonType.Customer);


        //EMPLOYEE
        public IEnumerable<Person> Employees => _appDbContext.Persons
                                                            .Include(p => p.ContactInfo).Include("ContactInfo.Phones").Include("ContactInfo.Emails")
                                                            .Include(p => p.PersonEmployeeWorkings).Include("PersonEmployeeWorkings.Department")
                                                            .Include("PersonEmployeeWorkings.DepartmentPosition")
                                                            .Where(p => p.PersonType == PersonType.Employee);


        //AGENCY REPRESENTATIVE
        public IEnumerable<Person> Representatives => _appDbContext.Persons
                                                            .Include(p => p.ContactInfo).Include("ContactInfo.Phones")
                                                            .Include("ContactInfo.Emails").Include(p => p.PersonCustomerWorkings)
                                                            .Where(p => p.PersonType == PersonType.Representative);
    }
}
