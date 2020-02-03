using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<PersonalNameType> PersonalNameTypes { get; }
        IEnumerable<Religion> Religions { get; }
        IEnumerable<PersonalDobType> PersonalDobTypes { get; }
        IEnumerable<MaritalStatusType> MaritalStatusTypes { get; }
        IEnumerable<Nationality> Nationalities { get; }
        IEnumerable<Ethnic> Ethnics { get; }
        IEnumerable<PersonalPhotoType> PersonalPhotoTypes { get; }
        IEnumerable<AddressType> AddressTypes { get; }
        IEnumerable<PhoneType> PhoneTypes { get; }
        IEnumerable<EmailType> EmailTypes { get; }
        IEnumerable<SocialType> SocialTypes { get; }
        IEnumerable<OttType> OTTTypes { get; }
        IEnumerable<IDDocumentType> IDDocumentTypes { get; }
        IEnumerable<BankAccountType> BankAccountTypes { get; }
        IEnumerable<BankCardType> BankCardTypes { get; }
        IEnumerable<PersonCustomerSourceType> PersonCustomerSourceTypes { get; }
        IEnumerable<Ward> Wards { get; }
        IEnumerable<DistrictPlace> DistrictPlaces { get; }
        IEnumerable<Province> Provinces { get; }
        

        Person GetById(int id);
        void Add(Person person);
        void Update(Person person);
        bool Exists(int id);


        IEnumerable<Person> Customers { get; }


        IEnumerable<Person> Employees { get; }


        IEnumerable<Person> Representatives { get; }
    }
}
