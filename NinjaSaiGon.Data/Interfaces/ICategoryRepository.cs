using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<DrinkCategory> Categories { get; }
        IEnumerable<DrinkCategoryType> CategorieTypes { get; }
        DrinkCategory GetDrinkCategoryById(int catId);
        void AddDrinkCategory(DrinkCategory drinkCategory);
        void UpdateDrinkCategory(DrinkCategory drinkCategory);
        void DeleteDrinkCategory(int catId);
        bool DrinkCategoryExists(int catId);

        IEnumerable<ToppingCategory> ToppingCategories { get; }
        ToppingCategory GetToppingCategoryById(int tId);
        void AddToppingCategory(ToppingCategory toppingCategory);
        void UpdateToppingCategory(ToppingCategory toppingCategory);
        void DeleteToppingCategory(int tId);
        bool ToppingCategoryExists(int tId);
    }
}
