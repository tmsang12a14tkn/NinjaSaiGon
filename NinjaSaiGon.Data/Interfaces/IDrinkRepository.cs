using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> Drinks { get; }
        IEnumerable<Drink> DrinksByCategory(int catId);
        IEnumerable<Drink> PreferredDrinks { get; }
        IEnumerable<Drink> NewDrinks { get; }
        IEnumerable<DrinkUnit> DrinkUnits { get; }
        Drink GetDrinkById(int drinkId);
        DrinkSize GetSizeByDrinkId(int drinkId);
        DrinkSize GetSizeById(int id);
        IceOption GetIceByDrinkId(int drinkId);
        IceOption GetIceById(int id);
        SugarOption GetSugarByDrinkId(int drinkId);
        SugarOption GetSugarById(int id);
        void AddDrink(Drink drink);
        void UpdateDrink(Drink drink);
        void UpdateStockStatus(UpdateStockStatusModel model);
        void UpdateDrinkRequire(Drink drink);
        void UpdateDrinkTopping(DrinkTopping drinkTopping);
        void DeleteDrinkTopping(int drinkId, int toppingId);
        void UpdateDrinkToppingCategory(DrinkToppingCategory drinkToppingCategory);
        void UpdateDrinkSize(DrinkSize drinkSize);
        void UpdatePrimarySize(int id, int drinkId);
        void UpdateIceOption(IceOption iceOption);
        void UpdatePrimaryIce(int id, int drinkId);
        void UpdateSugarOption(SugarOption sugarOption);
        void UpdatePrimarySugar(int id, int drinkId);
        void DeleteDrink(int drinkId);
        void DeleteDrinkSize(int id);
        void DeleteIceOption(int id);
        void DeleteSugarOption(int id);
        bool DrinkExists(int drinkId);
    }
}
