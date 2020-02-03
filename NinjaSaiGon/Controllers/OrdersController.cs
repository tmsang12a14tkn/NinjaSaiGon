using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using NinjaSaiGon.Admin.Models.DataTable;
using NinjaSaiGon.Admin.Helpers;
using NinjaSaiGon.Admin.Models.OrderModels;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NinjaSaiGon.Admin.Models.DrinkViewModels;
using NinjaSaiGon.Data.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using NinjaSaiGon.Admin.Services;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IToppingRepository _toppingRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPrivatePromotionRepository _privatePromotionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IOrderSourceRepository _orderSourceRepository;
        private readonly IOrderSourceTypeRepository _orderSourceTypeRepository;
        private readonly IDeliveryPartnerRepository _deliveryPartnerRepository;
        private readonly IMapper _mapper;
        //private BXLAPI.BxlCallBackDelegate statusCallBackDelegate = null;
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPdfService _pdfService;

        public OrdersController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IToppingRepository toppingRepository, IOrderSourceRepository orderSourceRepository,
            IDrinkRepository drinkRepository, ICategoryRepository categoryRepository, IPromotionRepository promotionRepository, IPrivatePromotionRepository privatePromotionRepository,
            IPersonRepository personRepository, IAgencyRepository agencyRepository, IOrderSourceTypeRepository orderSourceTypeRepository, IDeliveryPartnerRepository deliveryPartnerRepository,
            IMapper mapper, IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IPdfService pdfService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _promotionRepository = promotionRepository;
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
            _toppingRepository = toppingRepository;
            _privatePromotionRepository = privatePromotionRepository;
            _personRepository = personRepository;
            _agencyRepository = agencyRepository;
            _orderSourceRepository = orderSourceRepository;
            _orderSourceTypeRepository = orderSourceTypeRepository;
            _deliveryPartnerRepository = deliveryPartnerRepository;
            _mapper = mapper;
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult QuickCreate()
        {
            ViewBag.OrderSourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            ViewBag.DeliveryPartners = _deliveryPartnerRepository.DeliveryPartners.ToList();
            ViewBag.IsInBuy1Get1 = _promotionRepository.CheckPromotionBuy1Get1() ? 1 : 0;
            return View();
        }

        public IActionResult QuickEdit(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            var model = _mapper.Map<OrderViewModel>(order);
            foreach (var item in model.OrderDetails)
            {
                var drink = _drinkRepository.GetDrinkById(item.DrinkId);

                var allDrinkSizes = drink.DrinkSizes.Where(ds => ds.IsActive).OrderBy(dz => dz.Price);
                var drinkMaxSize = allDrinkSizes.LastOrDefault();
                var drinkMinSize = allDrinkSizes.FirstOrDefault();

                item.CategoryId = drink.CategoryId;
                item.DrinkMaxSize = drinkMaxSize.Name;
                item.DrinkMaxSizeId = drinkMaxSize.Id;
                item.DrinkMaxSizePrice = drinkMaxSize.Price;
                item.DrinkMinSize = drinkMinSize.Name;
                item.DrinkMinSizeId = drinkMinSize.Id;
                item.DrinkMinSizePrice = drinkMinSize.Price;
            }

            ViewBag.OrderSourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            ViewBag.DeliveryPartners = _deliveryPartnerRepository.DeliveryPartners.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDetails?.RemoveAll(od => od.IsDeleted);
                order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                _orderRepository.Create(order);
                return RedirectToAction("Edit", new { orderId = order.OrderId });
            }
            else
            {
                return View(order);
            }
        }

        [HttpPost]
        public IActionResult QuickCreate(Order order, bool isPrint)
        {
            if (ModelState.IsValid)
            {
                order.OrderDetails?.RemoveAll(od => od.IsDeleted);
                order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                var check = _orderRepository.Create(order);

                if (!isPrint)
                    return RedirectToAction("Index");
                else
                {
                    var orderView = _mapper.Map<OrderView>(order);
                    if (!string.IsNullOrEmpty(orderView.PromotionCode))
                    {
                        var promotion = _promotionRepository.GetPromotionByCode(orderView.PromotionCode);
                        orderView.PromotionName = promotion?.Name;
                    }

                    PrintBills(orderView);
                    var bytes = GetPdfBytes(orderView.OrderId);

                    return File(bytes, "application/pdf");
                }
            }
            else
            {
                ViewBag.OrderSourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
                ViewBag.DeliveryPartners = _deliveryPartnerRepository.DeliveryPartners.ToList();
                return View(order);
            }
        }

        public IActionResult Details(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            var orderView = _mapper.Map<OrderView>(order);
            return PartialView(orderView);
        }

        [HttpPost]
        public IActionResult GetCartItem(CartDrinkViewModel cartDrinkViewModel)
        {
            var drink = _drinkRepository.GetDrinkById(cartDrinkViewModel.DrinkId);
            var drinkSize = drink.DrinkSizes.FirstOrDefault(s => s.Id == cartDrinkViewModel.DrinkSizeId) ?? drink.PrimarySize;

            var allDrinkSizes = drink.DrinkSizes.Where(ds => ds.IsActive).OrderBy(dz => dz.Price);
            var drinkMaxSize = allDrinkSizes.LastOrDefault();
            var drinkMinSize = allDrinkSizes.FirstOrDefault();

            if (drinkSize == null || drinkMaxSize == null || drinkMinSize == null)
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
                CategoryId = cartDrinkViewModel.CategoryId,
                DrinkId = cartDrinkViewModel.DrinkId,
                Index = cartDrinkViewModel.Index,
                OrderDetailId = cartDrinkViewModel.OrderDetailId,
                DrinkName = drink.Name,
                Price = cartDrinkViewModel.IsFreeDrink ? 0 : drinkSize.Price,
                DrinkMaxSize = drinkMaxSize.Name,
                DrinkMaxSizeId = drinkMaxSize.Id,
                DrinkMaxSizePrice = drinkMaxSize.Price,
                DrinkMinSize = drinkMinSize.Name,
                DrinkMinSizeId = drinkMinSize.Id,
                DrinkMinSizePrice = drinkMinSize.Price,
                FullPrice = cartDrinkViewModel.IsFreeDrink ? 0 : drinkSize.Price + (cartDrinkViewModel.Toppings?.Sum(t => t.Quantity * toppingDictionary?[t.ToppingId].Price) ?? 0),
                IceOptionId = cartDrinkViewModel.IceOptionId,
                IceOption = iceOption?.Name,
                SugarOptionId = cartDrinkViewModel.SugarOptionId,
                DrinkSize = drinkSize.Name,
                DrinkSizeId = drinkSize.Id,
                SugarOption = sugarOption?.Name,
                Quantity = cartDrinkViewModel.Quantity,
                Amount = cartDrinkViewModel.IsFreeDrink ? 0 : (drinkSize.Price + (cartDrinkViewModel.Toppings?.Sum(t => t.Quantity * toppingDictionary?[t.ToppingId].Price) ?? 0)) * cartDrinkViewModel.Quantity,
                Toppings = cartDrinkViewModel.Toppings?.Select(t => new CartToppingView { Topping = toppingDictionary?[t.ToppingId].Name, ToppingId = t.ToppingId, Quantity = t.Quantity, Price = toppingDictionary?[t.ToppingId].Price ?? 0 }).ToList(),
                IsPromoDiscount = cartDrinkViewModel.IsPromoDiscount,
                IsDeleted = false,
                DiscountType = cartDrinkViewModel.DiscountType,
                DiscountPercentValue = cartDrinkViewModel.DiscountPercentValue,
                DiscountMoneyValue = cartDrinkViewModel.DiscountMoneyValue,
                FreeDrinkReasonId = cartDrinkViewModel.FreeDrinkReasonId,
                DiscountReason = cartDrinkViewModel.DiscountReason,
                IsFreeDrink = cartDrinkViewModel.IsFreeDrink,
                Note = cartDrinkViewModel.Note,
                IsNew = drink.IsNew
            };
            return PartialView("_CartItem", view);
        }

        [HttpPost]
        public IActionResult FilterCustomer(OrderCustomerFilterViewModel model)
        {
            var result = new List<OrderCustomerFilterResult>();
            var arrNames = !string.IsNullOrEmpty(model.CustomerName) ? model.CustomerName.Split(' ') : null;

            if (model.OrderCustomerType == OrderCustomerType.Customer)
            {
                var customers = _personRepository.Customers.ToList();

                if (!string.IsNullOrEmpty(model.CustomerPhone))
                    customers = customers.Where(c => c.ContactInfo.Phones.Any(p => !string.IsNullOrEmpty(p.PhoneNumber) && p.PhoneNumber.Contains(model.CustomerPhone))).ToList();

                if (arrNames != null)
                {
                    var filterCusName = new List<Person>();
                    foreach (var subName in arrNames)
                    {
                        var subCus = customers.Where(c => c.PrimaryName.ToLower().Contains(subName.ToLower())).ToList();
                        if (subCus.Any())
                            filterCusName.AddRange(subCus.Except(filterCusName));
                    }

                    if (filterCusName.Any())
                        customers = filterCusName;
                }

                if (customers.Count > 0)
                {
                    foreach (var cus in customers)
                    {
                        var person = _personRepository.GetById(cus.Id);
                        var item = new OrderCustomerFilterResult
                        {
                            Id = person.Id,
                            PrimaryName = person.PrimaryName,
                            FirstName = person.Names.ElementAt(0).FirstName,
                            MidName = person.Names.ElementAt(0).MidName,
                            LastName = person.Names.ElementAt(0).LastName,
                            OrderCustomerType = model.OrderCustomerType,
                            Phones = person.ContactInfo.Phones.Select(cp => cp.PhoneNumber).ToList()
                        };
                        result.Add(item);
                    }
                }
            }
            else
            {
                var agencies = _agencyRepository.Agencies.ToList();
                if (!string.IsNullOrEmpty(model.CustomerPhone))
                    agencies = agencies.Where(c => c.AgencyContactInfo.Phones.Any(p => !string.IsNullOrEmpty(p.PhoneNumber) && p.PhoneNumber.Contains(model.CustomerPhone))).ToList();

                if (arrNames != null)
                {
                    var filterCusName = new List<Agency>();
                    foreach (var subName in arrNames)
                    {
                        var subAgency = agencies.Where(c => c.Name.ToLower().Contains(subName.ToLower())).ToList();
                        if (subAgency.Any())
                            filterCusName.AddRange(subAgency.Except(filterCusName));
                    }

                    if (filterCusName.Any())
                        agencies = filterCusName;
                }

                if (agencies.Count > 0)
                {
                    foreach (var cus in agencies)
                    {
                        var agency = _agencyRepository.GetById(cus.Id);
                        var item = new OrderCustomerFilterResult
                        {
                            Id = agency.Id,
                            PrimaryName = agency.Name,
                            OrderCustomerType = model.OrderCustomerType,
                            Phones = agency.AgencyContactInfo.Phones.Select(cp => cp.PhoneNumber).ToList()
                        };
                        result.Add(item);
                    }
                }
            }
            return PartialView("_FilterCustomer", result);
        }
        
        public IActionResult FilterOrderSource(int sourceType, string sourceName)
        {
            var arrNames = !string.IsNullOrEmpty(sourceName) ? sourceName.Split(' ') : null;
            var sources = _orderSourceRepository.OrderSources.ToList();
            
            if (arrNames != null)
            {
                var filterSource = new List<OrderSource>();
                foreach (var subName in arrNames)
                {
                    var subSource = sources.Where(c => c.Name.ToLower().Contains(subName.ToLower())).ToList();
                    if (subSource.Any())
                        filterSource.AddRange(subSource.Except(filterSource));
                }

                if (filterSource.Any())
                    sources = filterSource;
            }

            return PartialView("_FilterOrderSource", sources);
        }

        public CheckPromotionResult CheckPromotionCode(string c, int orderId)
        {
            var isSameCode = false;
            var oldOrder = _orderRepository.GetById(orderId);
            if (oldOrder != null)
            {
                if ((!string.IsNullOrEmpty(oldOrder.PromotionCode) && string.IsNullOrEmpty(c)) || (!string.IsNullOrEmpty(oldOrder.PromotionCode) && !string.IsNullOrEmpty(c) && oldOrder.PromotionCode == c))
                    isSameCode = true;
            }
            return _promotionRepository.CheckPromotionCode(c, isSameCode);
        }

        public IActionResult Edit(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            return View(order);
        }

        [HttpPost]
        public IActionResult QuickEdit(int orderId, bool isPrint, int typePrint, OrderViewModel orderModel)
        {
            if (orderId != orderModel.OrderId)
            {
                return NotFound();
            }
            var order = _orderRepository.GetById(orderId);

            if (ModelState.IsValid)
            {
                if (order == null) return NotFound();

                var isAddNewCode = (!string.IsNullOrEmpty(orderModel.PromotionCode) && string.IsNullOrEmpty(order.PromotionCode)) ||
                                    (!string.IsNullOrEmpty(orderModel.PromotionCode) && !string.IsNullOrEmpty(order.PromotionCode) && order.PromotionCode != orderModel.PromotionCode);

                _mapper.Map(orderModel, order);

                foreach (var orderDetailModel in orderModel.OrderDetails)
                {
                    //update or delete
                    if (orderDetailModel.OrderDetailId != 0)
                    {
                        var orderDetail = order.OrderDetails.FirstOrDefault(od => od.OrderDetailId == orderDetailModel.OrderDetailId);
                        _mapper.Map(orderDetailModel, orderDetail);
                    }
                    //add
                    else
                    {
                        var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);
                        order.OrderDetails.Add(orderDetail);
                    }
                }
                order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                _orderRepository.Edit(order);

                //update private promotion
                var privatePromotionCode = _privatePromotionRepository.AllCode.FirstOrDefault(c => c.Code == orderModel.PromotionCode);
                if (privatePromotionCode != null && privatePromotionCode.CurrentUseCode < privatePromotionCode.MaxUseCode && isAddNewCode)
                {
                    privatePromotionCode.CurrentUseCode++;
                    _privatePromotionRepository.UpdateCode(privatePromotionCode);
                }

                if (!isPrint)
                    return new RedirectResult(Url.Action("Index") + OrderStatusMethods.GetTab(order.Status));
                else
                {
                    var orderView = _mapper.Map<OrderView>(order);
                    if (!string.IsNullOrEmpty(orderView.PromotionCode))
                    {
                        var promotion = _promotionRepository.GetPromotionByCode(orderView.PromotionCode);
                        orderView.PromotionName = promotion?.Name;
                    }

                    if (typePrint == 0)
                    {
                        PrintBills(orderView, typePrint);
                        var bytes = GetPdfBytes(orderId);
                        return File(bytes, "application/pdf");
                    }
                    else if (typePrint == 1 || typePrint == 2)
                    {
                        PrintBills(orderView, typePrint);
                        return Ok();
                    }
                    else if (typePrint == 3)
                    {
                        var bytes = GetPdfBytes(orderId);
                        return File(bytes, "application/pdf");
                    }
                    else
                        throw new ApplicationException("Có lỗi khi in!");
                }
            }

            ViewBag.DeliveryPartners = _deliveryPartnerRepository.DeliveryPartners.ToList();
            ViewBag.OrderSourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            return View(orderModel);
        }

        [HttpPost]
        public IActionResult EditInfo(int orderId, EditOrderInfoModel orderModel)
        {
            if (orderId != orderModel.OrderId)
            {
                return NotFound();
            }
            var order = _orderRepository.GetById(orderId);

            if (ModelState.IsValid)
            {
                try
                {
                    if (order == null)
                        return NotFound();
                    _mapper.Map(orderModel, order);
                    order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                    var success = _orderRepository.Edit(order);
                    //if (success)
                    //{
                    //    TempData["Message"] = "Lưu thành công";
                    //}
                    //else
                    //{
                    //    TempData["Message"] = "Lưu thất bại";
                    //}
                    //return new RedirectResult(Url.Action("Edit", new { orderId = orderModel.OrderId }) + "#editinfo");
                    return Json(new { success, action = "editinfo", orderTab = order.Status.GetTab() });
                }
                catch (DbUpdateConcurrencyException e)
                {
                    //TempData["Message"] = "Lưu thất bại. Lỗi: " + e.Message;
                    //return new RedirectResult(Url.Action("Edit", new { orderId = orderModel.OrderId }) + "#editinfo");
                    return Json(new { success = false, action = "editinfo", error = e.Message });
                }
            }
            var modelErrors = new List<string>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Any())
                {
                    modelErrors.Add(key);
                }
            }
            //TempData["Message"] = "Lưu thất bại. Lỗi: " + String.Join(",", modelErrors.ToArray());
            //return new RedirectResult(Url.Action("Edit", new { orderId = orderModel.OrderId }) + "#editinfo");
            return Json(new { success = false, error = String.Join(",", modelErrors.ToArray()) });

        }

        public IActionResult EditCustomer(int orderId, EditOrderCustomerModel customerModel)
        {
            if (orderId != customerModel.OrderId)
            {
                return NotFound();
            }
            var order = _orderRepository.GetById(orderId);

            if (ModelState.IsValid)
            {
                if (order == null)
                    return NotFound();
                _mapper.Map(customerModel, order);
                order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                var success = _orderRepository.Edit(order);
                //if (success)
                //{
                //    TempData["Message"] = "Lưu thành công";
                //}
                //else
                //{
                //    TempData["Message"] = "Lưu thất bại";
                //}
                //return new RedirectResult(Url.Action("Edit", new { orderId = customerModel.OrderId }) + "#editcustomer");
                return Json(new { success, action = "editcustomer" });

            }
            //TempData["Message"] = "Lưu thất bại";
            //return new RedirectResult(Url.Action("Edit", new { orderId = customerModel.OrderId }) + "#editcustomer");
            return Json(new { success = false, action = "editcustomer" });
        }

        public IActionResult EditDelivery(int orderId, EditOrderDeliveryModel deliveryModel)
        {
            if (orderId != deliveryModel.OrderId)
            {
                return NotFound();
            }
            var order = _orderRepository.GetById(orderId);

            if (ModelState.IsValid)
            {
                try
                {
                    if (order == null)
                        return NotFound();
                    if (order.OrderDelivery == null) order.OrderDelivery = new OrderDelivery();
                    _mapper.Map(deliveryModel, order.OrderDelivery);
                    order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                    var success = _orderRepository.Edit(order);
                    //if (success)
                    //{
                    //    TempData["Message"] = "Lưu thành công";
                    //}
                    //else
                    //{
                    //    TempData["Message"] = "Lưu thất bại";
                    //}
                    //return new RedirectResult(Url.Action("Edit", new { orderId = deliveryModel.OrderId }) + "#editdelivery");
                    return Json(new { success, action = "editdelivery" });

                }
                catch (DbUpdateConcurrencyException e)
                {
                    //TempData["Message"] = "Lưu thất bại. Lỗi: " + e.Message;
                    //return new RedirectResult(Url.Action("Edit", new { orderId = deliveryModel.OrderId }) + "#editdelivery");
                    return Json(new { success = false, error = e.Message, action = "editdelivery" });
                }
            }

            //TempData["Message"] = "Lưu thất bại. Lỗi: ModelState is not valid";
            //return new RedirectResult(Url.Action("Edit", new { orderId = deliveryModel.OrderId }) + "#editdelivery");
            return Json(new { success = false, error = "ModelState is not valid", action = "editdelivery" });
        }

        public IActionResult EditNotes(int orderId, EditOrderNotesModel notesModel)
        {
            if (orderId != notesModel.OrderId)
            {
                return NotFound();
            }
            var order = _orderRepository.GetById(orderId);

            if (ModelState.IsValid)
            {
                try
                {
                    if (order == null)
                        return NotFound();
                    _mapper.Map(notesModel, order);
                    order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
                    var success = _orderRepository.Edit(order);
                    return Json(new { success, action = "editnotes", employeeNote = order.EmployeeNote, customerNote = order.CustomerNote });
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return Json(new { success = false, action = "editnotes", error = e.Message });
                }
            }
            var modelErrors = new List<string>();
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Any())
                {
                    modelErrors.Add(key);
                }
            }
            return Json(new { success = false, error = String.Join(",", modelErrors.ToArray()) });
        }

        public IActionResult EditDetails(int orderDetailId)
        {

            var orderDetail = _orderDetailRepository.GetById(orderDetailId);
            var drink = _drinkRepository.GetDrinkById(orderDetail.DrinkId);
            var toppings = _toppingRepository.GetToppingByDrinkId(drink.Id);
            var otherToppings = _toppingRepository.Toppings.Except(toppings.SelectMany(tg => tg.Toppings), new ToppingCompare()).ToList();
            toppings.Add(new Data.ViewModels.GroupedTopping { Name = "Khác", Toppings = otherToppings });
            ViewBag.Toppings = toppings;
            ViewBag.OtherToppings = _toppingRepository.Toppings.Except(toppings.SelectMany(tg => tg.Toppings));
            ViewBag.DrinkSizes = drink.DrinkSizes;
            ViewBag.IceOptions = drink.IceOptions;
            ViewBag.SugarOptions = drink.SugarOptions;
            ViewBag.DisplayIceOption = drink.DisplayIceOption;
            ViewBag.DisplaySugarOption = drink.DisplaySugarOption;
            ViewBag.RequireIceOption = drink.RequireIceOption;
            ViewBag.RequireSugarOption = drink.RequireSugarOption;

            return PartialView("_EditDetails", _mapper.Map<EditOrderDetailModel>(orderDetail));
        }

        public IActionResult AddDetails(int drinkId)
        {
            var drink = _drinkRepository.GetDrinkById(drinkId);
            var toppings = _toppingRepository.GetToppingByDrinkId(drink.Id);
            var otherToppings = _toppingRepository.Toppings.Except(toppings.SelectMany(tg => tg.Toppings), new ToppingCompare()).ToList();
            toppings.Add(new Data.ViewModels.GroupedTopping { Name = "Khác", Toppings = otherToppings });
            ViewBag.Toppings = toppings;
            ViewBag.DrinkSizes = drink.DrinkSizes;
            ViewBag.IceOptions = drink.IceOptions;
            ViewBag.SugarOptions = drink.SugarOptions;
            ViewBag.DisplayIceOption = drink.DisplayIceOption;
            ViewBag.DisplaySugarOption = drink.DisplaySugarOption;
            ViewBag.RequireIceOption = drink.RequireIceOption;
            ViewBag.RequireSugarOption = drink.RequireSugarOption;

            return PartialView("_EditDetails",
                new EditOrderDetailModel()
                {
                    DrinkId = drinkId,
                    Quantity = 1,
                    DrinkSizeId = drink.PrimarySize.Id,
                    IceOption = (drink.IceOptions?.FirstOrDefault(t => t.IsPrimary) ?? new IceOption()).Name,
                    SugarOption = (drink.SugarOptions?.FirstOrDefault(t => t.IsPrimary) ?? new SugarOption()).Name
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDetails(EditOrderDetailModel orderDetailModel)
        {
            if (orderDetailModel.OrderDetailToppings != null)
                orderDetailModel.OrderDetailToppings.RemoveAll(t => !t.IsChecked);
            var isNew = orderDetailModel.OrderDetailId == 0;

            var orderDetail = isNew ? new OrderDetail() : _orderDetailRepository.GetById(orderDetailModel.OrderDetailId);
            _mapper.Map(orderDetailModel, orderDetail);

            //set drink name/drink size if new 
            if (isNew)
            {
                var drink = _drinkRepository.GetDrinkById(orderDetail.DrinkId);
                orderDetail.DrinkName = drink.Name;
                orderDetail.IsNewDrink = drink.IsNew;
            }

            var drinkSize = _drinkRepository.GetSizeById(orderDetailModel.DrinkSizeId);
            orderDetail.DrinkSize = drinkSize.Name;
            orderDetail.Price = drinkSize.Price;

            //update price
            orderDetail.FullPrice = orderDetail.Price + (orderDetail.OrderDetailToppings?.Sum(t => t.Price * t.Quantity) ?? 0);
            orderDetail.Amount = orderDetail.FullPrice * orderDetail.Quantity;

            var success = isNew ? _orderDetailRepository.Create(orderDetail) : _orderDetailRepository.Edit(orderDetail);
            if (success)
            {
                TempData["Message"] = "Lưu thành công";
            }
            else
            {
                TempData["Message"] = "Lưu thất bại";
            }

            //update order
            var order = _orderRepository.GetById(orderDetail.OrderId);
            order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
            _orderRepository.Edit(order);

            return new RedirectResult(Url.Action("Edit", new { orderId = orderDetail.OrderId }) + "#editdrinks");
        }

        public IActionResult GetPdf(int orderId, int type)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new ApplicationException("Không tìm thấy đơn đặt hàng!");
            }

            var orderView = _mapper.Map<OrderView>(order);
            if (!string.IsNullOrEmpty(orderView.PromotionCode))
            {
                var promotion = _promotionRepository.GetPromotionByCode(orderView.PromotionCode);
                orderView.PromotionName = promotion?.Name;
            }

            if (type == 0)
            {
                PrintBills(orderView, type);
                var bytes = GetPdfBytes(orderId);
                return File(bytes, "application/pdf");
            }
            else if (type == 1 || type == 2)
            {
                PrintBills(orderView, type);
                return Ok();
            }
            else if (type == 3)
            {
                var bytes = GetPdfBytes(orderId);
                return File(bytes, "application/pdf");
            }
            else
                throw new ApplicationException("Có lỗi khi in!");
        }

        public IActionResult PaymentInfo(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            return PartialView("PaymentInfo", order);
        }

        public IActionResult DeleteOrderDetail(int orderDetailId)
        {
            var orderDetail = _orderDetailRepository.GetById(orderDetailId);
            var orderId = orderDetail.OrderId;

            _orderDetailRepository.Delete(orderDetailId);

            //update order
            var order = _orderRepository.GetById(orderDetail.OrderId);
            order.IsSpecialPromo = _promotionRepository.CheckPromotionBuy1Get1();
            _orderRepository.Edit(order);

            return new RedirectResult(Url.Action("Edit", new { orderId = orderDetail.OrderId }) + "#editdrinks");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _orderRepository.Delete(id);
                return Json(new { success = true, id = id });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        //API
        public JsonResult All()
        {
            var beginTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Json(new
            {
                data = _orderRepository.List.Select(o => new object[] {
                    o.Code,
                    EnumHelper<OrderStatus>.GetDisplayValue(o.Status),
                    (o.OrderPlaced - beginTime).TotalSeconds,
                    (o.OrderDeliveried - beginTime).TotalSeconds,
                    o.OrderDetails.Sum(od=>od.Quantity), //.Select(od=> new { DrinkName = od.DrinkName, Quantity = od.Quantity}),
                    o.CustomerNote,
                    String.Join(" ", new string[] {o.LastName, o.MiddleName, o.FirstName}.Where(s => !string.IsNullOrEmpty(s))),
                    o.AddressLine,
                    o.PhoneNumber,
                    o.OrderId.ToString(),
                    o.IsDeliveryNow,
                    o.Distance
                })
            });
        }

        public JsonResult Today()
        {
            var beginTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Json(new
            {
                data = _orderRepository.TodayList.Select(o => new object[] {
                    o.Code,
                    EnumHelper<OrderStatus>.GetDisplayValue(o.Status),
                    (o.OrderPlaced - beginTime).TotalSeconds,
                    (o.OrderDeliveried - beginTime).TotalSeconds,
                    o.OrderDetails.Sum(od=>od.Quantity), //.Select(od=> new { DrinkName = od.DrinkName, Quantity = od.Quantity}),
                    o.CustomerNote,
                    String.Join(" ", new string[] {o.LastName, o.MiddleName, o.FirstName}.Where(s => !string.IsNullOrEmpty(s))),
                    o.AddressLine,
                    o.PhoneNumber,
                    o.OrderId.ToString(),
                    o.IsDeliveryNow,
                    o.Distance
                })
            });
        }

        public IEnumerable<DrinkCategoryView> DrinkCategories()
        {
            return _categoryRepository.Categories.Select(c => new DrinkCategoryView()
            {
                Id = c.Id,
                Name = c.Name,
                Drinks = c.Drinks.Where(d => d.IsActive).Select(d => new DrinkView
                {
                    Id = d.Id,
                    Name = d.Name,
                    IceOptions = d.IceOptions?.Select(i => new OptionView
                    {
                        Id = i.Id,
                        Name = i.Name
                    }),
                    SugarOptions = d.SugarOptions?.Select(i => new OptionView
                    {
                        Id = i.Id,
                        Name = i.Name
                    }),
                    SizeOptions = d.DrinkSizes?.Select(i => new OptionView
                    {
                        Id = i.Id,
                        Name = i.Name
                    }),
                    PrimarySize = d.DrinkSizes?.FirstOrDefault(s => s.IsPrimary)?.Name ?? d.DrinkSizes.First().Name,
                    PrimaryIce = d.IceOptions?.FirstOrDefault(i => i.IsPrimary)?.Name ?? d.IceOptions.First().Name,
                    PrimarySugar = d.SugarOptions?.FirstOrDefault(s => s.IsPrimary)?.Name ?? d.SugarOptions.First().Name
                })
            });
        }

        public JsonResult CustomServerSideSearchAction(DataTableAjaxPostModel model)
        {
            // action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var res = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

            var result = new List<Order>(res.Count);
            foreach (var s in res)
            {
                // simple remapping adding extra info to found dataset
                result.Add(new Order
                {

                });
            };

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }

        private IList<Order> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = _orderRepository.Query(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<Order>();
            }
            return result.ToList();
        }

        private void PrintBills(OrderView orderView, int type = 0)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            var path = _hostingEnvironment.WebRootPath;
            var logoPath = path + "/images/logo.bmp";
            var qrPath = path + "/images/ninja-saigon-qr.bmp";

            var isConnected = true;
            var bill4CusOK = false;
            var bill4SelfOK = false;
            var bill4Cook = false;

            //Connect via USB
            //if (BXLAPI.PrinterOpen(BXLAPI.IUsb, "", 0, 0, 0, 0) != BXLAPI.BXL_SUCCESS)
            //{
            //    isConnected = false;
            //}

            //Connect via LAN
            if (BXLAPI.PrinterOpen(BXLAPI.ILan, "27.74.252.26", 9100, 0, 0, 0) != BXLAPI.BXL_SUCCESS)
            {
                isConnected = false;
            }

            if (isConnected)
            {
                if (type == 0)
                {
                    bill4CusOK = PrintHelper.PrintBillForCustomer(orderView, logoPath, qrPath, cul);
                    bill4SelfOK = PrintHelper.PrintBillForSelf(orderView, logoPath, qrPath, cul);
                    bill4Cook = PrintHelper.PrintBillForCook(orderView, logoPath, qrPath, cul);

                    BXLAPI.PrinterClose();

                    if (!bill4CusOK || !bill4SelfOK || !bill4Cook)
                        throw new ApplicationException("Có lỗi xảy ra trong quá trình in. Vui lòng thử lại!");
                }
                else if (type == 1)
                {
                    bill4CusOK = PrintHelper.PrintBillForCustomer(orderView, logoPath, qrPath, cul);
                    bill4SelfOK = PrintHelper.PrintBillForSelf(orderView, logoPath, qrPath, cul);

                    BXLAPI.PrinterClose();

                    if (!bill4CusOK || !bill4SelfOK)
                        throw new ApplicationException("Có lỗi xảy ra trong quá trình in. Vui lòng thử lại!");
                }
                else if (type == 2)
                {
                    bill4Cook = PrintHelper.PrintBillForCook(orderView, logoPath, qrPath, cul);

                    BXLAPI.PrinterClose();

                    if (!bill4Cook)
                        throw new ApplicationException("Có lỗi xảy ra trong quá trình in. Vui lòng thử lại!");
                }
            }
            else
                throw new ApplicationException("Không kết nối được với máy in!");
        }

        private byte[] GetPdfBytes(int orderId)
        {
            var stickerHtml = _orderRepository.GetHtml(orderId);
            return _pdfService.GeneratePdfFromHtml(stickerHtml);
        }

        public IActionResult CountDrinkDiscountFree()
        {
            try
            {
                var orders = _orderRepository.All;
                foreach (var order in orders)
                {
                    _orderRepository.UpdatePromotionFreeDrinkCount(order);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        public IActionResult FixDataBugs()
        {
            try
            {
                var orders = _orderRepository.All;
                var tmpDate = new DateTime();
                var tmpIndex = 1;
                foreach (var order in orders)
                {
                    if (tmpDate.Date != order.OrderPlaced.Date)
                    {
                        tmpDate = order.OrderPlaced;
                        tmpIndex = 1;
                    }
                    order.Code = $"PDC{order.OrderPlaced:yyyyMMdd}{tmpIndex:0000}";
                    tmpIndex += 1;
                    var success = _orderRepository.Edit(order);
                }

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}