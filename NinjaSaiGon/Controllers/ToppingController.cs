using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;
using NinjaSaiGon.Data.ViewModels;
using NinjaSaiGon.Admin.Models.ToppingModels;
using Microsoft.AspNetCore.Authorization;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ToppingController : Controller
    {
        private readonly IToppingRepository _toppingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ToppingController(IToppingRepository toppingRepository, ICategoryRepository categoryRepository)
        {
            _toppingRepository = toppingRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Topping
        public IActionResult Index()
        {
            return View(_toppingRepository.Toppings);
        }

        // GET: Topping/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = _toppingRepository.GetToppingById(id.Value);
            if (topping == null)
            {
                return NotFound();
            }

            return View(topping);
        }

        // GET: Topping/Create
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.ToppingCategories;
            ViewBag.Units = _toppingRepository.Units;
            return View();
        }

        // POST: Topping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Topping topping)
        {
            if (ModelState.IsValid)
            {
                _toppingRepository.AddTopping(topping);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ToppingCategories;
            ViewBag.Units = _toppingRepository.Units;
            return View(topping);
        }

        // GET: Topping/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = _toppingRepository.GetToppingById(id.Value);
            if (topping == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryRepository.ToppingCategories;
            ViewBag.Units = _toppingRepository.Units;
            return View(topping);
        }

        // POST: Topping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Topping topping)
        {
            if (id != topping.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _toppingRepository.UpdateTopping(topping);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_toppingRepository.ToppingExists(topping.Id))
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
            ViewBag.Categories = _categoryRepository.ToppingCategories;
            ViewBag.Units = _toppingRepository.Units;
            return View(topping);
        }

        [HttpPost]
        public IActionResult UpdateStockStatus(UpdateStockStatusModel model)
        {
            try
            {
                _toppingRepository.UpdateStockStatus(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }
            
            return Json(new { success = true, id = model.Id, outofstock = model.OutOfStock });
        }

        // GET: Topping/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topping = _toppingRepository.GetToppingById(id.Value);
            if (topping == null)
            {
                return NotFound();
            }
            ViewBag.CategoryName = _categoryRepository.GetToppingCategoryById(topping.CategoryId).Name;
            return View(topping);
        }

        // POST: Topping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var topping = _toppingRepository.GetToppingById(id);
            _toppingRepository.DeleteTopping(topping.Id);
            return RedirectToAction(nameof(Index));
        }

        public IEnumerable<ToppingDto> QuickCreateList()
        {
            return _toppingRepository.Toppings.Where(t => t.QuickCreateIndex > 0).OrderBy(t => t.QuickCreateIndex).Select(t => new ToppingDto
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                Price = t.Price
            });
        }
    }
}
