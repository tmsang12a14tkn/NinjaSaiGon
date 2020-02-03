using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Order.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace NinjaSaiGon.Order.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly INotifyPopupRepository _notifyPopupRepository;

        public HomeController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository, IPromotionRepository promotionRepository, INotifyPopupRepository notifyPopupRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
            _promotionRepository = promotionRepository;
            _notifyPopupRepository = notifyPopupRepository;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                IsInBuy1Get1 = _promotionRepository.CheckPromotionBuy1Get1() ? 1 : 0,
                NotifyPopup = _notifyPopupRepository.GetActiveNotifyPopup(),
                DrinkCategories = _categoryRepository.Categories.ToList()
            };
            
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
