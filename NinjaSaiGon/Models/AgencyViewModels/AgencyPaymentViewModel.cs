using System;

namespace NinjaSaiGon.Admin.Models.AgencyViewModels
{
    public class AgencyPaymentViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
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
        public bool IsDeleted { get; set; }
    }
}
