using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Admin.Models.PromotionViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PrivatePromotionDrinkNewItemViewComponent : ViewComponent
    {
        private readonly IPrivatePromotionRepository _privatePromotionRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IToppingRepository _toppingRepository;

        public PrivatePromotionDrinkNewItemViewComponent(IPrivatePromotionRepository privatePromotionRepository, IDrinkRepository drinkRepository, IToppingRepository toppingRepository)
        {
            _privatePromotionRepository = privatePromotionRepository;
            _drinkRepository = drinkRepository;
            _toppingRepository = toppingRepository;
        }

        public IViewComponentResult Invoke(PromotionDrinkItemView promotionDrinkViewModel)
        {
            var drink = _drinkRepository.GetDrinkById(promotionDrinkViewModel.DrinkId);
            var drinkSize = drink.DrinkSizes.FirstOrDefault(s => s.Id == promotionDrinkViewModel.DrinkSizeId) ?? drink.PrimarySize;

            if (drinkSize == null)
            {
                throw new ApplicationException("DrinkSize is invalid!");
            }

            promotionDrinkViewModel.PromotionDrinkToppings = promotionDrinkViewModel.PromotionDrinkToppings?.ToList();
            Dictionary<int, Topping> toppingDictionary = promotionDrinkViewModel.PromotionDrinkToppings?.Select(t => new KeyValuePair<int, Topping>(t.ToppingId, _toppingRepository.GetToppingById(t.ToppingId))).ToDictionary(k => k.Key, k => k.Value);

            promotionDrinkViewModel.CategoryId = drink.CategoryId;
            promotionDrinkViewModel.DrinkName = drink.Name;
            promotionDrinkViewModel.Price = drinkSize.Price;
            promotionDrinkViewModel.FullPrice = drinkSize.Price + (promotionDrinkViewModel.PromotionDrinkToppings?.Sum(t => t.Quantity * toppingDictionary?[t.ToppingId].Price) ?? 0);
            promotionDrinkViewModel.DrinkSize = drinkSize.Name;
            promotionDrinkViewModel.Amount = (drinkSize.Price + (promotionDrinkViewModel.PromotionDrinkToppings?.Sum(t => t.Quantity * toppingDictionary?[t.ToppingId].Price) ?? 0)) * promotionDrinkViewModel.Quantity;
            
            return View(promotionDrinkViewModel);
        }
        
    }
}
