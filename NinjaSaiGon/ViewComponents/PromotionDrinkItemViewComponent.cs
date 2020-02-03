using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Admin.Models.PromotionViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PromotionDrinkItemViewComponent: ViewComponent
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IToppingRepository _toppingRepository;

        public PromotionDrinkItemViewComponent(IPromotionRepository promotionRepository, IDrinkRepository drinkRepository, IToppingRepository toppingRepository)
        {
            _promotionRepository = promotionRepository;
            _drinkRepository = drinkRepository;
            _toppingRepository = toppingRepository;
        }

        public IViewComponentResult Invoke(int promotionDrinkId, int index)
        {
            var promotionDrink = _promotionRepository.GetPromotionDrinkById(promotionDrinkId);
            var drinkSize = _drinkRepository.GetSizeByDrinkId(promotionDrink.DrinkId);
            Dictionary<int, Topping> toppingDictionary = promotionDrink.PromotionDrinkToppings?.Select(t => new KeyValuePair<int, Topping>(t.ToppingId, _toppingRepository.GetToppingById(t.ToppingId))).ToDictionary(k => k.Key, k => k.Value);
            var promotionDrinkViewModel = new PromotionDrinkItemView
            {
                DrinkSize = promotionDrink.DrinkSizeName,
                DrinkSizeId = promotionDrink.DrinkSizeId,
                DrinkName = promotionDrink.Drink.Name,
                Index = index,
                Id = promotionDrink.Id,
                PromotionId = promotionDrink.PromotionId,
                DrinkId = promotionDrink.DrinkId,
                Quantity = promotionDrink.Quantity,
                CategoryId = promotionDrink.Drink.CategoryId,
                Price = drinkSize.Price,
                PromotionDrinkToppings = promotionDrink.PromotionDrinkToppings?.Select(t=> new PromotionDrinkToppingView {
                    Id = t.Id,
                    PromotionDrinkId = t.PromotionDrinkId,
                    Quantity = t.Quantity,
                    ToppingId = t.ToppingId
                }).ToList()
            };
            promotionDrinkViewModel.PromotionDrinkToppings?.ForEach(t =>
            {
                t.ToppingName = toppingDictionary?[t.ToppingId]?.Name;
                t.ToppingPrice = toppingDictionary?[t.ToppingId]?.Price;
            });
            promotionDrinkViewModel.FullPrice = drinkSize.Price + (promotionDrinkViewModel.PromotionDrinkToppings?.Sum(t => t.Quantity * t.ToppingPrice) ?? 0);
            promotionDrinkViewModel.Amount = promotionDrinkViewModel.FullPrice * promotionDrinkViewModel.Quantity;

            return View(promotionDrinkViewModel);
        }
    }
}
