using NinjaSaiGon.Data.Models;
using System;
using System.Linq;
using Xunit;
using DayOfWeek = NinjaSaiGon.Data.Models.DayOfWeek;

namespace NinjaSaigon.Test
{
    public class PromotionTests: TestBase
    {

        [Fact]
        public void PromotionValidTest()
        {
            var promotion = new Promotion()
            {
                ApplyTimeType = ApplyTimeType.DayOfWeeks,
                ApplyDayOfWeek = DayOfWeek.Saturday |DayOfWeek.Monday,
                PromotionCode = "CCC",
                DiscountType = DiscountType.Money,
                DiscountValue = 5000
            };
            var success = true;
            var now = new DateTime(2018,8,25,10,8,2);
            //check promotion valid
            if (promotion.ApplyTimeType == ApplyTimeType.Hours)
            {
                if (promotion.ApplyHours.All(h => h.From > now.TimeOfDay || h.To < now.TimeOfDay))
                {
                    success = false;
                }
            }
            else if (promotion.ApplyTimeType == ApplyTimeType.DayOfWeeks)
            {
                if (!promotion.ApplyDayOfWeek.ToString().Contains(now.DayOfWeek.ToString()))
                    success = false;
            }
            else //days
            {
                if (now.Date < promotion.ApplyFromDate || now.Date > promotion.ApplyToDate)
                {
                    success = false;
                }
            }
            //Assert.Contains(DateTime.Now.DayOfWeek.ToString(), (DayOfWeek.Saturday | DayOfWeek.Monday).ToString());
            Assert.True(success);
            
        }
    }
}
