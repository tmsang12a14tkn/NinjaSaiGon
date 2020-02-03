using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PromotionDrinkSettingViewComponent: ViewComponent
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionDrinkSettingViewComponent(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public IViewComponentResult Invoke(int promotionId)
        {
            var setting = _promotionRepository.GetPromotionDrinkSettingById(promotionId);
            if (setting == null)
                setting = new Data.Models.PromotionDrinkSetting
                {
                    PromotionId = promotionId,
                    PromotionDrinks = new List<PromotionDrink>()
                };
            return View(setting);
        }
    }
}
