using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public class PrivatePromotion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ApplyWithOther { get; set; } // ap dung voi cac chuong trinh khac
        public ApplyTimeType ApplyTimeType { get; set; }
        public ApplyFor ApplyFor { get; set; }
        public ApplyRule ApplyRule { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountValue { get; set; }
        public bool IsActive { get; set; }

        public virtual IList<PrivatePromotionPhoto> Photos { get; set; }
        public virtual IList<PrivatePromotionCode> PrivatePromotionCodes { get; set; }
        public virtual PrivatePromotionDrinkSetting PrivatePromotionDrinkSetting { get; set; }
    }

    public class PrivatePromotionCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int MaxUseCode { get; set; }
        public int CurrentUseCode { get; set; }
        public bool IsInfinityUse { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsInfinityTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CodeComment { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public int PrivatePromotionId { get; set; }

        public virtual PrivatePromotion PrivatePromotion { get; set; }
    }

    public class PrivatePromotionPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PhysicalPath { get; set; }
        public bool IsPrimary { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
        public int PrivatePromotionId { get; set; }

        public virtual PrivatePromotion PrivatePromotion { get; set; }
    }

    public class PrivatePromotionDrinkSetting
    {
        [Key]
        [ForeignKey("PrivatePromotion")]
        public int PrivatePromotionId { get; set; }
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


        public virtual ICollection<PrivatePromotionDrink> PrivatePromotionDrinks { get; set; }
        public virtual PrivatePromotion PrivatePromotion { get; set; }
    }

    public class PrivatePromotionDrink
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public int DrinkSizeId { get; set; }
        public string DrinkSizeName { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("PrivatePromotionDrinkSetting")]
        public int PrivatePromotionId { get; set; }
        public virtual Drink Drink { get; set; }
        public virtual PrivatePromotionDrinkSetting PrivatePromotionDrinkSetting { get; set; }
        public virtual ICollection<PrivatePromotionDrinkTopping> PrivatePromotionDrinkToppings { get; set; }
        [NotMapped]
        public bool IsDeleted { get; set; }
    }

    public class PrivatePromotionDrinkTopping
    {
        public int Id { get; set; }
        public int ToppingId { get; set; }
        public int Quantity { get; set; }
        public int PrivatePromotionDrinkId { get; set; }
        public virtual Topping Topping { get; set; }
        public virtual PrivatePromotionDrink PrivatePromotionDrink { get; set; }
    }
}
