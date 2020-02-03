using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Detection;

namespace NinjaSaiGon.Order.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly INotifyPopupRepository _notifyPopupRepository;
        private readonly IDevice _device;

        public MenuViewComponent(IDrinkRepository categoryRepository, INotifyPopupRepository notifyPopupRepository, IDeviceResolver deviceResolver)
        {
            _drinkRepository = categoryRepository;
            _notifyPopupRepository = notifyPopupRepository;
            _device = deviceResolver.Device;
        }

        public IViewComponentResult Invoke(DrinkCategory cat)
        {
            var isMobileTablet = _device.Type == DeviceType.Mobile || _device.Type == DeviceType.Tablet;
            var isOpen = DateTime.Now.TimeOfDay > TimeSpan.FromHours(8) && DateTime.Now.TimeOfDay < TimeSpan.FromHours(17);
            var drinks = cat.Id == 0 ? _drinkRepository.PreferredDrinks.ToList() : cat.Id == -1 ? _drinkRepository.NewDrinks.ToList(): _drinkRepository.DrinksByCategory(cat.Id).ToList();
            var notifyPopup = _notifyPopupRepository.GetActiveNotifyPopup();

            ViewData["IsOpen"] = isOpen;
            ViewData["NotifyPopup"] = notifyPopup;
            ViewData["isMobileTablet"] = isMobileTablet;
            ViewData["cat"] = cat;

            return View(drinks);
        }
    }
}
