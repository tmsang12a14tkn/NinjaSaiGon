using System;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;
using NinjaSaiGon.Data.Models.Dto;

namespace NinjaSaiGon.Data.Repositories
{
    public class PrivatePromotionRepository : IPrivatePromotionRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public PrivatePromotionRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public PrivatePromotion GetPrivatePromotionById(int ppId) => _appDbContext.PrivatePromotions.Include(d => d.Photos).Include(d => d.PrivatePromotionCodes).FirstOrDefault(p => p.Id == ppId);

        public IEnumerable<PrivatePromotion> List => _appDbContext.PrivatePromotions.Include(p => p.Photos).Include(d => d.PrivatePromotionCodes).Where(p => p.IsActive);

        public IEnumerable<PrivatePromotionCode> AllCode => _appDbContext.PrivatePromotionCodes.Include(c => c.PrivatePromotion).Where(c => !c.IsDeleted).OrderByDescending(c => c.CreatedDate);

        public void AddPrivatePromotion(PrivatePromotion privatePromotion)
        {
            if (privatePromotion.Photos != null)
            {
                privatePromotion.Photos = privatePromotion.Photos.Where(p => !p.IsDeleted).ToList();
            }

            _appDbContext.PrivatePromotions.Add(privatePromotion);
            _appDbContext.SaveChanges();
        }

        public void DeletePrivatePromotion(int ppId)
        {
            var privatePromotion = GetPrivatePromotionById(ppId);
            _appDbContext.PrivatePromotions.Remove(privatePromotion);
            _appDbContext.SaveChanges();
        }

        public bool PrivatePromotionExists(int ppId)
        {
            return _appDbContext.PrivatePromotions.Any(p => p.Id == ppId);
        }

        public void UpdatePrivatePromotion(PrivatePromotion privatePromotion)
        {
            if (privatePromotion.Photos != null)
            {
                foreach (var photoModel in privatePromotion.Photos)
                {
                    if (photoModel.Id != 0)
                    {
                        var ppPhoto = _appDbContext.PrivatePromotionPhotos.Find(photoModel.Id);
                        _appDbContext.Entry(ppPhoto).State = EntityState.Detached;
                        //deleted
                        if (photoModel.IsDeleted)
                            _appDbContext.PrivatePromotionPhotos.Remove(ppPhoto);
                        else
                        {
                            ppPhoto.IsPrimary = photoModel.IsPrimary;
                            _appDbContext.Update(ppPhoto);
                        }
                    }
                    else
                    {
                        if (!photoModel.IsDeleted)
                        {
                            photoModel.PrivatePromotionId = privatePromotion.Id;
                            _appDbContext.PrivatePromotionPhotos.Add(photoModel);
                        }
                    }
                }
            }

            _appDbContext.Entry(privatePromotion).State = EntityState.Modified;
            _appDbContext.Update(privatePromotion);
            _appDbContext.SaveChanges();
        }
        
        public PrivatePromotionDrinkSetting GetPrivatePromotionDrinkSettingById(int ppId)
        {
            return _appDbContext.PrivatePromotionDrinkSettings.Include(s => s.PrivatePromotionDrinks).ThenInclude(s => s.Drink).FirstOrDefault(s => s.PrivatePromotionId == ppId);
        }

        public PrivatePromotionDrink GetPrivatePromotionDrinkById(int ppDrinkId)
        {
            return _appDbContext.PrivatePromotionDrinks.Include(s => s.Drink).Include(s => s.PrivatePromotionDrinkToppings).FirstOrDefault(s => s.Id == ppDrinkId);
        }

        public bool SavePrivatePromotionDrinkSetting(PrivatePromotionDrinkSetting setting)
        {
            var isExistSetting = _appDbContext.PrivatePromotionDrinkSettings.Any(s => s.PrivatePromotionId == setting.PrivatePromotionId);
            if (!isExistSetting)
            {
                _appDbContext.PrivatePromotionDrinkSettings.Add(setting);
            }
            else
            {
                if (setting.PrivatePromotionDrinks != null)
                {
                    foreach (var promotionDrink in setting.PrivatePromotionDrinks)
                    {
                        if (promotionDrink.IsDeleted)
                            _appDbContext.Remove(promotionDrink);
                        else if (promotionDrink.Id <= 0)
                            _appDbContext.Add(promotionDrink);
                        else
                        {
                            var oldPromotionDrinkToppingIds = _appDbContext.PrivatePromotionDrinkToppings.Where(t => t.PrivatePromotionDrinkId == promotionDrink.Id).Select(t => t.Id).ToList();
                            if (promotionDrink.PrivatePromotionDrinkToppings != null)
                            {
                                foreach (var promotionDrinkTopping in promotionDrink.PrivatePromotionDrinkToppings)
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
                            oldPromotionDrinkToppingIds.ForEach(id => { _appDbContext.Remove(_appDbContext.PrivatePromotionDrinkToppings.Find(id)); });
                            _appDbContext.Update(promotionDrink);
                        }
                    }
                }
                _appDbContext.Update(setting);
            }
            _appDbContext.SaveChanges();

            return true;
        }

        public void AddPrivatePromotionCode(PrivatePromotionCode privatePromotionCode)
        {
            _appDbContext.PrivatePromotionCodes.Add(privatePromotionCode);
            _appDbContext.SaveChanges();
        }

        public PrivatePromotionCode GetPrivatePromotionCodeById(int codeId) => _appDbContext.PrivatePromotionCodes.FirstOrDefault(c => c.Id == codeId);

        public void UpdateCode(PrivatePromotionCode privatePromotionCode)
        {
            _appDbContext.Entry(privatePromotionCode).State = EntityState.Modified;
            _appDbContext.Update(privatePromotionCode);
            _appDbContext.SaveChanges();
        }

        public void DeleteCode(int codeId)
        {
            var privateCode = _appDbContext.PrivatePromotionCodes.Find(codeId);
            privateCode.IsDeleted = true;

            _appDbContext.Entry(privateCode).State = EntityState.Modified;
            _appDbContext.Update(privateCode);
            _appDbContext.SaveChanges();
        }
    }
}