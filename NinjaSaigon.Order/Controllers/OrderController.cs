using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Order.Models.ShoppingCartViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NinjaSaiGon.Data.Models.Dto;
using NinjaSaiGon.Order.Hubs;
using Microsoft.AspNetCore.SignalR;
using WebPush;
using Microsoft.Extensions.Configuration;

namespace NinjaSaiGon.Order.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly ShoppingCart _shoppingCart;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IToppingRepository _toppingRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IGenericRepository<Device> _deviceRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotifyHub> _hubContext;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderRepository orderRepository, 
            //ShoppingCart shoppingCart, 
            IDrinkRepository drinkRepository, 
            IToppingRepository toppingRepository, 
            IPromotionRepository promotionRepository,
            IGenericRepository<Device> deviceRepository,
            IMapper mapper,
            IHubContext<NotifyHub> hubContext,
            IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            //_shoppingCart = shoppingCart;
            _drinkRepository = drinkRepository;
            _toppingRepository = toppingRepository;
            _promotionRepository = promotionRepository;
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _configuration = configuration;
        }
        
        public IActionResult Checkout()
        {
            return View();
        } 

        public IActionResult DrinkOptions(int index, int drinkId, int sizeId)
        {
            var drink = _drinkRepository.GetDrinkById(drinkId);
            ViewBag.Index = index;
            ViewBag.Toppings = _toppingRepository.GetToppingByDrinkId(drink.Id);
            ViewBag.Size = drink.DrinkSizes.FirstOrDefault(dz => dz.Id == sizeId);
            ViewBag.SizeId = sizeId;
            ViewBag.Quantity = 1;
            ViewBag.SelectedToppings = new List<OrderDetailTopping>();
            ViewBag.IceOptions = drink.IceOptions.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            ViewBag.SugarOptions = drink.SugarOptions.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            ViewBag.DrinkSizes = drink.DrinkSizes.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            return PartialView(drink);
        }
        [HttpPost]
        public IActionResult DrinkOptions(int index, OrderDetail orderDetail)
        {
            var drink = _drinkRepository.GetDrinkById(orderDetail.DrinkId);
            ViewBag.Index = index;
            ViewBag.Toppings = _toppingRepository.GetToppingByDrinkId(drink.Id);
            ViewBag.Size = drink.DrinkSizes.FirstOrDefault(dz => dz.Id == orderDetail.DrinkSizeId);
            ViewBag.SizeId = orderDetail.DrinkSizeId;
            ViewBag.Quantity = orderDetail.Quantity;
            ViewBag.SelectedToppings = (orderDetail.OrderDetailToppings?? new List<OrderDetailTopping>()).ToList();
            ViewBag.IceOptions = drink.IceOptions.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            ViewBag.SugarOptions = drink.SugarOptions.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            ViewBag.DrinkSizes = drink.DrinkSizes.Where(io => io.IsShowOrder).OrderBy(io => io.Order).ToList();
            foreach (var sugarOption in drink.SugarOptions)
            {
                sugarOption.IsPrimary = sugarOption.Name == orderDetail.SugarOption;
            }
            foreach (var iceOption in drink.IceOptions)
            {
                iceOption.IsPrimary = iceOption.Name == orderDetail.IceOption;
            }
            return PartialView(drink);
        }
        //[HttpPost]
        //public IActionResult Checkout(NinjaSaiGon.Data.Models.Order order)
        //{
        //    var items = _shoppingCart.GetShoppingCartItems();
        //    _shoppingCart.ShoppingCartItems = items;
        //    if (_shoppingCart.ShoppingCartItems.Count == 0)
        //    {
        //        ModelState.AddModelError("", "Your card is empty, add some drinks first");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _orderRepository.Create(order);
        //        _shoppingCart.ClearCart();
        //        return RedirectToAction("CheckoutComplete");
        //    }

        //    return View(order);
        //}

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            return View();
        }

        [HttpPost]
        public IActionResult GetCartItem(CartDrinkViewModel cartDrinkViewModel)
        {
            var drink = _drinkRepository.GetDrinkById(cartDrinkViewModel.DrinkId);
            var drinkSize = drink.DrinkSizes.FirstOrDefault(s => s.Id == cartDrinkViewModel.DrinkSizeId) ?? drink.PrimarySize;

            var allDrinkSizes = drink.DrinkSizes.Where(ds=>ds.IsActive).OrderBy(dz => dz.Price);
            var drinkMaxSize = allDrinkSizes.LastOrDefault();
            var drinkMinSize = allDrinkSizes.FirstOrDefault();

            if(drinkSize == null || drinkMaxSize == null || drinkMinSize == null)
            {
                throw new ApplicationException("DrinkSize is invalid!");
            }

            var iceOption = drink.IceOptions.FirstOrDefault(i => i.Id == cartDrinkViewModel.IceOptionId);
            if (drink.RequireIceOption && iceOption == null)
                throw new ApplicationException("IceOption is invalid!");

            var sugarOption = drink.SugarOptions.FirstOrDefault(s => s.Id == cartDrinkViewModel.SugarOptionId);
            if (drink.RequireSugarOption && sugarOption == null)
                throw new ApplicationException("SugarOption is invalid!");

            //var toppings = cartDrinkViewModel.Toppings.Select(t => _toppingRepository.GetToppingById(t.ToppingId)).ToList();
            cartDrinkViewModel.Toppings = cartDrinkViewModel.Toppings?.Where(t => t.IsChecked).ToList();
            Dictionary<int, Topping> toppingDictionary = cartDrinkViewModel.Toppings?.Select(t => new KeyValuePair<int, Topping>(t.ToppingId, _toppingRepository.GetToppingById(t.ToppingId))).ToDictionary(k => k.Key, k => k.Value);
            CartDrinkView view = new CartDrinkView
            {
                DrinkId = cartDrinkViewModel.DrinkId,
                Index = cartDrinkViewModel.Index,
                DrinkName = drink.Name,
                IceOptionId = cartDrinkViewModel.IceOptionId,
                IceOption = iceOption?.Name,
                SugarOptionId = cartDrinkViewModel.SugarOptionId,
                DrinkSize = drinkSize.Name,
                DrinkSizeId = drinkSize.Id,
                Price = drinkSize.Price,
                DrinkMaxSize = drinkMaxSize.Name,
                DrinkMaxSizeId = drinkMaxSize.Id,
                DrinkMaxSizePrice = drinkMaxSize.Price,
                DrinkMinSize = drinkMinSize.Name,
                DrinkMinSizeId = drinkMinSize.Id,
                DrinkMinSizePrice = drinkMinSize.Price,
                SugarOption = sugarOption?.Name,
                Quantity = cartDrinkViewModel.Quantity,
                Amount = (drinkSize.Price + (cartDrinkViewModel.Toppings?.Sum(t => t.Quantity * toppingDictionary?[t.ToppingId].Price) ?? 0)) * cartDrinkViewModel.Quantity,
                Toppings = cartDrinkViewModel.Toppings?.Select(t => new CartToppingView { Topping = toppingDictionary?[t.ToppingId].Name, ToppingId = t.ToppingId, Quantity = t.Quantity, Price = toppingDictionary?[t.ToppingId].Price ?? 0 }).ToList(),
                IsPromoDiscount = cartDrinkViewModel.IsPromoDiscount,
                IsDeleted = false,
                IsNewDrink = drink.IsNew
            };
            return PartialView("_CartItem", view);
        }

        [HttpPost]
        public IActionResult GetCart(CreateOrderModel orderModel)
        {
            var order = _mapper.Map<Data.Models.Order>(orderModel);
            order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
            _orderRepository.Normalize(order);
            return PartialView("_Cart", order);
        }

        public IActionResult CreateOrder(CreateOrderModel orderModel)
        {
            orderModel.OrderDetails.RemoveAll(od => od.IsDeleted);
            var order = _mapper.Map<Data.Models.Order>(orderModel);
            order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
            var success = _orderRepository.Create(order);
            if (success)
            {
                _hubContext.Clients.All.SendAsync("ReceiveNewOrder", $"{order.LastName} {order.MiddleName} {order.FirstName}", order.PhoneNumber, order.OrderDeliveried.ToString("HH:mm dd/MM"));

                foreach (var device in _deviceRepository.GetAll())
                {
                    try
                    {
                        var payload = "{\"title\":\"Đơn hàng mới\",\"message\":\"Họ tên: " + $"{order.LastName} {order.MiddleName} {order.FirstName}" + "\\nThời gian giao: " + order.OrderDeliveried.ToString("HH:mm dd/MM") + "\\nĐiện thoại: " + order.PhoneNumber + "\"}";
                        string vapidPublicKey = _configuration.GetSection("VapidKeys")["PublicKey"];
                        string vapidPrivateKey = _configuration.GetSection("VapidKeys")["PrivateKey"];

                        var pushSubscription = new PushSubscription(device.PushEndpoint,
                            device.PushP256DH,
                            device.PushAuth);
                        var vapidDetails = new VapidDetails("https://admin.ninjasaigon.com/Orders", vapidPublicKey, vapidPrivateKey);

                        var webPushClient = new WebPushClient();
                        webPushClient.SendNotification(pushSubscription, payload, vapidDetails);
                    }
                    catch
                    {
                        //fail device
                    }
                }
            }

            return Json(new { success });
        }

        public CheckPromotionResult CheckPromotionCode(string c)
        {
            return _promotionRepository.CheckPromotionCode(c, false);
        }

        [HttpPost]
        public IEnumerable<PromotionFreeDrinkResult> GetPromotionFreeDrink(CreateOrderModel orderModel)
        {
            orderModel.OrderDetails.RemoveAll(od => od.IsDeleted);
            var order = _mapper.Map<Data.Models.Order>(orderModel);
            _orderRepository.Normalize(order);
            var freeDrinks = order.OrderDetails
                .Where(od => od.IsFreeDrink == true)
                .Select(od=> new PromotionFreeDrinkResult {
                    DrinkName = od.DrinkName,
                    DrinkSize = od.DrinkSize,
                    Quantity = od.Quantity
                });
            return freeDrinks;
        }
    }
    
}
