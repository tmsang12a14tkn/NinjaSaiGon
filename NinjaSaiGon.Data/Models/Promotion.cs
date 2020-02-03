using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public enum ApplyTimeType
    {
        [Display(Name = "Theo ngày")]
        Days,
        [Display(Name = "Theo giờ")]
        Hours,
        [Display(Name = "Theo ngày trong tuần")]
        DayOfWeeks
    }

    [Flags] //be treated as a bit field
    public enum DayOfWeek
    {
        [Display(Name = "Thứ 2")] Monday = 2,
        [Display(Name = "Thứ 3")] Tuesday = 4,
        [Display(Name = "Thứ 4")] Wednesday = 8,
        [Display(Name = "Thứ 5")] Thursday = 16,
        [Display(Name = "Thứ 6")] Friday = 32,
        [Display(Name = "Thứ 7")] Saturday = 64,
        [Display(Name = "Chủ nhật")] Sunday = 1
    }

    public enum ApplyFor
    {
        [Display(Name = "Tất cả")]
        All,
        [Display(Name = "Có thẻ thành viên")]
        MemberOnly
    }

    public enum ApplyRule
    {
        [Display(Name = "Giảm giá trên tổng hóa đơn")]
        DiscountOnTotal,

        [Display(Name="Mua 1 tặng 1")]
        Buy1Get1,

        [Display(Name = "Miễn phí up size và 1 topping")]
        FreeUpSizeAnd1Topping,

        [Display(Name = "Tặng món")]
        FreeDrink,

        [Display(Name = "Mua M tặng N (giá thấp nhất)")]
        BuyMGetNLowPrice,

        [Display(Name = "Mua 1 tặng 1 (món mới)")]
        Buy1Get1NewDrink
    }

    public enum DiscountType
    {
        [Display(Name = "Giảm theo tiền")]
        Money,
        [Display(Name = "Giảm theo %")]
        Percent
    }

    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PromotionCode { get; set; }
        public bool AutoPromotion { get; set; }
        public bool ApplyWithOther { get; set; } // ap dung voi cac chuong trinh khac
        public ApplyTimeType ApplyTimeType { get; set; }
        public DateTime? ApplyFromDate { get; set; }
        public DateTime? ApplyToDate { get; set; }
        public DayOfWeek? ApplyDayOfWeek { get; set; }
        public ApplyFor ApplyFor { get; set; }
        public ApplyRule ApplyRule { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountValue { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<PromotionPhoto> Photos { get; set; }
        public virtual IList<PromotionApplyHour> ApplyHours { get; set; }

        public virtual PromotionDrinkSetting PromotionDrinkSetting { get; set; }
    }

    public class PromotionPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
    }

    public class PromotionApplyHour
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public virtual Promotion Promotion { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
    }

    //dành cho khuyến mãi tặng món
    public class PromotionDrinkSetting
    {
        [Key]
        [ForeignKey("Promotion")]
        public int PromotionId { get; set; }
        //Kiểu tặng món
        public int PromotionGiftType { get; set; } //1: tiền phát sinh - 2: tổng hóa đơn - 3: mua m tặng n (rẻ nhất) - 4: mua m tặng n
        //Điều kiện khuyến mãi

        //ĐK - số tiền ít nhất
        public bool Condition_MinMoney { get; set; }
        public int Condition_MinMoneyValue { get; set; }

        //ĐK - số ly
        public bool Condition_MinDrink { get; set; }
        public int Condition_MinDrinkValue { get; set; }

        //ĐK - topping
        public bool Condition_Topping { get; set; } //yêu cầu về topping, true thì mới set điều kiện dưới

        /// <summary>
        /// true: tặng món kèm topping, false: tặng món ko kèm topping
        /// </summary>
        public bool Condition_WithTopping { get; set; }

        /// <summary>
        /// số ly được tặng
        /// </summary>
        public int PromotionDrinkQuantity { get; set; } //số ly được tặng

        /// <summary>
        /// true: áp dụng 1 lần, false : lặp lại
        /// </summary>
        public bool ApplyOneTimeOrRepeat { get; set; } //true: áp dụng 1 lần, false : lặp lại

        
        /// <summary>
        /// hình thức tặng: true tặng 1 trong các món: false: tất cả các món
        /// </summary>
        public bool PromotionForm { get; set; }

        /// <summary>
        /// kiểu sinh tặng món: true: random, false: theo thứ tự
        /// </summary>
        public bool PromotionDrinkRandom { get; set; }

        /// <summary>
        /// số ly mua - CT: mua M tặng N giá thấp nhất
        /// </summary>
        public int PromotionDrinkBuyQuantity { get; set; }

        /// <summary>
        /// số ly tặng - CT: mua M tặng N giá thấp nhất
        /// </summary>
        public int PromotionDrinkGiveQuantity { get; set; }
        

        public virtual ICollection<PromotionDrink> PromotionDrinks { get; set; }
        public virtual Promotion Promotion { get; set; }
    }

    public class PromotionDrink
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public int DrinkSizeId { get; set; }
        public string DrinkSizeName { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("PromotionDrinkSetting")]
        public int PromotionId { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual PromotionDrinkSetting PromotionDrinkSetting { get; set; }
        public virtual ICollection<PromotionDrinkTopping> PromotionDrinkToppings { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
    }

    public class PromotionDrinkTopping
    {
        public int Id { get; set; }
        public int ToppingId { get; set; }
        public int Quantity { get; set; }
        public int PromotionDrinkId { get; set; }
        public virtual Topping Topping { get; set; }
        public virtual PromotionDrink PromotionDrink { get; set; }
    }
}
