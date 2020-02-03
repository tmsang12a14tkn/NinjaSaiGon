using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IToppingRepository
    {
        IEnumerable<Topping> Toppings { get; }
        Topping GetToppingById(int toppingId);
        List<GroupedTopping> GetToppingByDrinkId(int drinkId);
        void AddTopping(Topping topping);
        void UpdateTopping(Topping topping);
        void UpdateStockStatus(UpdateStockStatusModel model);
        void DeleteTopping(int toppingId);
        bool ToppingExists(int toppingId);
        IEnumerable<DrinkUnit> Units { get; }
    }
}
