using NinjaSaiGon.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Models;
using System.Threading.Tasks;
using NinjaSaiGon.Data.ViewModels;

namespace NinjaSaiGon.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public DrinkRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Drink> Drinks => _appDbContext.Drinks.Include(c => c.Category).Include(c => c.DrinkSizes).Include(c => c.Photos);

        public IEnumerable<Drink> DrinksByCategory(int catId) => _appDbContext.Drinks.Where(d=>d.CategoryId == catId && d.IsActive).Include(c => c.Category).Include(c => c.DrinkSizes).Include(c => c.Photos).OrderBy(d => d.Position);

        public IEnumerable<Drink> PreferredDrinks => _appDbContext.Drinks.Where(d=>d.IsSuggested && d.IsActive).Include(c => c.Category).Include(c => c.DrinkSizes).Include(c => c.Photos);

        public IEnumerable<Drink> NewDrinks => _appDbContext.Drinks.Where(d => d.IsNew && d.IsActive).Include(c => c.Category).Include(c => c.DrinkSizes).Include(c => c.Photos).OrderBy(o => o.NewOrderSort);

        public IEnumerable<DrinkUnit> DrinkUnits => _appDbContext.DrinkUnits;

        public Drink GetDrinkById(int drinkId) => _appDbContext.Drinks.Include(d => d.Photos)
                                                                    .Include(d => d.DrinkToppings)
                                                                    .Include(d => d.Category)
                                                                    .Include(d => d.DrinkSizes)
                                                                    .Include(d => d.IceOptions)
                                                                    .Include(d => d.SugarOptions)
                                                                    .Include(d => d.DrinkToppingCategories)
                                                                    .AsNoTracking()
                                                                    .FirstOrDefault(p => p.Id == drinkId);

        public DrinkSize GetSizeByDrinkId(int drinkId) => _appDbContext.DrinkSizes.FirstOrDefault(p => p.DrinkId == drinkId);

        public DrinkSize GetSizeById(int id) => _appDbContext.DrinkSizes.FirstOrDefault(p => p.Id == id);

        public IceOption GetIceByDrinkId(int drinkId) => _appDbContext.IceOptions.FirstOrDefault(p => p.DrinkId == drinkId);

        public IceOption GetIceById(int id) => _appDbContext.IceOptions.FirstOrDefault(p => p.Id == id);

        public SugarOption GetSugarByDrinkId(int drinkId) => _appDbContext.SugarOptions.FirstOrDefault(p => p.DrinkId == drinkId);

        public SugarOption GetSugarById(int id) => _appDbContext.SugarOptions.FirstOrDefault(p => p.Id == id);

        public void AddDrink(Drink drink)
        {
            if (drink.Photos != null)
            {
                drink.Photos = drink.Photos.Where(p => !p.IsDeleted).ToList();
            }
            drink.DrinkSizes = new List<DrinkSize> { new DrinkSize { IsActive = true, IsPrimary = true } };
            _appDbContext.Drinks.Add(drink);
            _appDbContext.SaveChanges();
        }

        public void UpdateDrink(Drink drink)
        {
            if (drink.Photos != null)
            {
                foreach (var photoModel in drink.Photos)
                {
                    if (photoModel.Id != 0)
                    {
                        if (photoModel.IsDeleted)
                            _appDbContext.Entry(photoModel).State = EntityState.Deleted;
                        else
                            _appDbContext.Entry(photoModel).State = EntityState.Modified;
                    }
                    else
                    {
                        if (!photoModel.IsDeleted)
                        {
                            photoModel.DrinkId = drink.Id;
                            _appDbContext.DrinkPhotos.Add(photoModel);
                        }
                    }
                }
            }

            _appDbContext.Entry(drink).State = EntityState.Modified;
            _appDbContext.Update(drink);
            _appDbContext.SaveChanges();
        }

        public void UpdateStockStatus(UpdateStockStatusModel model)
        {
            var oDrink = this.GetDrinkById(model.Id);
            oDrink.OutOfStock = model.OutOfStock;
            oDrink.OutOfStockFrom = model.OutOfStockFrom;
            oDrink.OutOfStockTo = model.OutOfStockTo;

            _appDbContext.Entry(oDrink).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void UpdateDrinkRequire(Drink drink)
        {
            var oDrink = this.GetDrinkById(drink.Id);
            oDrink.RequireSizeOption = drink.RequireSizeOption;
            oDrink.RequireIceOption = drink.RequireIceOption;
            oDrink.RequireSugarOption = drink.RequireSugarOption;
            oDrink.DisplaySizeOption = drink.DisplaySizeOption;
            oDrink.DisplayIceOption = drink.DisplayIceOption;
            oDrink.DisplaySugarOption = drink.DisplaySugarOption;

            _appDbContext.Entry(oDrink).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void UpdateDrinkTopping(DrinkTopping drinkTopping)
        {
            var oDrinkTopping = _appDbContext.DrinkToppings.FirstOrDefault(dt => dt.DrinkId == drinkTopping.DrinkId && dt.ToppingId == drinkTopping.ToppingId);
            if (oDrinkTopping == null)
            {
                _appDbContext.Add(drinkTopping);
            }
            else
            {
                oDrinkTopping.IsPrimary = drinkTopping.IsPrimary;
                oDrinkTopping.PriceForExtra = drinkTopping.PriceForExtra;
                oDrinkTopping.PriceForSale = drinkTopping.PriceForSale;
            };
            _appDbContext.SaveChanges();

        }

        public void DeleteDrinkTopping(int drinkId, int toppingId)
        {
            var eDrinkTopping = _appDbContext.DrinkToppings.Find(drinkId, toppingId);
            if (eDrinkTopping != null)
            {
                _appDbContext.Remove(eDrinkTopping);
                _appDbContext.SaveChanges();
            };

        }

        public void UpdateDrinkSize(DrinkSize drinkSize)
        {
            var oDrinkSize = _appDbContext.DrinkSizes.FirstOrDefault(ds => ds.Id == drinkSize.Id);
            if (oDrinkSize == null)
            {
                _appDbContext.DrinkSizes.Add(drinkSize);
            }
            else
            {
                _appDbContext.Entry(oDrinkSize).State = EntityState.Detached;
                _appDbContext.Entry(drinkSize).State = EntityState.Modified;
                _appDbContext.Update(drinkSize);
            };

            if (drinkSize.IsPrimary)
            {
                var sizes = _appDbContext.DrinkSizes.Where(s => s.Id != drinkSize.Id && s.DrinkId == drinkSize.DrinkId);
                foreach (var s in sizes)
                {
                    s.IsPrimary = false;
                }
            }

            _appDbContext.SaveChanges();
        }

        public void UpdatePrimarySize(int id, int drinkId)
        {
            var oDrinkSize = _appDbContext.DrinkSizes.FirstOrDefault(ds => ds.Id == id);
            var sizes = _appDbContext.DrinkSizes.Where(s => s.Id != oDrinkSize.Id && s.DrinkId == drinkId);
            foreach (var s in sizes)
            {
                s.IsPrimary = false;
            }

            _appDbContext.Entry(oDrinkSize).State = EntityState.Modified;
            oDrinkSize.IsPrimary = true;

            _appDbContext.SaveChanges();
        }

        public void UpdateIceOption(IceOption iceOption)
        {
            var oIceOption = _appDbContext.IceOptions.FirstOrDefault(io => io.Id == iceOption.Id);
            if (oIceOption == null)
            {
                _appDbContext.IceOptions.Add(iceOption);
            }
            else
            {
                _appDbContext.Entry(oIceOption).State = EntityState.Detached;
                _appDbContext.Entry(iceOption).State = EntityState.Modified;
                _appDbContext.Update(iceOption);
            };

            if (iceOption.IsPrimary)
            {
                var ices = _appDbContext.IceOptions.Where(s => s.Id != iceOption.Id && s.DrinkId == iceOption.DrinkId);
                foreach (var ice in ices)
                {
                    ice.IsPrimary = false;
                }
            }

            _appDbContext.SaveChanges();
        }

        public void UpdatePrimaryIce(int id, int drinkId)
        {
            var oIceOption = _appDbContext.IceOptions.FirstOrDefault(ds => ds.Id == id);
            var ices = _appDbContext.IceOptions.Where(s => s.Id != oIceOption.Id && s.DrinkId == drinkId);
            foreach (var i in ices)
            {
                i.IsPrimary = false;
            }

            _appDbContext.Entry(oIceOption).State = EntityState.Modified;
            oIceOption.IsPrimary = true;

            _appDbContext.SaveChanges();
        }

        public void UpdateSugarOption(SugarOption sugarOption)
        {
            var oSugarOption = _appDbContext.SugarOptions.FirstOrDefault(so => so.Id == sugarOption.Id);
            if (oSugarOption == null)
            {
                _appDbContext.SugarOptions.Add(sugarOption);
            }
            else
            {
                _appDbContext.Entry(oSugarOption).State = EntityState.Detached;
                _appDbContext.Entry(sugarOption).State = EntityState.Modified;
                _appDbContext.Update(sugarOption);
            };

            if (sugarOption.IsPrimary)
            {
                var sugars = _appDbContext.SugarOptions.Where(s => s.Id != sugarOption.Id && s.DrinkId == sugarOption.DrinkId);
                foreach (var sugar in sugars)
                {
                    sugar.IsPrimary = false;
                }
            }

            _appDbContext.SaveChanges();
        }

        public void UpdatePrimarySugar(int id, int drinkId)
        {
            var oSugarOption = _appDbContext.SugarOptions.FirstOrDefault(ds => ds.Id == id);
            var sugars = _appDbContext.SugarOptions.Where(s => s.Id != oSugarOption.Id && s.DrinkId == drinkId);
            foreach (var s in sugars)
            {
                s.IsPrimary = false;
            }

            if (oSugarOption != null)
            {
                _appDbContext.Entry(oSugarOption).State = EntityState.Modified;
                oSugarOption.IsPrimary = true;
            }

            _appDbContext.SaveChanges();
        }

        public void DeleteDrink(int drinkId)
        {
            var drink = this.GetDrinkById(drinkId);
            _appDbContext.Drinks.Remove(drink);
            _appDbContext.SaveChanges();
        }

        public void DeleteDrinkSize(int id)
        {
            var size = this.GetSizeById(id);
            _appDbContext.DrinkSizes.Remove(size);
            _appDbContext.SaveChanges();
        }

        public void DeleteIceOption(int id)
        {
            var ice = this.GetIceById(id);
            _appDbContext.IceOptions.Remove(ice);
            _appDbContext.SaveChanges();
        }

        public void DeleteSugarOption(int id)
        {
            var sugar = this.GetSugarById(id);
            _appDbContext.SugarOptions.Remove(sugar);
            _appDbContext.SaveChanges();
        }

        public bool DrinkExists(int drinkId)
        {
            return _appDbContext.Drinks.Any(e => e.Id == drinkId);
        }

        public void UpdateDrinkToppingCategory(DrinkToppingCategory drinkToppingCategory)
        {
            var oDrinkToppingCategory = _appDbContext.DrinkToppingCategories.FirstOrDefault(dtc => dtc.DrinkId == drinkToppingCategory.DrinkId && dtc.ToppingCategoryId == drinkToppingCategory.ToppingCategoryId);
            if (oDrinkToppingCategory == null)
            {
                _appDbContext.Add(drinkToppingCategory);
            }
            else
            {
                oDrinkToppingCategory.Max = drinkToppingCategory.Max;
                oDrinkToppingCategory.Min = drinkToppingCategory.Min;
            };
            _appDbContext.SaveChanges();
        }
    }
}
