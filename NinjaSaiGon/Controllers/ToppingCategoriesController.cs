using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ToppingCategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public ToppingCategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: ToppingCategories
        public IActionResult Index()
        {
            return View(_categoryRepository.ToppingCategories);
        }

        public IActionResult Sort()
        {
            return View(_categoryRepository.ToppingCategories);
        }

        [HttpPost]
        public IActionResult Sort(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var toppingCat = _categoryRepository.GetToppingCategoryById(ids[i]);
                toppingCat.Position = i;
                _categoryRepository.UpdateToppingCategory(toppingCat);
            }
            return RedirectToAction(nameof(Sort));
        }

        // GET: ToppingCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingCategory = _categoryRepository.GetToppingCategoryById(id.Value);
            if (toppingCategory == null)
            {
                return NotFound();
            }

            return View(toppingCategory);
        }

        // GET: ToppingCategories/Create
        public IActionResult Create()
        {
            ViewBag.Parents = _categoryRepository.ToppingCategories;
            return View();
        }

        // POST: ToppingCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,EnglishName,Price,Description,IsActive,ParentId")] ToppingCategory toppingCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddToppingCategory(toppingCategory);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Parents = _categoryRepository.ToppingCategories;
            return View(toppingCategory);
        }

        // GET: ToppingCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingCategory = _categoryRepository.GetToppingCategoryById(id.Value);
            if (toppingCategory == null)
            {
                return NotFound();
            }
            ViewBag.Parents = _categoryRepository.ToppingCategories.Where(c=>c.Id != id && c.ParentId != id);
            return View(toppingCategory);
        }

        // POST: ToppingCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,EnglishName,Price,Description,IsActive,ParentId")] ToppingCategory toppingCategory)
        {
            if (id != toppingCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.UpdateToppingCategory(toppingCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryRepository.ToppingCategoryExists(toppingCategory.Id))
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
            ViewBag.Parents = _categoryRepository.ToppingCategories.Where(c => c.Id != id && c.ParentId != id);
            return View(toppingCategory);
        }

        // GET: ToppingCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toppingCategory = _categoryRepository.GetToppingCategoryById(id.Value);
            if (toppingCategory == null)
            {
                return NotFound();
            }

            return View(toppingCategory);
        }

        // POST: ToppingCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var toppingCategory = _categoryRepository.GetToppingCategoryById(id);
            _categoryRepository.DeleteToppingCategory(toppingCategory.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
