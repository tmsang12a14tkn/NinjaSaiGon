using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using NinjaSaiGon.Data.Models.Dto;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IPromotionRepository
    {
        Promotion GetPromotionById(int promotionId);
        PromotionDrinkSetting GetPromotionDrinkSettingById(int promotionId);
        PromotionDrink GetPromotionDrinkById(int promotionDrinkId);
        bool SavePromotionDrinkSetting(PromotionDrinkSetting setting);
        Promotion GetPromotionByCode(string code);
        IEnumerable<Promotion> List { get; }
        void AddPromotion(Promotion promotion);
        void UpdatePromotion(Promotion promotion);
        void DeletePromotion(int promotionId);
        bool PromotionExists(int promotionId);
        CheckPromotionResult CheckPromotionCode(string code, bool isSameCode);
        bool CheckPromotionBuy1Get1();
    }
}
