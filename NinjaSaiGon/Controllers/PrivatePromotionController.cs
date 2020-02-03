using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Models.PromotionViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class PrivatePromotionController : Controller
    {
        private readonly IPrivatePromotionRepository _privatePromotionRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IToppingRepository _toppingRepository;

        private const int CodeLength = 8;
        private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());

        public PrivatePromotionController(IPrivatePromotionRepository privatePromotionRepository, IDrinkRepository drinkRepository, IToppingRepository toppingRepository)
        {
            _privatePromotionRepository = privatePromotionRepository;
            _drinkRepository = drinkRepository;
            _toppingRepository = toppingRepository;
        }

        public IActionResult Index()
        {
            return View(_privatePromotionRepository.List);
        }

        public IActionResult ManageCode(string code, int ppId = 0, int status = 0, DateTime? startDate = null, DateTime? endDate = null)
        {
            var codes = _privatePromotionRepository.AllCode;
            if (ppId != 0)
                codes = codes.Where(c => c.PrivatePromotionId == ppId);

            if (!string.IsNullOrEmpty(code))
                codes = codes.Where(c => c.Code.Contains(code));

            if (status == 1)
                codes = codes.Where(c => c.Status);
            else if (status == 2)
                codes = codes.Where(c => !c.Status);

            if (startDate.HasValue)
                codes = codes.Where(c => c.StartDate.Value.Date >= startDate.Value.Date && c.IsInfinityTime);

            if (endDate.HasValue)
                codes = codes.Where(c => c.EndDate.Value.Date <= endDate.Value.Date && c.IsInfinityTime);

            ViewBag.PrivatePromotions = _privatePromotionRepository.List.ToList();
            return View(codes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrivatePromotion privatePromotion)
        {
            if (ModelState.IsValid)
            {
                _privatePromotionRepository.AddPrivatePromotion(privatePromotion);
                return RedirectToAction(nameof(Index));
            }

            return View(privatePromotion);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privatePromotion = _privatePromotionRepository.GetPrivatePromotionById(id.Value);
            if (privatePromotion == null)
            {
                return NotFound();
            }

            return View(privatePromotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PrivatePromotion privatePromotion)
        {
            if (id != privatePromotion.Id)
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
                    _privatePromotionRepository.UpdatePrivatePromotion(privatePromotion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_privatePromotionRepository.PrivatePromotionExists(privatePromotion.Id))
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

            return View(privatePromotion);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privatePromotion = _privatePromotionRepository.GetPrivatePromotionById(id.Value);
            if (privatePromotion == null)
            {
                return NotFound();
            }

            return View(privatePromotion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _privatePromotionRepository.DeletePrivatePromotion(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult EditPrivatePromotionDrinkSetting(PrivatePromotionDrinkSetting setting)
        {
            var success = _privatePromotionRepository.SavePrivatePromotionDrinkSetting(setting);
            return Json(new { success });
        }

        [HttpPost]
        public IActionResult GetPrivatePromotionDrinkItem(PromotionDrinkItemView promotionDrinkViewModel)
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

            return ViewComponent("PrivatePromotionDrinkNewItem", new { promotionDrinkViewModel });
        }

        public IActionResult FilterCode(int ppId, string code, int status, DateTime? startDate = null, DateTime? endDate = null)
        {
            var codes = _privatePromotionRepository.AllCode.Where(c => c.PrivatePromotionId == ppId);
            if (!string.IsNullOrEmpty(code))
                codes = codes.Where(c => c.Code.Contains(code));

            if (status == 1)
                codes = codes.Where(c => c.Status);
            else if (status == 2)
                codes = codes.Where(c => !c.Status);

            if (startDate.HasValue)
                codes = codes.Where(c => c.StartDate.Value.Date >= startDate.Value.Date && c.IsInfinityTime);

            if (endDate.HasValue)
                codes = codes.Where(c => c.EndDate.Value.Date <= endDate.Value.Date && c.IsInfinityTime);

            return PartialView("_ListCode", codes.ToList());
        }

        public IActionResult EditCode(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = _privatePromotionRepository.GetPrivatePromotionCodeById(id.Value);
            if (code == null)
            {
                return NotFound();
            }

            return PartialView("_EditCode", code);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCode(int id, PrivatePromotionCode privatePromotionCode)
        {
            try
            {
                _privatePromotionRepository.UpdateCode(privatePromotionCode);
                var code = _privatePromotionRepository.GetPrivatePromotionCodeById(privatePromotionCode.Id);
                return Json(new {
                    success = true,
                    codeId = code.Id,
                    codeComment = code.CodeComment,
                    codeStatus = code.Status,
                    codeUse = code.IsInfinityUse ? $"{code.CurrentUseCode} / Không giới hạn" : $"{code.CurrentUseCode} / {code.MaxUseCode}",
                    codeTime = code.IsInfinityTime ? "Không giới hạn" : code.StartDate?.ToString("dd/MM/yyyy") + " - " + code.EndDate?.ToString("dd/MM/yyyy")
                });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult UpdateCodeStatus(int id)
        {
            try
            {
                var code = _privatePromotionRepository.GetPrivatePromotionCodeById(id);
                code.Status = !code.Status;
                _privatePromotionRepository.UpdateCode(code);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult DeleteCode(int codeId)
        {
            var code = _privatePromotionRepository.GetPrivatePromotionCodeById(codeId);
            if (code != null && code.CurrentUseCode == 0)
            {
                try
                {
                    _privatePromotionRepository.DeleteCode(codeId);
                    return Json(new { success = true, codeId = code.Id });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
            else
                return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult GenerateCode(CreatePrivatePromotionCode model)
        {
            var success = false;
            var newCodes = Enumerable.Range(0, model.NumOfCode).Select(_ => GenerateCode(CodeLength, model.CodePrefix, model.CodeSuffix)).ToList();
            var existingCodes = _privatePromotionRepository.AllCode.Select(v => v.Code).Where(v => newCodes.Contains(v)).ToList();

            if (existingCodes.Count == 0)
            {
                try
                {
                    foreach (var code in newCodes)
                    {
                        var ppCode = new PrivatePromotionCode
                        {
                            Code = code,
                            CodeComment = model.CodeComment,
                            CreatedDate = DateTime.Now,
                            MaxUseCode = model.MaxUseCode,
                            IsInfinityUse = model.IsInfinityUse,
                            StartDate = model.StartDate,
                            EndDate = model.EndDate,
                            IsInfinityTime = model.IsInfinityTime,
                            PrivatePromotionId = model.PrivatePromotionId,
                            Status = true
                        };
                        _privatePromotionRepository.AddPrivatePromotionCode(ppCode);
                    }
                    success = true;
                }
                catch
                { }
            }
            
            return Json(new { success });
        }

        private static string GenerateCode(int numberOfCharsToGenerate, string prefix, string suffix)
        {
            var sb = new StringBuilder();
            char[] chars = "ACDEFGHJKLMNPQRTUVWXYZ234679".ToCharArray();

            if (!string.IsNullOrEmpty(prefix))
            {
                numberOfCharsToGenerate -= prefix.Length;
                sb.Append(prefix);
            }

            if (!string.IsNullOrEmpty(suffix))
                numberOfCharsToGenerate -= suffix.Length;

            for (int i = 0; i < numberOfCharsToGenerate; i++)
            {
                int num = _random.Value.Next(0, chars.Length);
                sb.Append(chars[num]);
            }

            if (!string.IsNullOrEmpty(suffix))
                sb.Append(suffix);

            return sb.ToString();
        }
    }
}