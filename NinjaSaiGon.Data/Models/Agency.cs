using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public enum AgencyType
    {
        [Display(Name = "Cá nhân")]
        Personal,
        [Display(Name = "Tổ chức")]
        Organization
    }

    public class Agency
    {
        public int Id { get; set; }
        public AgencyType AgencyType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TradingName { get; set; }
        public string TaxCode { get; set; }
        public string BusinessRegNumber { get; set; }
        public DateTime StartDate { get; set; }
        public int? AgencyGroupId { get; set; }
        public int? AgencyBusinessId { get; set; }

        public virtual AgencyContactInfo AgencyContactInfo { get; set; }
        public virtual AgencyGroup AgencyGroup { get; set; }
        public virtual AgencyBusiness AgencyBusiness { get; set; }
        public virtual ICollection<AgencyPayment> AgencyPayments { get; set; }
        public virtual ICollection<AgencyBank> AgencyBanks { get; set; }
        public virtual ICollection<AgencyRepresentative> AgencyRepresentatives { get; set; }
        public virtual ICollection<AgencyDelivery> AgencyDeliveries { get; set; }
        public virtual ICollection<AgencyCare> AgencyCares { get; set; }
    }

    public class AgencyCare
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual Person Employee { get; set; }
    }

    public class AgencyDelivery
    {
        public int Id { get; set; }
        public int? PickupTypeId { get; set; }
        public int AgencyId { get; set; }
        public int? AgencyDiscountCustomerTypeId { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual PickupType PickupType { get; set; }
        public virtual AgencyDiscountCustomerType AgencyDiscountCustomerType { get; set; }
    }

    public class AgencyDiscountCustomerType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AgencyPayment
    {
        public int Id { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? CurrencyTypeId { get; set; }
        public int? PaymentTermTypeId { get; set; }
        public int PaymentLimit { get; set; } //han muc thanh toan
        public double InterestRate { get; set; } // lai suat
        public int PaymentCredit { get; set; } // han muc tin dung
        public int InvoiceLimit { get; set; } //han muc hoa don
        public int? AgencyDiscountTypeId { get; set; }
        public double DiscountAmount { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AgencyId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual PaymentTermType PaymentTermType { get; set; }
        public virtual AgencyDiscountType AgencyDiscountType { get; set; }
    }

    public class PickupType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AgencyDiscountType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PaymentTermType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class PaymentType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class CurrencyType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AgencyBank
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string SwiftCode { get; set; }
        public string BankBranch { get; set; }
        public int? BankAccountTypeId { get; set; }
        public int AgencyId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual BankAccountType BankAccountType { get; set; }
        public virtual ICollection<AgencyBankCard> AgencyBankCards { get; set; }
    }

    public class AgencyBankCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpiredDate { get; set; }
        public int? BankCardTypeId { get; set; }
        public int AgencyBankId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyBank AgencyBank { get; set; }
        public virtual BankCardType BankCardType { get; set; }
    }

    public class AgencyRepresentative
    {
        public int PersonId { get; set; }
        public int AgencyId { get; set; }
        public bool IsMainContact { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public virtual Person Person { get; set; }
        public virtual Agency Agency { get; set; }
    }

    public class AgencyGroup
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AgencyBusiness
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class AgencyContactInfo
    {
        [Key]
        [ForeignKey("Agency")]
        public int Id { get; set; }

        public virtual ICollection<AgencyContactAddress> Addresses { get; set; }
        public virtual ICollection<AgencyContactEmail> Emails { get; set; }
        public virtual ICollection<AgencyContactPhone> Phones { get; set; }
        public virtual ICollection<AgencyContactSocial> Socials { get; set; }
        public virtual ICollection<AgencyContactOtt> OTTs { get; set; }
        public virtual Agency Agency { get; set; }
    }

    public class AgencyContactAddress
    {
        public int Id { get; set; }
        public int? NationalityId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictPlaceId { get; set; }
        public int? WardId { get; set; }
        public string MoreInfo { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public int AgencyContactInfoId { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyContactInfo ContactInfo { get; set; }

        public virtual AddressType Type { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Province Province { get; set; }
        public virtual DistrictPlace DistrictPlace { get; set; }
        public virtual Ward Ward { get; set; }
    }

    public class AgencyContactEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int AgencyContactInfoId { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyContactInfo ContactInfo { get; set; }
        public virtual EmailType Type { get; set; }
    }

    public class AgencyContactPhone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int AgencyContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyContactInfo ContactInfo { get; set; }
        public virtual PhoneType Type { get; set; }
    }

    public class AgencyContactSocial
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int AgencyContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyContactInfo ContactInfo { get; set; }
        public virtual SocialType Type { get; set; }
    }

    public class AgencyContactOtt
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int AgencyContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual AgencyContactInfo ContactInfo { get; set; }
        public virtual OttType Type { get; set; }
    }
}
