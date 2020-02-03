using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public class ContactInfo
    {
        [Key]
        [ForeignKey("Person")]
        public int Id { get; set; }
        
        public virtual ICollection<ContactAddress> Addresses { get; set; }
        public virtual ICollection<ContactEmail> Emails { get; set; }
        public virtual ICollection<ContactPhone> Phones { get; set; }
        public virtual ICollection<ContactSocial> Socials { get; set; }
        public virtual ICollection<ContactOtt> OTTs { get; set; }
        public string RelativeInfo { get; set; }
        public virtual Person Person { get; set; }
    }

    public class ContactAddress
    {
        public int Id { get; set; }
        public int? NationalityId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictPlaceId { get; set; }
        public int? WardId { get; set; }
        public string MoreInfo { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        [ForeignKey("Type")]
        public int? TypeId { get; set; }
        public int ContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual AddressType Type { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Province Province { get; set; }
        public virtual DistrictPlace DistrictPlace { get; set; }
        public virtual Ward Ward { get; set; }
    }

    public class AddressType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ContactEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int ContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual EmailType Type { get; set; }
    }

    public class EmailType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ContactPhone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int ContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual PhoneType Type { get; set; }
    }

    public class PhoneType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ContactSocial
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int ContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual SocialType Type { get; set; }
    }

    public class SocialType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ContactOtt
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public bool IsPrimary { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public int ContactInfoId { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }
        public virtual OttType Type { get; set; }
    }

    public class OttType
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
