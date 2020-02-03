using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IPrivatePromotionRepository
    {
        PrivatePromotion GetPrivatePromotionById(int ppId);
        PrivatePromotionDrinkSetting GetPrivatePromotionDrinkSettingById(int ppId);
        PrivatePromotionDrink GetPrivatePromotionDrinkById(int ppDrinkId);
        bool SavePrivatePromotionDrinkSetting(PrivatePromotionDrinkSetting setting);
        IEnumerable<PrivatePromotion> List { get; }
        IEnumerable<PrivatePromotionCode> AllCode { get; }
        void AddPrivatePromotion(PrivatePromotion privatePromotion);
        void UpdatePrivatePromotion(PrivatePromotion privatePromotion);
        void DeletePrivatePromotion(int ppId);
        bool PrivatePromotionExists(int ppId);

        PrivatePromotionCode GetPrivatePromotionCodeById(int codeId);
        void AddPrivatePromotionCode(PrivatePromotionCode privatePromotionCode);
        void DeleteCode(int codeId);
        void UpdateCode(PrivatePromotionCode privatePromotionCode);
    }
}
