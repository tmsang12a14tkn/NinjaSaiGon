using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public enum CommissionType
    {
        [Display(Name = "Theo %")]
        Percent,
        [Display(Name = "Theo số tiền")]
        Money
    }

    public class BusinessArea
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public double Coefficient { get; set; } // he so
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual BusinessArea Parent { get; set; }
    }

    public class CommissionFormula
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CommissionType NewCusCommissionType { get; set; }
        public double NewCusCommissionValue { get; set; }
        public CommissionType OldCusCommissionType { get; set; }
        public double OldCusCommissionValue { get; set; }
        public CommissionType AreaCommissionType { get; set; }
        public double AreaCommissionValue { get; set; }
        public CommissionType AllCommissionType { get; set; }
        public double AllCommissionValue { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
    }
}
