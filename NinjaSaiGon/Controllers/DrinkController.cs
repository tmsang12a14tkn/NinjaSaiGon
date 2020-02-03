using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Extensions;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NinjaSaiGon.Data.ViewModels;
using System.Collections.Generic;
using NinjaSaiGon.Admin.Models;
using AutoMapper;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class DrinkController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IToppingRepository _toppingRepository;
        private readonly IMapper _mapper;

        public DrinkController(IMapper mapper, IDrinkRepository drinkRepository, ICategoryRepository categoryRepository, IToppingRepository toppingRepository)
        {
            _mapper = mapper;
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
            _toppingRepository = toppingRepository;
        }

        // GET: Drink
        public IActionResult Index()
        {
            return View(_drinkRepository.Drinks.ToList());
        }
        
        [HttpPost]
        public IActionResult Sort(int[] ids, int catId, string activetab = "")
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var drink = _drinkRepository.GetDrinkById(ids[i]);
                if (catId != -1)
                    drink.Position = i;
                else
                    drink.NewOrderSort = i;
                _drinkRepository.UpdateDrink(drink);
            }

            return new RedirectResult(Url.Action("Edit", "DrinkCategories", new { id = catId }) + activetab);
        }

        // GET: Drink/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.Categories;
            return View();
        }

        // POST: Drink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Drink drink)
        {
            if (ModelState.IsValid)
            {
                _drinkRepository.AddDrink(drink);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.Categories;
            return View(drink);
        }

        public IActionResult SortOptions(int drinkId)
        {
            var drink = _drinkRepository.GetDrinkById(drinkId);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        [HttpPost]
        public IActionResult SortOptions(int id, DrinkOrderOptionsViewModel options)
        {
            foreach (var sizeModel in options.Sizes)
            {
                var sizeOption = _drinkRepository.GetSizeById(sizeModel.Id);
                _mapper.Map(sizeModel, sizeOption);
                _drinkRepository.UpdateDrinkSize(sizeOption);
            }

            foreach (var iceModel in options.Ices)
            {
                var iceOption = _drinkRepository.GetIceById(iceModel.Id);
                _mapper.Map(iceModel, iceOption);
                _drinkRepository.UpdateIceOption(iceOption);
            }

            foreach (var sugarModel in options.Sugars)
            {
                var sugarOption = _drinkRepository.GetSugarById(sugarModel.Id);
                _mapper.Map(sugarModel, sugarOption);
                _drinkRepository.UpdateSugarOption(sugarOption);
            }
            return RedirectToAction("SortOptions", new { drinkId = id });
        }

        // GET: Drink/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = _drinkRepository.GetDrinkById(id.Value);
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryRepository.Categories;
            ViewBag.AllDrinkToppings = _toppingRepository.Toppings.ToList();
            return View(drink);
        }

        // POST: Drink/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditDrinkModel drinkModel, string activetab = "")
        {
            if (id != drinkModel.Id)
            {
                return NotFound();
            }

            var drink = _drinkRepository.GetDrinkById(drinkModel.Id);
            if (drink == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map(drinkModel, drink);
                    //foreach(var photoModel in drinkModel.Photos)
                    //{
                    //    if(photoModel.IsDeleted && photoModel.Id != 0)
                    //    {
                    //        drink.Photos.Remove(drink.Photos.FirstOrDefault(p=>p.Id == photoModel.Id));
                    //    }
                    //    else if(photoModel.Id == 0 && !photoModel.IsDeleted)
                    //    {
                    //        drink.Photos.Add(_mapper.Map<DrinkPhoto>(photoModel));
                    //    }
                    //}
                    _drinkRepository.UpdateDrink(drink);
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.Categories = _categoryRepository.Categories;
                    ViewBag.AllDrinkToppings = _toppingRepository.Toppings.ToList();
                    return View(drink);
                }
                
                return new RedirectResult(Url.Action("Edit", new { id }) + activetab);
            }
            ViewBag.Categories = _categoryRepository.Categories;
            ViewBag.AllDrinkToppings = _toppingRepository.Toppings.ToList();
            return View(drink);
        }

        [HttpPost]
        public IActionResult EditMisc(Drink drink, string activetab = "")
        {
            try
            {
                _drinkRepository.UpdateDrinkRequire(drink);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_drinkRepository.DrinkExists(drink.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult EditToppings(EditDrinkToppingsModel model, string activetab = "")
        {  
            foreach (var topping in model.Toppings)
            {
                if (topping.Selected)
                {
                    var drinkTopping = new DrinkTopping
                    {
                        DrinkId = model.DrinkId,
                        ToppingId = topping.ToppingId,
                        IsPrimary = topping.IsPrimary,
                        PriceForExtra = topping.PriceForExtra,
                        PriceForSale = topping.PriceForSale
                    };
                    _drinkRepository.UpdateDrinkTopping(drinkTopping);
                }
                else
                {
                    _drinkRepository.DeleteDrinkTopping(model.DrinkId, topping.ToppingId);
                }
            }
            foreach(var toppingCategory in model.ToppingCategories)
            {
                var drinkToppingCategory = new DrinkToppingCategory
                {
                    DrinkId = model.DrinkId,
                    ToppingCategoryId = toppingCategory.ToppingCategoryId,
                    Min = toppingCategory.Min,
                    Max = toppingCategory.Max
                };
                _drinkRepository.UpdateDrinkToppingCategory(drinkToppingCategory);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateOptionsActive(int id, int drinkId, int type)
        {
            try
            {
                if (type == 1)
                {
                    var size = _drinkRepository.GetSizeById(id);
                    size.IsActive = !size.IsActive;
                    _drinkRepository.UpdateDrinkSize(size);
                }
                else if (type == 2)
                {
                    var ice = _drinkRepository.GetIceById(id);
                    ice.IsActive = !ice.IsActive;
                    _drinkRepository.UpdateIceOption(ice);
                }
                else if (type == 3)
                {
                    var sugar = _drinkRepository.GetSugarById(id);
                    sugar.IsActive = !sugar.IsActive;
                    _drinkRepository.UpdateSugarOption(sugar);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateOptionsShowOrder(int id, int drinkId, int type)
        {
            try
            {
                if (type == 1)
                {
                    var size = _drinkRepository.GetSizeById(id);
                    size.IsShowOrder = !size.IsShowOrder;
                    _drinkRepository.UpdateDrinkSize(size);
                }
                else if (type == 2)
                {
                    var ice = _drinkRepository.GetIceById(id);
                    ice.IsShowOrder = !ice.IsShowOrder;
                    _drinkRepository.UpdateIceOption(ice);
                }
                else if (type == 3)
                {
                    var sugar = _drinkRepository.GetSugarById(id);
                    sugar.IsShowOrder = !sugar.IsShowOrder;
                    _drinkRepository.UpdateSugarOption(sugar);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDrinkSize(DrinkSize drinkSize)
        {
            try
            {
                _drinkRepository.UpdateDrinkSize(drinkSize);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(drinkSize.DrinkId);
            var table = await this.RenderViewAsync("_ListDrinkSize", drink.DrinkSizes, true);
            return Json(new { success = true, html = table });
        }
        
        public IActionResult UpdateDrinkSize(int? id, int drinkId)
        {
            var size = new DrinkSize();
            if (id == null)
            {
                size.DrinkId = drinkId;
                size.IsActive = true;
            }
            else
            {
                size = _drinkRepository.GetSizeById(id.Value);
                if (size == null)
                {
                    return NotFound();
                }
            }
            
            ViewBag.Units = _drinkRepository.DrinkUnits;
            return PartialView(size);
        }

        [HttpPost]
        public IActionResult UpdateStockStatus(UpdateStockStatusModel model)
        {
            try
            {
                _drinkRepository.UpdateStockStatus(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }
            
            return Json(new { success = true, id = model.Id, outofstock = model.OutOfStock });
        }

        [HttpPost]
        public IActionResult UpdatePrimarySize(int id, int drinkId)
        {
            try
            {
                _drinkRepository.UpdatePrimarySize(id, drinkId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }
            
            return Json(new { success = true });
        }

        public IActionResult UpdateIceOption(int? id, int drinkId)
        {
            var opt = new IceOption();
            if (id == null)
            {
                opt.DrinkId = drinkId;
                opt.IsActive = true;
            }
            else
            {
                opt = _drinkRepository.GetIceById(id.Value);
                if (opt == null)
                {
                    return NotFound();
                }
            }

            ViewBag.Units = _drinkRepository.DrinkUnits;
            return PartialView(opt);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIceOption(IceOption iceOption)
        {
            try
            {
                _drinkRepository.UpdateIceOption(iceOption);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(iceOption.DrinkId);
            var table = await this.RenderViewAsync("_ListIceOption", drink.IceOptions, true);
            return Json(new { success = true, html = table });
        }

        [HttpPost]
        public IActionResult UpdatePrimaryIce(int id, int drinkId)
        {
            try
            {
                _drinkRepository.UpdatePrimaryIce(id, drinkId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public IActionResult UpdateSugarOption(int? id, int drinkId)
        {
            var opt = new SugarOption();
            if (id == null)
            {
                opt.DrinkId = drinkId;
                opt.IsActive = true;
            }
            else
            {
                opt = _drinkRepository.GetSugarById(id.Value);
                if (opt == null)
                {
                    return NotFound();
                }
            }

            ViewBag.Units = _drinkRepository.DrinkUnits;
            return PartialView(opt);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSugarOption(SugarOption sugarOption)
        {
            try
            {
                _drinkRepository.UpdateSugarOption(sugarOption);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(sugarOption.DrinkId);
            var table = await this.RenderViewAsync("_ListSugarOption", drink.SugarOptions, true);
            return Json(new { success = true, html = table });
        }

        [HttpPost]
        public IActionResult UpdatePrimarySugar(int id, int drinkId)
        {
            try
            {
                _drinkRepository.UpdatePrimarySugar(id, drinkId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        // GET: DrinkCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = _drinkRepository.GetDrinkById(id.Value);
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = _categoryRepository.GetDrinkCategoryById(drink.CategoryId).Name;
            return View(drink);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _drinkRepository.DeleteDrink(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDrinkSize(int id, int drinkId)
        {
            try
            {
                _drinkRepository.DeleteDrinkSize(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(drinkId);
            var table = await this.RenderViewAsync("_ListDrinkSize", drink.DrinkSizes, true);
            return Json(new { success = true, html = table });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIceOption(int id, int drinkId)
        {
            try
            {
                _drinkRepository.DeleteIceOption(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(drinkId);
            var table = await this.RenderViewAsync("_ListIceOption", drink.IceOptions, true);
            return Json(new { success = true, html = table });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSugarOption(int id, int drinkId)
        {
            try
            {
                _drinkRepository.DeleteSugarOption(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            var drink = _drinkRepository.GetDrinkById(drinkId);
            var table = await this.RenderViewAsync("_ListSugarOption", drink.SugarOptions, true);
            return Json(new { success = true, html = table });
        }

        public List<SelectItemView> List()
        {
            return _drinkRepository.Drinks.Select(d => new SelectItemView { id = d.Id, text = d.Name }).ToList();
        }

        public IActionResult Photo(int id)
        {
            var drink = _drinkRepository.GetDrinkById(id);
            if (drink == null) return NotFound();
            else
                return new RedirectResult(drink.PrimaryPhoto.Url);
        }
    }
}
