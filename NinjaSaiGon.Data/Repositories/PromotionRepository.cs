using System;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;
using NinjaSaiGon.Data.Models.Dto;

namespace NinjaSaiGon.Data.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PromotionRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Promotion GetPromotionById(int promotionId) => _appDbContext.Promotions.Include(d => d.Photos).Include(d => d.ApplyHours).FirstOrDefault(p => p.Id == promotionId);

        public IEnumerable<Promotion> List => _appDbContext.Promotions.Include(p=>p.Photos).Include(d => d.ApplyHours);

        public void AddPromotion(Promotion promotion)
        {
            if (promotion.Photos != null)
            {
                promotion.Photos = promotion.Photos.Where(p => !p.IsDeleted).ToList();
            }
            if (promotion.ApplyHours != null)
            {
                promotion.ApplyHours = promotion.ApplyHours.Where(p => !p.IsDeleted).ToList();
            }

            _appDbContext.Promotions.Add(promotion);
            _appDbContext.SaveChanges();
        }

        public void DeletePromotion(int promotionId)
        {
            var promotion = GetPromotionById(promotionId);
            _appDbContext.Promotions.Remove(promotion);
            _appDbContext.SaveChanges();
        }

        public bool PromotionExists(int promotionId)
        {
            return _appDbContext.Promotions.Any(promotion => promotion.Id == promotionId);
        }

        public CheckPromotionResult CheckPromotionCode(string code, bool isSameCode)
        {
            var promotion = GetPromotionByCode(code);
            var now = DateTime.Now;
            var success = true;
            if (promotion != null)
            {
                //check promotion valid
                if (promotion.ApplyTimeType == ApplyTimeType.Hours)
                {
                    if (promotion.ApplyHours == null || promotion.ApplyHours.All(h => h.From > now.TimeOfDay || h.To < now.TimeOfDay))
                    {
                        success = false;
                    }
                }
                else if (promotion.ApplyTimeType == ApplyTimeType.DayOfWeeks)
                {
                    if (!promotion.ApplyDayOfWeek.HasValue || !promotion.ApplyDayOfWeek.Value.ToString().Contains(now.DayOfWeek.ToString()))
                        success = false;
                }
                else //days
                {
                    if (now.Date < promotion.ApplyFromDate || now.Date > promotion.ApplyToDate)
                    {
                        success = false;
                    }
                }
                if (success == false)
                    return new CheckPromotionResult
                    {
                        Success = false
                    };
                var promotionDrinkRandom = false;
                if (promotion.ApplyRule == ApplyRule.FreeDrink)
                {
                    var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(promotion.Id);
                    if(promotionDrinkSetting != null)
                    {
                        promotionDrinkRandom = promotionDrinkSetting.PromotionDrinkRandom;
                    }
                }
                var buyQuantity = 0;
                var giveQuantity = 0;
                if (promotion.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                {
                    var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(promotion.Id);
                    if (promotionDrinkSetting != null)
                    {
                        buyQuantity = promotionDrinkSetting.PromotionDrinkBuyQuantity;
                        giveQuantity = promotionDrinkSetting.PromotionDrinkGiveQuantity;
                    }
                    else
                    {
                        return new CheckPromotionResult
                        {
                            Success = false
                        };
                    }
                }
                return new CheckPromotionResult
                {
                    Success = true,
                    DiscountType = promotion.DiscountType,
                    DiscountValue = promotion.DiscountValue,
                    ApplyRule = promotion.ApplyRule,
                    PromotionId = promotion.Id,
                    PromotionDrinkRandom = promotionDrinkRandom,
                    BuyQuantity = buyQuantity,
                    GiveQuantity = giveQuantity
                };
            }

            //Private Promotion Code
            var privatePromotionCode = GetPrivatePromotionByCode(code);
            if (privatePromotionCode != null && ((privatePromotionCode.Status && !isSameCode) || isSameCode))
            {
                var privatePromotion = _appDbContext.PrivatePromotions.Find(privatePromotionCode.PrivatePromotionId);
                if(privatePromotion != null && ((privatePromotion.IsActive && !isSameCode) || isSameCode))
                {
                    //check promotion valid
                    if (!isSameCode)
                    {
                        if (!privatePromotionCode.IsInfinityTime &&
                        ((privatePromotionCode.StartDate.HasValue && now.Date < privatePromotionCode.StartDate)
                            || (privatePromotionCode.EndDate.HasValue && now.Date > privatePromotionCode.EndDate)))
                        {
                            success = false;
                        }
                        else if (!privatePromotionCode.IsInfinityUse && privatePromotionCode.CurrentUseCode >= privatePromotionCode.MaxUseCode)
                        {
                            success = false;
                        }
                    }
                    
                    if (!success)
                        return new CheckPromotionResult
                        {
                            Success = false
                        };
                    var promotionDrinkRandom = false;
                    if (privatePromotion.ApplyRule == ApplyRule.FreeDrink)
                    {
                        var promotionDrinkSetting = _appDbContext.PrivatePromotionDrinkSettings.Find(privatePromotion.Id);
                        if (promotionDrinkSetting != null)
                        {
                            promotionDrinkRandom = promotionDrinkSetting.PromotionDrinkRandom;
                        }
                    }
                    var buyQuantity = 0;
                    var giveQuantity = 0;
                    if (privatePromotion.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                    {
                        var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(privatePromotion.Id);
                        if (promotionDrinkSetting != null)
                        {
                            buyQuantity = promotionDrinkSetting.PromotionDrinkBuyQuantity;
                            giveQuantity = promotionDrinkSetting.PromotionDrinkGiveQuantity;
                        }
                        else
                        {
                            return new CheckPromotionResult
                            {
                                Success = false
                            };
                        }
                    }
                    return new CheckPromotionResult
                    {
                        Success = true,
                        DiscountType = privatePromotion.DiscountType,
                        DiscountValue = privatePromotion.DiscountValue,
                        ApplyRule = privatePromotion.ApplyRule,
                        PromotionId = privatePromotion.Id,
                        PromotionDrinkRandom = promotionDrinkRandom,
                        BuyQuantity = buyQuantity,
                        GiveQuantity = giveQuantity,
                        IsPrivatePromotion = true
                    };
                }
            }

            return new CheckPromotionResult
            {
                Success = false
            };
        }

        public void UpdatePromotion(Promotion promotion)
        {
            if (promotion.Photos != null)
            {
                foreach (var applyHourModel in promotion.Photos)
                {
                    if (applyHourModel.Id != 0)
                    {
                        var applyHour = _appDbContext.PromotionPhotos.Find(applyHourModel.Id);
                        _appDbContext.Entry(applyHour).State = EntityState.Detached;
                        //deleted
                        if (applyHourModel.IsDeleted)
                            _appDbContext.PromotionPhotos.Remove(applyHour);
                        else
                        {
                            applyHour.IsPrimary = applyHourModel.IsPrimary;
                            _appDbContext.Update(applyHour);
                        }
                    }
                    else
                    {
                        if (!applyHourModel.IsDeleted)
                        {
                            applyHourModel.PromotionId = promotion.Id;
                            _appDbContext.PromotionPhotos.Add(applyHourModel);
                        }
                    }
                }
            }
            if (promotion.ApplyHours != null)
            {
                foreach (var applyHourModel in promotion.ApplyHours)
                {
                    if (applyHourModel.Id != 0)
                    {
                        var applyHour = _appDbContext.PromotionApplyHours.Find(applyHourModel.Id);
                        _appDbContext.Entry(applyHour).State = EntityState.Detached;
                        //deleted
                        if (applyHourModel.IsDeleted)
                            _appDbContext.PromotionApplyHours.Remove(applyHour);
                        else
                        {
                            applyHour.From = applyHourModel.From;
                            applyHour.To = applyHourModel.To;
                            _appDbContext.Update(applyHour);
                        }
                    }
                    else
                    {
                        if (!applyHourModel.IsDeleted)
                        {
                            applyHourModel.PromotionId = promotion.Id;
                            _appDbContext.PromotionApplyHours.Add(applyHourModel);
                        }
                    }
                }
            }

            _appDbContext.Entry(promotion).State = EntityState.Modified;
            _appDbContext.Update(promotion);
            _appDbContext.SaveChanges();
        }

        public Promotion GetPromotionByCode(string code) => _appDbContext.Promotions.Include(p=>p.ApplyHours).FirstOrDefault(p=>p.PromotionCode == code);

        public PrivatePromotionCode GetPrivatePromotionByCode(string code) => _appDbContext.PrivatePromotionCodes.FirstOrDefault(p => p.Code == code);
        public bool CheckPromotionBuy1Get1()
        {
            var isAutoPromo = false;
            var promotion = _appDbContext.Promotions.Include(p => p.ApplyHours).Where(p => p.ApplyRule == ApplyRule.Buy1Get1 && p.AutoPromotion && p.IsActive).FirstOrDefault();
            if (promotion != null)
            {
                var now = DateTime.Now;
                if (promotion.ApplyTimeType == ApplyTimeType.Hours)
                {
                    if (promotion.ApplyHours != null && promotion.ApplyHours.All(h => h.From <= now.TimeOfDay || h.To >= now.TimeOfDay))
                    {
                        isAutoPromo = true;
                    }
                }
                else if (promotion.ApplyTimeType == ApplyTimeType.DayOfWeeks)
                {
                    if (promotion.ApplyDayOfWeek.HasValue && promotion.ApplyDayOfWeek.Value.ToString().Contains(now.DayOfWeek.ToString()))
                        isAutoPromo = true;
                }
                else //days
                {
                    if (now.Date >= promotion.ApplyFromDate && now.Date <= promotion.ApplyToDate)
                    {
                        isAutoPromo = true;
                    }
                }
            }

            return isAutoPromo;
        }

        public PromotionDrinkSetting GetPromotionDrinkSettingById(int promotionId)
        {
            return _appDbContext.PromotionDrinkSettings.Include(s=>s.PromotionDrinks).ThenInclude(s=>s.Drink).FirstOrDefault(s=>s.PromotionId == promotionId);
        }

        public PromotionDrink GetPromotionDrinkById(int promotionDrinkId)
        {
            return _appDbContext.PromotionDrinks.Include(s => s.Drink).Include(s=>s.PromotionDrinkToppings).FirstOrDefault(s => s.Id == promotionDrinkId);
        }
        
        public bool SavePromotionDrinkSetting(PromotionDrinkSetting setting)
        {
            var isExistSetting  = _appDbContext.PromotionDrinkSettings.Any(s=>s.PromotionId == setting.PromotionId);
            if(isExistSetting == false)
            {
                _appDbContext.PromotionDrinkSettings.Add(setting);
            }
            else
            {
                if (setting.PromotionDrinks != null)
                {
                    foreach (var promotionDrink in setting.PromotionDrinks)
                    {
                        if (promotionDrink.IsDeleted == true)
                            _appDbContext.Remove(promotionDrink);
                        else if (promotionDrink.Id <= 0)
                            _appDbContext.Add(promotionDrink);
                        else
                        {
                            var oldPromotionDrinkToppingIds = _appDbContext.PromotionDrinkToppings.Where(t => t.PromotionDrinkId == promotionDrink.Id).Select(t=>t.Id).ToList();
                            if (promotionDrink.PromotionDrinkToppings != null)
                            {
                                foreach (var promotionDrinkTopping in promotionDrink.PromotionDrinkToppings)
                                {
                                    if (promotionDrinkTopping.Id > 0 && promotionDrinkTopping.Quantity == 0)
                                    {
                                        _appDbContext.Remove(promotionDrinkTopping);
                                    }
                                    else if (promotionDrinkTopping.Id == 0)
                                    {
                                        _appDbContext.Add(promotionDrinkTopping);
                                    }
                                    else if (promotionDrinkTopping.Id > 0)
                                    {
                                        oldPromotionDrinkToppingIds.Remove(promotionDrinkTopping.Id);
                                        _appDbContext.Update(promotionDrinkTopping);

                                    }
                                }
                                
                            }
                            //remove all old items
                            oldPromotionDrinkToppingIds.ForEach(id => { _appDbContext.Remove(_appDbContext.PromotionDrinkToppings.Find(id)); });
                            _appDbContext.Update(promotionDrink);
                        }
                    }
                }
                _appDbContext.Update(setting);
            }
            _appDbContext.SaveChanges();

            return true;
        }
    }
}