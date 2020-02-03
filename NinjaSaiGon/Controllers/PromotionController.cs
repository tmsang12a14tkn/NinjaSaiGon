using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Admin.Models.PromotionViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class PromotionController : Controller
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IToppingRepository _toppingRepository;

        public PromotionController(IPromotionRepository promotionRepository, IDrinkRepository drinkRepository, IToppingRepository toppingRepository)
        {
            _promotionRepository = promotionRepository;
            _drinkRepository = drinkRepository;
            _toppingRepository = toppingRepository;
        }

        public IActionResult Index()
        {
            return View(_promotionRepository.List);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _promotionRepository.AddPromotion(promotion);
                return RedirectToAction(nameof(Index));
            }
            
            return View(promotion);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = _promotionRepository.GetPromotionById(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }
            
            return View(promotion);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Promotion promotion)
        {
            if (id != promotion.Id)
            {
                return NotFound();
            }
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    //DoSomethingWith(error);
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _promotionRepository.UpdatePromotion(promotion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_promotionRepository.PromotionExists(promotion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(promotion);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = _promotionRepository.GetPromotionById(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }
            
            return View(promotion);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _promotionRepository.DeletePromotion(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditPromotionDrinkSetting(PromotionDrinkSetting setting)
        {
            var success = _promotionRepository.SavePromotionDrinkSetting(setting);
            return Json(new { success });
        }

        [HttpPost]
        public IActionResult GetPromotionDrinkItem(PromotionDrinkItemView promotionDrinkViewModel)
        {
            var drink = _drinkRepository.GetDrinkById(promotionDrinkViewModel.DrinkId);
            var drinkSize = drink.DrinkSizes.FirstOrDefault(s => s.Id == promotionDrinkViewModel.DrinkSizeId) ?? drink.PrimarySize;
            
            if (drinkSize == null)
            {
                throw new ApplicationException("DrinkSize is invalid!");
            }
            
            Dictionary<int, Topping> toppingDictionary = promotionDrinkViewModel.PromotionDrinkToppings?.Select(t => new KeyValuePair<int, Topping>(t.ToppingId, _toppingRepository.GetToppingById(t.ToppingId))).ToDictionary(k => k.Key, k => k.Value);

            promotionDrinkViewModel.CategoryId = drink.CategoryId;
            promotionDrinkViewModel.DrinkName = drink.Name;
            promotionDrinkViewModel.Price = drinkSize.Price;
            promotionDrinkViewModel.PromotionDrinkToppings?.ForEach(t =>
            {
                t.ToppingName = toppingDictionary?[t.ToppingId]?.Name;
                t.ToppingPrice = toppingDictionary?[t.ToppingId]?.Price;
            });
            promotionDrinkViewModel.FullPrice = drinkSize.Price + (promotionDrinkViewModel.PromotionDrinkToppings?.Sum(t => t.Quantity * t.ToppingPrice) ?? 0);
            promotionDrinkViewModel.DrinkSize = drinkSize.Name;
            promotionDrinkViewModel.Amount = promotionDrinkViewModel.FullPrice * promotionDrinkViewModel.Quantity;
           
            return ViewComponent("PromotionDrinkNewItem", new { promotionDrinkViewModel } );
        }
    }
}