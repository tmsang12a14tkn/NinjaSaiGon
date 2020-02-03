using NinjaSaiGon.Data.Interfaces;
using System.Collections.Generic;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NinjaSaiGon.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public CategoryRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<DrinkCategory> Categories => _appDbContext.DrinkCategories.Include(dc => dc.Photos).Include(dc => dc.Drinks)
                                                                                    .Include("Drinks.DrinkSizes").Include("Drinks.IceOptions")
                                                                                    .Include("Drinks.SugarOptions").Include("Drinks.Photos")
                                                                                    .OrderBy(c => c.Position);

        public DrinkCategory GetDrinkCategoryById(int catId) => _appDbContext.DrinkCategories.Include(dc => dc.Drinks).Include(dc => dc.Photos).SingleOrDefault(m => m.Id == catId);

        public IEnumerable<DrinkCategoryType> CategorieTypes => _appDbContext.DrinkCategoryTypes;

        public void AddDrinkCategory(DrinkCategory drinkCategory)
        {
            if (drinkCategory.Photos != null)
            {
                drinkCategory.Photos = drinkCategory.Photos.Where(p => !p.IsDeleted).ToList();
            }
            _appDbContext.DrinkCategories.Add(drinkCategory);
            _appDbContext.SaveChanges();
        }

        public void UpdateDrinkCategory(DrinkCategory drinkCategory)
        {
            if (drinkCategory.Photos != null)
            {
                foreach (var photoModel in drinkCategory.Photos)
                {
                    if (photoModel.Id != 0)
                    {
                        var photo = _appDbContext.DrinkCategoryPhotos.Find(photoModel.Id);
                        _appDbContext.Entry(photo).State = EntityState.Detached;
                        //deleted
                        if (photoModel.IsDeleted)
                            _appDbContext.DrinkCategoryPhotos.Remove(photo);
                        else
                        {
                            photo.IsPrimary = photoModel.IsPrimary;
                            _appDbContext.Update(photo);
                        }
                    }
                    else
                    {
                        if (!photoModel.IsDeleted)
                        {
                            photoModel.DrinkCategoryId = drinkCategory.Id;
                            _appDbContext.DrinkCategoryPhotos.Add(photoModel);
                        }
                    }
                }
            }

            _appDbContext.Entry(drinkCategory).State = EntityState.Modified;
            _appDbContext.Update(drinkCategory);
            _appDbContext.SaveChanges();
        }

        public void DeleteDrinkCategory(int catId)
        {
            var drinkCategory = this.GetDrinkCategoryById(catId);
            _appDbContext.DrinkCategories.Remove(drinkCategory);
            _appDbContext.SaveChanges();
        }

        public bool DrinkCategoryExists(int catId) => _appDbContext.DrinkCategories.Any(e => e.Id == catId);


        public IEnumerable<ToppingCategory> ToppingCategories => _appDbContext.ToppingCategories.Include(t => t.Toppings).Include("Toppings.Unit").OrderBy(c => c.Position);

        public ToppingCategory GetToppingCategoryById(int tId) => _appDbContext.ToppingCategories.SingleOrDefault(m => m.Id == tId);

        public void AddToppingCategory(ToppingCategory toppingCategory)
        {
            _appDbContext.ToppingCategories.Add(toppingCategory);
            _appDbContext.SaveChanges();
        }

        public void UpdateToppingCategory(ToppingCategory toppingCategory)
        {
            //_appDbContext.Entry(toppingCategory).Property(c => c.Position).IsModified = false;
            _appDbContext.Update(toppingCategory);
            _appDbContext.SaveChanges();
        }

        public void DeleteToppingCategory(int tId)
        {
            var toppingCategory = this.GetToppingCategoryById(tId);
            _appDbContext.ToppingCategories.Remove(toppingCategory);
            _appDbContext.SaveChanges();
        }

        public bool ToppingCategoryExists(int tId) => _appDbContext.ToppingCategories.Any(e => e.Id == tId);
    }
}
