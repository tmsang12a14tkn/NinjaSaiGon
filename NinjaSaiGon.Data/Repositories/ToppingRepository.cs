using NinjaSaiGon.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;

namespace NinjaSaiGon.Data.Repositories
{
    public class ToppingRepository : IToppingRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public ToppingRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Topping> Toppings => _appDbContext.Toppings.Include(t => t.Category).Include(t => t.Photos);
        public IEnumerable<DrinkUnit> Units => _appDbContext.DrinkUnits.ToList();
        public IEnumerable<Topping> PreferredToppings => _appDbContext.Toppings.Include(c => c.Category);

        public Topping GetToppingById(int toppingId) => _appDbContext.Toppings.Include(d => d.Photos).FirstOrDefault(p => p.Id == toppingId);

        public List<GroupedTopping> GetToppingByDrinkId(int drinkId)
        {
            var groupedToppings = from t in _appDbContext.Toppings
                join dt in _appDbContext.DrinkToppings on t.Id equals dt.ToppingId
                join tc in _appDbContext.ToppingCategories on t.CategoryId equals tc.Id
                where dt.DrinkId == drinkId
                group t by tc
                into g
                select new GroupedTopping
                {
                    ToppingCategoryId = g.Key.Id,
                    Name = g.Key.Name,
                    Toppings = g.ToList()
                };
                var result = from groupedTopping in groupedToppings
                join dtc in _appDbContext.DrinkToppingCategories on groupedTopping.ToppingCategoryId equals dtc.ToppingCategoryId
                where dtc.DrinkId == drinkId
                select groupedTopping.GetMinMax(dtc);
            
            return result.ToList();
        }

        public void AddTopping(Topping topping)
        {
            if (topping.Photos != null)
            {
                topping.Photos = topping.Photos.Where(p => !p.IsDeleted).ToList();
            }
            _appDbContext.Toppings.Add(topping);
            _appDbContext.SaveChanges();
        }

        public void UpdateTopping(Topping topping)
        {
            if (topping.Photos != null)
            {
                foreach (var photoModel in topping.Photos)
                {
                    if (photoModel.Id != 0)
                    {
                        var photo = _appDbContext.ToppingPhotos.Find(photoModel.Id);
                        _appDbContext.Entry(photo).State = EntityState.Detached;
                        //deleted
                        if (photoModel.IsDeleted)
                            _appDbContext.ToppingPhotos.Remove(photo);
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
                            photoModel.ToppingId = topping.Id;
                            _appDbContext.ToppingPhotos.Add(photoModel);
                        }
                    }
                }
            }

            _appDbContext.Entry(topping).State = EntityState.Modified;
            _appDbContext.Update(topping);
            _appDbContext.SaveChanges();
        }

        public void UpdateStockStatus(UpdateStockStatusModel model)
        {
            var oTopping = this.GetToppingById(model.Id);
            oTopping.OutOfStock = model.OutOfStock;
            oTopping.OutOfStockFrom = model.OutOfStockFrom;
            oTopping.OutOfStockTo = model.OutOfStockTo;

            _appDbContext.Entry(oTopping).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void DeleteTopping(int toppingId)
        {
            var topping = GetToppingById(toppingId);
            _appDbContext.Toppings.Remove(topping);
            _appDbContext.SaveChanges();
        }

        public bool ToppingExists(int toppingId)
        {
            return _appDbContext.Toppings.Any(e => e.Id == toppingId);
        }
    }
}
