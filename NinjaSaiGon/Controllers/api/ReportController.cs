using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Helpers;
using NinjaSaiGon.Admin.Models.ReportViewModels;
using NinjaSaiGon.Data.Interfaces;

namespace NinjaSaiGon.Admin.Controllers.api
{
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ReportController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IDrinkRepository drinkRepository, ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("ByWeek")]
        public WeekReportFilterResult ByWeek([FromQuery]WeekReportFilterInput input)
        {
            if (!input.From.HasValue) input.From = DateTime.Now.Date.AddDays(-7);
            if (!input.To.HasValue) input.To = DateTime.Now.Date;

            if (input.Count <= 0) input.Count = 10;
            if (input.Page <= 0) input.Page = 1;

            var result = new WeekReportFilterResult();
            var allDrinks = _drinkRepository.Drinks.ToList();
            var queryDays = _orderRepository.GetAll.Include(o => o.OrderDetails).Include(o => o.OrderDelivery).ThenInclude(od => od.DeliveryPartner)
                                .Where(o => !o.IsDeleted && o.OrderPlaced >= input.From && o.OrderPlaced < input.To).AsNoTracking().ToList();

            result.Days = queryDays
                .GroupBy(o => o.OrderPlaced.Date)
                .OrderByDescending(g => g.Key)
                .Select(g => new DayWeekReportItem
                {
                    Date = g.Key,
                    DateString = Utilities.FormatDatetime(g.Key),
                    Orders = g.OrderBy(o => o.OrderPlaced).Select(o => new OrderWeekReportItem
                    {
                        OrderId = o.OrderId,
                        OrderCode = o.Code,
                        ShipFee = o.ShipFee,
                        NinjaOrderTotal = o.OrderTotal,
                        AllDrinkCount = o.OrderDetails.Sum(od => od.Quantity),
                        FreeDrinkCount = o.FreeDrinkCount,
                        DiscountDrinkCount = o.DiscountDrinkCount,
                        BaseDrinkCount = o.BaseDrinkCount,
                        DrinksTotal = o.OrderDetails.Sum(od => od.Amount),
                        Partner = o.OrderDelivery != null ? o.OrderDelivery.DeliveryPartner?.Name : "",
                        PartnerShipFee = o.OrderDelivery != null ? o.OrderDelivery.PartnerShipFee : 0
                    }).ToList()
                }).ToList();

            if (result.Days.Any())
            {
                foreach (var day in result.Days)
                {
                    if (day.Orders.Any())
                    {
                        foreach (var order in day.Orders)
                        {
                            var orderDetails = queryDays.First(o => o.OrderId == order.OrderId).OrderDetails;
                            var query = (from od in orderDetails
                                         join d in allDrinks on od.DrinkId equals d.Id
                                         select new { od, d } into odd
                                         group odd by odd.d.Category.Name into grp
                                         select new OrderDetailFilterResult
                                         {
                                             CategoryName = grp.Key,
                                             DrinkCount = grp.Count()
                                         }).ToList();

                            foreach (var grp in query)
                            {
                                if (grp.CategoryName.Contains("Cafe"))
                                    order.CafeCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Trà sữa"))
                                    order.MilkTeaCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Trà"))
                                    order.TeaCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Thanh nhiệt"))
                                    order.HerbalCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Nước ép"))
                                    order.FruitCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Macchiato"))
                                    order.MacchiatoCount += grp.DrinkCount;
                                else if (grp.CategoryName.Contains("Soda & Mojito"))
                                    order.SodaCount += grp.DrinkCount;
                            }

                            result.SumAllDrinkCount += order.AllDrinkCount;
                            result.SumBaseDrinkCount += order.BaseDrinkCount;
                            result.SumFreeDrinkCount += order.FreeDrinkCount;
                            result.SumDiscountDrinkCount += order.DiscountDrinkCount;

                            result.SumCafeCount += order.CafeCount;
                            result.SumTeaCount += order.TeaCount;
                            result.SumMilkTeaCount += order.MilkTeaCount;
                            result.SumHerbalCount += order.HerbalCount;
                            result.SumFruitCount += order.FruitCount;
                            result.SumMacchiatoCount += order.MacchiatoCount;
                            result.SumSodaCount += order.SodaCount;

                            result.SumDrinksTotal += order.DrinksTotal;
                            result.SumMoneyDiscount += order.MoneyDiscount;
                            result.SumMoneySurcharge += order.MoneySurcharge;
                            result.SumShipFee += order.ShipFee;
                            result.SumPartnerShipFee += order.PartnerShipFee;
                            result.SumNinjaDayTotal += order.NinjaOrderTotal;
                        }
                    }
                }
            }

            result.From = input.From;
            result.To = input.To;
            result.Page = input.Page;
            result.TotalPage = (result.Days.Count() + input.Count - 1) / input.Count;
            result.SumOrder = result.Days.Sum(d => d.Orders.Count());

            if (!input.LoadAll)
                result.Days = result.Days.Skip((input.Page - 1) * input.Count).Take(input.Count);

            return result;
        }

        [HttpGet("ByDay")]
        public DayReportFilterResult ByDay([FromQuery]DayReportFilterInput input)
        {
            if (!input.From.HasValue) input.From = DateTime.Now.Date.AddDays(-7);
            if (!input.To.HasValue) input.To = DateTime.Now.Date;

            if (input.Count <= 0) input.Count = 10;
            if (input.Page <= 0) input.Page = 1;

            var result = new DayReportFilterResult();
            var ordersQuery = _orderRepository.GetAll.Include(o => o.OrderDetails)
                                    .Include("OrderDetails.OrderDetailToppings").Include(o => o.OrderDelivery).ThenInclude(od => od.DeliveryPartner)
                                    .Where(o => !o.IsDeleted && o.OrderPlaced >= input.From && o.OrderPlaced.Date <= input.To).AsNoTracking().ToList();
            result.Days = ordersQuery
                .GroupBy(o => o.OrderPlaced.Date)
                .OrderByDescending(g => g.Key)
                .Skip((input.Page - 1) * input.Count).Take(input.Count)
                .Select(g => new DayReportItem
                {
                    Date = Utilities.FormatDatetime(g.Key),
                    Orders = g.OrderBy(o => o.OrderPlaced).Select(o => new OrderDayReportItem
                    {
                        OrderId = o.OrderId,
                        OrderCode = o.Code,
                        OrderPlaced = o.OrderPlaced,
                        OrderDeliveried = o.OrderDeliveried,
                        PromotionCode = o.PromotionCode,
                        MoneySurcharge = o.SurchargeAmount,
                        ShipFee = o.ShipFee,
                        NinjaOrderTotal = o.OrderTotal,
                        DiscountAmount = o.DiscountAmount,
                        BonusDiscountAmount = 0,
                        Partner = o.OrderDelivery != null ? o.OrderDelivery.DeliveryPartner?.Name : "",
                        PartnerShipFee = o.OrderDelivery != null ? o.OrderDelivery.PartnerShipFee : 0,
                        Drinks = o.OrderDetails.Select(od => new DrinkDayReportItem
                        {
                            Amount = od.Amount,
                            DrinkName = od.DrinkName,
                            DrinkSize = od.DrinkSize,
                            FullPrice = od.FullPrice,
                            IceOption = od.IceOption,
                            Quantity = od.Quantity,
                            SugarOption = od.SugarOption,
                            Toppings = od.OrderDetailToppings.Select(t => t.ToppingName).ToArray()
                        }).ToList(),
                        CustomerName = string.Join(" ", new string[] { o.LastName, o.MiddleName, o.FirstName }.Where(s => !string.IsNullOrEmpty(s))),
                        CustomerAddress = o.AddressLine,
                        CustomerDistance = o.Distance.ToString(),
                        CustomerPhone = o.PhoneNumber,
                        Note = o.EmployeeNote
                    })
                }).ToList();

            result.From = input.From;
            result.To = input.To;
            result.Page = input.Page;
            result.TotalPage = (ordersQuery.Count + input.Count - 1) / input.Count;

            return result;
        }

        [HttpGet("ByProduct")]
        public ProductReportFilterResult ByProduct([FromQuery]ProductReportFilterInput input)
        {
            if (!input.From.HasValue) input.From = DateTime.Now.Date.AddDays(-7);
            if (!input.To.HasValue) input.To = DateTime.Now.Date;
            
            var result = new ProductReportFilterResult { ProductGroups = new List<ProductCategoryGroupReport>() };
            var allCategory = _categoryRepository.Categories.ToList();
            var allToppingCategory = _categoryRepository.ToppingCategories.ToList();
            var queryProducts = _orderRepository.GetAll.Include(o => o.OrderDetails).Include("OrderDetails.OrderDetailToppings")
                                .Where(o => !o.IsDeleted && o.OrderPlaced >= input.From && o.OrderPlaced < input.To).AsNoTracking().ToList();

            var querySizeS = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "S")).ToList();
            var querySizeM = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "M")).ToList();
            var querySizeL = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "L")).ToList();

            foreach (var cat in allCategory)
            {
                var productGrp = new ProductCategoryGroupReport
                {
                    CategoryName = cat.Name,
                    Products = new List<ProductReportItem>()
                };

                var drinks = !input.IsShowDrinkOnMenu ? cat.Drinks.Where(d => d.IsActive).ToList() : cat.Drinks;
                foreach (var drink in drinks)
                {
                    var queryDSizeS = querySizeS.Where(od => od.DrinkId == drink.Id).ToList();
                    var queryDSizeM = querySizeM.Where(od => od.DrinkId == drink.Id).ToList();
                    var queryDSizeL = querySizeL.Where(od => od.DrinkId == drink.Id).ToList();

                    var productResult = new ProductReportItem
                    {
                        DrinkCode = drink.Code,
                        DrinkName = drink.Name,
                        DrinkUnit = "Ly",
                        SizeSQuantity = queryDSizeS.Sum(s => s.Quantity),
                        SizeSAmount = queryDSizeS.Sum(s => s.Amount),
                        SizeMQuantity = queryDSizeM.Sum(s => s.Quantity),
                        SizeMAmount = queryDSizeM.Sum(s => s.Amount),
                        SizeLQuantity = queryDSizeL.Sum(s => s.Quantity),
                        SizeLAmount = queryDSizeL.Sum(s => s.Amount)
                    };
                    productGrp.Products.Add(productResult);
                }
                result.ProductGroups.Add(productGrp);
            }
            
            foreach (var tcat in allToppingCategory)
            {
                var toppingGrp = new ProductCategoryGroupReport
                {
                    CategoryName = tcat.Name,
                    Products = new List<ProductReportItem>()
                };

                foreach (var topping in tcat.Toppings)
                {
                    var queryTSizeS = querySizeS.SelectMany(od => od.OrderDetailToppings.Where(odt => odt.ToppingId == topping.Id)).ToList();
                    var queryTSizeM = querySizeM.SelectMany(od => od.OrderDetailToppings.Where(odt => odt.ToppingId == topping.Id)).ToList();
                    var queryTSizeL = querySizeL.SelectMany(od => od.OrderDetailToppings.Where(odt => odt.ToppingId == topping.Id)).ToList();

                    var productResult = new ProductReportItem
                    {
                        DrinkCode = topping.Code,
                        DrinkName = topping.Name,
                        SizeSQuantity = queryTSizeS.Sum(s => s.Quantity),
                        SizeSAmount = queryTSizeS.Sum(s => s.Amount),
                        SizeMQuantity = queryTSizeM.Sum(s => s.Quantity),
                        SizeMAmount = queryTSizeM.Sum(s => s.Amount),
                        SizeLQuantity = queryTSizeL.Sum(s => s.Quantity),
                        SizeLAmount = queryTSizeL.Sum(s => s.Amount)
                    };
                    toppingGrp.Products.Add(productResult);
                }
                result.ProductGroups.Add(toppingGrp);
            }

            var ices = new List<string> { "0%", "50%", "100%" };
            var iceGrp = new ProductCategoryGroupReport
            {
                CategoryName = "Mức đá",
                Products = new List<ProductReportItem>()
            };
            foreach (var ice in ices)
            {
                var queryIceS = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "S" && s.IceOption == ice)).ToList();
                var queryIceM = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "M" && s.IceOption == ice)).ToList();
                var queryIceL = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "L" && s.IceOption == ice)).ToList();

                var iceResult = new ProductReportItem
                {
                    DrinkName = ice,
                    SizeSQuantity = queryIceS.Sum(s => s.Quantity),
                    SizeMQuantity = queryIceM.Sum(s => s.Quantity),
                    SizeLQuantity = queryIceL.Sum(s => s.Quantity)
                };
                iceGrp.Products.Add(iceResult);
            }
            result.ProductGroups.Add(iceGrp);

            var sugars = new List<string> { "Ngọt", "Vừa", "Nhạt" };
            var sugarGrp = new ProductCategoryGroupReport
            {
                CategoryName = "Mức đường",
                Products = new List<ProductReportItem>()
            };
            foreach (var sugar in sugars)
            {
                var querySugarS = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "S" && s.SugarOption == sugar)).ToList();
                var querySugarM = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "M" && s.SugarOption == sugar)).ToList();
                var querySugarL = queryProducts.SelectMany(p => p.OrderDetails.Where(s => s.DrinkSize == "L" && s.SugarOption == sugar)).ToList();

                var sugarResult = new ProductReportItem
                {
                    DrinkName = sugar,
                    SizeSQuantity = querySugarS.Sum(s => s.Quantity),
                    SizeMQuantity = querySugarM.Sum(s => s.Quantity),
                    SizeLQuantity = querySugarL.Sum(s => s.Quantity)
                };
                sugarGrp.Products.Add(sugarResult);
            }
            result.ProductGroups.Add(sugarGrp);

            result.From = input.From;
            result.To = input.To;

            return result;
        }
    }
}