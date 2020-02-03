using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class DrinkCategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDrinkRepository _drinkRepository;

        public DrinkCategoriesController(ICategoryRepository categoryRepository, IDrinkRepository drinkRepository)
        {
            _categoryRepository = categoryRepository;
            _drinkRepository = drinkRepository;
        }

        // GET: DrinkCategories
        public IActionResult Index()
        {
            return View(_categoryRepository.Categories);
        }
        public IActionResult Sort()
        {
            return View(_categoryRepository.Categories);
        }

        [HttpPost]
        public IActionResult Sort(int[] ids)
        {
            for(int i = 0;i< ids.Length;i++)
            {
                var drinkCat = _categoryRepository.GetDrinkCategoryById(ids[i]);
                drinkCat.Position = i;
                _categoryRepository.UpdateDrinkCategory(drinkCat);
            }
            return View(_categoryRepository.Categories);
        }
        // GET: DrinkCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = _categoryRepository.GetDrinkCategoryById(id.Value);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        // GET: DrinkCategories/Create
        public IActionResult Create()
        {
            ViewBag.Parents = _categoryRepository.Categories;
            ViewBag.CatTypes = _categoryRepository.CategorieTypes;
            return View();
        }

        // POST: DrinkCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DrinkCategory drinkCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddDrinkCategory(drinkCategory);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Parents = _categoryRepository.Categories;
            ViewBag.CatTypes = _categoryRepository.CategorieTypes;
            return View(drinkCategory);
        }

        // GET: DrinkCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = new DrinkCategory();
            if (id.Value == -1)
            {
                drinkCategory.Drinks = _drinkRepository.NewDrinks.ToList();
                ViewBag.IsNewList = true;
            }
            else
            {
                drinkCategory = _categoryRepository.GetDrinkCategoryById(id.Value);
                if (drinkCategory == null)
                {
                    return NotFound();
                }
                ViewBag.IsNewList = false;
            }
            ViewBag.Parents = _categoryRepository.Categories.Where(c => c.Id != id && c.ParentId != id);
            ViewBag.CatTypes = _categoryRepository.CategorieTypes;
            return View(drinkCategory);
        }

        // POST: DrinkCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DrinkCategory drinkCategory, string activetab = "")
        {
            if (id != drinkCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.UpdateDrinkCategory(drinkCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoryRepository.DrinkCategoryExists(drinkCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return new RedirectResult(Url.Action("Edit", new { id }) + activetab);
            }
            ViewBag.Parents = _categoryRepository.Categories.Where(c=>c.Id != id && c.ParentId != id);
            ViewBag.CatTypes = _categoryRepository.CategorieTypes;
            return View(drinkCategory);
        }

        // GET: DrinkCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkCategory = _categoryRepository.GetDrinkCategoryById(id.Value);
            if (drinkCategory == null)
            {
                return NotFound();
            }

            return View(drinkCategory);
        }

        // POST: DrinkCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.DeleteDrinkCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
