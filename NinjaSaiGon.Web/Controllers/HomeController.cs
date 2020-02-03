using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Web.Models;
using Wangkanai.Detection;

namespace NinjaSaiGon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IToppingRepository _toppingRepository;
        private readonly IDevice _device;

        public HomeController(ICategoryRepository categoryRepository, IToppingRepository toppingRepository, IDeviceResolver deviceResolver)
        {
            _categoryRepository = categoryRepository;
            _toppingRepository = toppingRepository;
            _device = deviceResolver.Device;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                DrinkCategories = _categoryRepository.Categories.ToList(),
                DeviceType = _device.Type,
                Toppings = _toppingRepository.Toppings.Where(t => t.IsActive).ToList()
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
