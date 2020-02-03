using NinjaSaiGon.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public enum TitlePrefix
    {
        [Display(Name = "Ông")]
        Sir,
        [Display(Name = "Bà")]
        Madam,
        [Display(Name = "Anh")]
        Mr,
        [Display(Name = "Chị")]
        Mrs,
        [Display(Name = "Cô")]
        Miss
    }

    public enum PersonType
    {
        [Display(Name = "Nhân viên")]
        Employee,
        [Display(Name = "Khách hàng")]
        Customer,
        [Display(Name = "Người đại diện")]
        Representative
    }

    public enum GenderType
    {
        [Display(Name = "Nam")]
        Male,
        [Display(Name = "Nữ")]
        Female,
        [Display(Name = "Khác")]
        Other
    }

    public class Person
    {
        public Person()
        {
            IsHaveAccount = false;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public TitlePrefix TitlePrefixType { get; set; }
        public PersonType PersonType { get; set; }
        public GenderType GenderId { get; set; }
        public int? EthnicId { get; set; }
        public int? ReligionId { get; set; }
        public int? PersonLevelId { get; set; }
        public int? PersonCustomerSourceTypeId { get; set; }
        [ForeignKey("CustomerSourceEmp")]
        public int? CustomerSourceEmpId { get; set; }
        [ForeignKey("CustomerSourceCus")]
        public int? CustomerSourceCusId { get; set; }
        public int? AgencyId { get; set; }
        public int? SocialTypeId { get; set; }
        public string CustomerSourceNote { get; set; }
        public bool IsDeleted { get; set; }
        public string PrimaryName { get; set; }
        public string PrimaryDOB { get; set; }
        public string PrimaryPhoto { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Person CustomerSourceEmp { get; set; }
        public virtual Person CustomerSourceCus { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual SocialType SocialType { get; set; }
        public virtual ICollection<PersonalNationality> Nationalities { get; set; }
        public virtual ICollection<PersonalName> Names { get; set; }
        public virtual ICollection<VehicleInfo> Vehicles { get; set; }
        public virtual ICollection<PersonalPhoto> Photos { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual ICollection<PersonalDob> DOBs { get; set; }
        public virtual PersonalPob POB { get; set; }
        public virtual PersonalHomeTown HomeTown { get; set; }
        public virtual Ethnic Ethnic { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual PersonLevel PersonLevel { get; set; }
        public virtual PersonCustomerSourceType PersonCustomerSourceType { get; set; }
        public virtual ICollection<MaritalStatus> MaritalStatuses { get; set; }
        public virtual ICollection<PersonCustomerCare> CustomerRepresentativeCares { get; set; }
        public virtual ICollection<PersonCustomerCare> EmployeeCares { get; set; }
        public virtual ICollection<IDCard> IDCards { get; set; }
        public virtual ICollection<Passport> Passports { get; set; }
        public virtual ICollection<IDDocument> IDDocuments { get; set; }
        public virtual ICollection<PersonBank> PersonBanks { get; set; }
        public virtual ICollection<PersonCustomerWorking> PersonCustomerWorkings { get; set; }
        public virtual ICollection<PersonEmployeeWorking> PersonEmployeeWorkings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        [NotMapped]
        public bool IsHaveAccount { get; set; }
        [NotMapped]
        public virtual PersonUserView PersonUser { get; set; }
    }

    public class PersonCustomerCare
    {
        public int Id { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual Person Employee { get; set; }
    }

    public class PersonCustomerSourceType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonLevel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Points { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }

    public class PersonalName
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public bool IsPrimary { get; set; } //la ten duoc dung, neu co nhieu ten cung loai 

        public virtual Person Person { get; set; }
        public virtual PersonalNameType Type { get; set; }

        public string FullName { get; set; }
    }

    public class PersonalNameType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonalDob
    {
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }


        public virtual Person Person { get; set; }
        public virtual PersonalDobType Type { get; set; }
    }

    public class PersonalPob
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string MoreInfo { get; set; }

        public virtual Person Person { get; set; }
    }

    public class PersonalHomeTown
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string MoreInfo { get; set; }

        public virtual Person Person { get; set; }
    }

    public class PersonalDobType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonalNationality
    {
        public int PersonId { get; set; }
        public int NationalityId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual Nationality Nationality { get; set; }
    }

    public class PersonalPhoto
    {
        public int Id { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public string Url { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public bool IsPrimary { get; set; }

        public virtual Person Person { get; set; }
        public virtual PersonalPhotoType Type { get; set; }
    }

    public class PersonalPhotoType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class Ethnic
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class Religion
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class Nationality
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class MaritalStatus
    {
        public int Id { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int PersonId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IsCurrent { get; set; }
        public virtual Person Person { get; set; }
        public virtual MaritalStatusType Type { get; set; }
    }

    public class MaritalStatusType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class IDCard
    {
        public int Id { get; set; }
        public string IDNumber { get; set; }
        public string ProvidedDate { get; set; } //ngay cap
        public string ExpriredDate { get; set; } //ngay het han
        public string ProvidedPlace { get; set; } //noi cap
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
    }

    public class Passport
    {
        public int Id { get; set; }
        public string IDNumber { get; set; }
        public string ProvidedDate { get; set; } //ngay cap
        public string ExpriredDate { get; set; } //ngay het han
        public string ProvidedPlace { get; set; } //noi cap
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
    }

    public class IDDocument
    {
        public int Id { get; set; }
        public int? IDDocumentTypeId { get; set; }
        public string IDNumber { get; set; }
        public string ProvidedDate { get; set; } //ngay cap
        public string ExpriredDate { get; set; } //ngay het han
        public string ProvidedPlace { get; set; } //noi cap
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual IDDocumentType IDDocumentType { get; set; }
    }

    public class IDDocumentType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonBank
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string SwiftCode { get; set; }
        public string BankBranch { get; set; }
        public int? BankAccountTypeId { get; set; }
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual BankAccountType BankAccountType { get; set; }
        public virtual ICollection<BankCard> BankCards { get; set; }
    }

    public class BankAccountType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class BankCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpiredDate { get; set; }
        public int? BankCardTypeId { get; set; }
        public int PersonBankId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual PersonBank PersonBank { get; set; }
        public virtual BankCardType BankCardType { get; set; }
    }

    public class BankCardType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PersonCustomerWorking
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsMainPosition { get; set; }
        public bool IsWorking { get; set; }
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
    }

    public class PersonEmployeeWorking
    {
        public int Id { get; set; }
        public int DepartmentPositionId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? PositionBeginDate { get; set; }
        public DateTime? PositionEndDate { get; set; }
        public DateTime? TrialWorkBeginDate { get; set; }
        public DateTime? WorkBeginDate { get; set; }
        public bool IsMainPosition { get; set; }
        public bool IsWorking { get; set; }
        public int PersonId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual Department Department { get; set; }
        public virtual DepartmentPosition DepartmentPosition { get; set; }
    }
}
