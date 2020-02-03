using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PrivatePromotionDrinkSettingViewComponent : ViewComponent
    {
        private readonly IPrivatePromotionRepository _privatePromotionRepository;

        public PrivatePromotionDrinkSettingViewComponent(IPrivatePromotionRepository privatePromotionRepository)
        {
            _privatePromotionRepository = privatePromotionRepository;
        }

        public IViewComponentResult Invoke(int ppId)
        {
            var setting = _privatePromotionRepository.GetPrivatePromotionDrinkSettingById(ppId);
            if (setting == null)
                setting = new PrivatePromotionDrinkSetting
                {
                    PrivatePromotionId = ppId,
                    PrivatePromotionDrinks = new List<PrivatePromotionDrink>()
                };
            return View(setting);
        }
    }
}
