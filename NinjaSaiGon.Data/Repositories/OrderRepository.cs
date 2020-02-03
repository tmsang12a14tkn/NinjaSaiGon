using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using NinjaSaiGon.Data.Models.Dto;
using System.Globalization;

namespace NinjaSaiGon.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public OrderRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool Create(Order order)
        {
            try
            {
                var orderDateIndex = _appDbContext.Orders.Count(o => o.OrderPlaced.Date == order.OrderPlaced.Date) + 1;
                order.Code = $"PDC{order.OrderPlaced:yyyyMMdd}{orderDateIndex:0000}";

                Normalize(order);

                //update private promotion
                if (!string.IsNullOrEmpty(order.PromotionCode))
                {
                    var privatePromotionCode = _appDbContext.PrivatePromotionCodes.FirstOrDefault(c => c.Code == order.PromotionCode);
                    if (privatePromotionCode != null)
                    {
                        privatePromotionCode.CurrentUseCode++;
                    }
                }

                _appDbContext.Orders.Add(order);
                _appDbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public Order GetById(int id)
        {
            return _appDbContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.OrderDetailToppings).Include(o => o.OrderDelivery).FirstOrDefault(o => o.OrderId == id);
        }

        public IEnumerable<OrderDetail> GetDetailsByOrderId(int id)
        {
            return _appDbContext.OrderDetails.Where(o => o.OrderId == id);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            order.IsDeleted = true;

            //update private promotion
            if (!string.IsNullOrEmpty(order.PromotionCode))
            {
                var privatePromotionCode = _appDbContext.PrivatePromotionCodes.FirstOrDefault(c => c.Code == order.PromotionCode);
                if (privatePromotionCode != null)
                {
                    privatePromotionCode.CurrentUseCode--;
                }
            }

            _appDbContext.SaveChanges();
        }

        public bool Edit(Order order)
        {
            Normalize(order);
            _appDbContext.Entry(order).State = EntityState.Modified;
            order.OrderDetails?.RemoveAll(od => od.IsDeleted && od.OrderDetailId < 0);
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.IsDeleted)
                {
                    _appDbContext.Entry(orderDetail).State = EntityState.Deleted;
                }
                else if (orderDetail.OrderDetailId <= 0)
                {
                    _appDbContext.Entry(orderDetail).State = EntityState.Added;
                }
                else
                    _appDbContext.Entry(orderDetail).State = EntityState.Modified;
            }
            try
            {
                _appDbContext.Update(order);

                if (order.OrderDelivery != null)
                {
                    if (!_appDbContext.OrderDeliveries.Any(od => od.OrderId == order.OrderId))
                    {
                        _appDbContext.Entry(order.OrderDelivery).State = EntityState.Added;
                        _appDbContext.Add(order.OrderDelivery);
                    }
                    else
                    {
                        _appDbContext.Entry(order.OrderDelivery).State = EntityState.Modified;
                        _appDbContext.Update(order.OrderDelivery);
                    }
                }

                if (order.OrderCustomerType == OrderCustomerType.Customer)
                {
                    var customer = _appDbContext.Persons.Include(p => p.ContactInfo).Include("ContactInfo.Phones")
                                                        .Include("ContactInfo.Addresses").FirstOrDefault(p => p.Id == order.CustomerId);
                    if (customer != null)
                    {
                        if (!customer.ContactInfo.Phones.Any(p => p.PhoneNumber == order.PhoneNumber))
                        {
                            var newPhone = new ContactPhone
                            {
                                PhoneNumber = order.PhoneNumber,
                                TypeId = 1,
                                ContactInfoId = order.CustomerId.Value
                            };
                            _appDbContext.Entry(newPhone).State = EntityState.Added;
                            _appDbContext.ContactPhones.Add(newPhone);
                        }

                        if (!customer.ContactInfo.Addresses.Any(p => p.MoreInfo == order.AddressLine))
                        {
                            var newAddress = new ContactAddress
                            {
                                ContactInfoId = order.CustomerId.Value,
                                MoreInfo = order.AddressLine
                            };
                            _appDbContext.Entry(newAddress).State = EntityState.Added;
                            _appDbContext.ContactAddresses.Add(newAddress);
                        }
                    }
                }
                else if (order.OrderCustomerType == OrderCustomerType.Agency)
                {
                    var agency = _appDbContext.Agencies.Include(p => p.AgencyContactInfo).Include("AgencyContactInfo.Phones")
                                                        .Include("AgencyContactInfo.Addresses").FirstOrDefault(p => p.Id == order.AgencyId);
                    if (agency != null)
                    {
                        if (!agency.AgencyContactInfo.Phones.Any(p => p.PhoneNumber == order.PhoneNumber))
                        {
                            var newPhone = new AgencyContactPhone
                            {
                                PhoneNumber = order.PhoneNumber,
                                TypeId = 1,
                                AgencyContactInfoId = order.AgencyId.Value
                            };
                            _appDbContext.Entry(newPhone).State = EntityState.Added;
                            _appDbContext.AgencyContactPhones.Add(newPhone);
                        }

                        if (!agency.AgencyContactInfo.Addresses.Any(p => p.MoreInfo == order.AddressLine))
                        {
                            var newAddress = new AgencyContactAddress
                            {
                                AgencyContactInfoId = order.AgencyId.Value,
                                MoreInfo = order.AddressLine
                            };
                            _appDbContext.Entry(newAddress).State = EntityState.Added;
                            _appDbContext.AgencyContactAddresses.Add(newAddress);
                        }                            
                    }
                }

                _appDbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(int orderId)
        {
            return _appDbContext.Orders.Any(e => e.OrderId == orderId);
        }

        public Order Normalize(Order order)
        {
            if (order.OrderDetails != null)
            {
                var isSameCode = false;
                var oldOrder = _appDbContext.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (oldOrder != null)
                {
                    _appDbContext.Entry(oldOrder).State = EntityState.Detached;
                    if (!string.IsNullOrEmpty(oldOrder.PromotionCode) && string.IsNullOrEmpty(order.PromotionCode))
                    {
                        order.PromotionCode = oldOrder.PromotionCode;
                        isSameCode = true;
                    }
                    else if (!string.IsNullOrEmpty(oldOrder.PromotionCode) && !string.IsNullOrEmpty(order.PromotionCode) && oldOrder.PromotionCode == order.PromotionCode)
                        isSameCode = true;
                }

                if (!string.IsNullOrEmpty(order.PromotionCode))
                    order.PromotionCode = order.PromotionCode.Trim();

                var orderDetails = order.OrderDetails.Where(od => !od.IsDeleted && !od.IsFreeDrink).ToList();
                var arrDrinkPrice = new List<int>();

                orderDetails.ForEach(od =>
                {
                    var drink = _appDbContext.Drinks.Find(od.DrinkId);
                    if (drink == null)
                    {
                        throw new ArgumentException("Drink is invalid");
                    }

                    var drinkSize = _appDbContext.DrinkSizes.Find(od.DrinkSizeId);
                    if (drinkSize == null || drinkSize.DrinkId != od.DrinkId)
                    {
                        throw new ArgumentException("DrinkSize is invalid");
                    }
                    od.DrinkSize = drinkSize.Name;
                    od.Price = drinkSize.Price;
                    od.IsNewDrink = drink.IsNew;
                    od.DrinkName = drink.Name;
                    if (od.OrderDetailToppings != null)
                    {
                        foreach (var orderDetailTopping in od.OrderDetailToppings)
                        {
                            var topping = _appDbContext.Toppings.Find(orderDetailTopping.ToppingId);
                            if (topping == null)
                            {
                                throw new ArgumentException("Topping is invalid");
                            }
                            orderDetailTopping.Price = topping.Price;
                            orderDetailTopping.ToppingName = topping.Name;
                            orderDetailTopping.Amount = topping.Price * orderDetailTopping.Quantity;
                        }
                    }

                    //tạm tính giá
                    od.FullPrice = od.Price + (od.OrderDetailToppings?.Sum(t => t.Price * t.Quantity) ?? 0);
                    od.Amount = od.FullPrice * od.Quantity;

                    for (var i = 0; i < od.Quantity; i++)
                    {
                        arrDrinkPrice.Add(od.FullPrice);
                    }
                });
                //tạm tính tổng
                order.OrderTotal = orderDetails.Sum(d => d.Amount);

                //update free drinks
                var freeDrinks = order.OrderDetails.Where(od => !od.IsDeleted && od.IsFreeDrink).ToList();
                freeDrinks.ForEach(od =>
                {
                    var drink = _appDbContext.Drinks.Find(od.DrinkId);
                    if (drink == null)
                    {
                        throw new ArgumentException("Drink is invalid");
                    }

                    var drinkSize = _appDbContext.DrinkSizes.Find(od.DrinkSizeId);
                    if (drinkSize == null || drinkSize.DrinkId != od.DrinkId)
                    {
                        throw new ArgumentException("DrinkSize is invalid");
                    }
                    od.DrinkSize = drinkSize.Name;
                    od.Price = 0;
                    od.DrinkName = drink.Name;
                    od.FullPrice = 0;
                    od.IsNewDrink = drink.IsNew;
                    od.Amount = 0;

                    if (od.OrderDetailToppings != null)
                    {
                        foreach (var orderDetailTopping in od.OrderDetailToppings)
                        {
                            var topping = _appDbContext.Toppings.Find(orderDetailTopping.ToppingId);
                            if (topping == null)
                            {
                                throw new ArgumentException("Topping is invalid");
                            }
                            orderDetailTopping.Price = 0;
                            orderDetailTopping.ToppingName = topping.Name;
                            orderDetailTopping.Amount = 0;
                        }
                    }
                });

                //apply Promotion Code
                var checkPromotionResult = CheckPromotionCode(order.PromotionCode, order.OrderPlaced, isSameCode);
                var cntItem = orderDetails.Sum(od => od.Quantity);
                if (order.IsAutoDiscount)
                {
                    //Double-check free drinks buy1get1
                    if (order.IsSpecialPromo || (checkPromotionResult.Success && checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1) || CheckAutoBuy1Get1(order.OrderPlaced))
                    {
                        var chargeItem = cntItem < 2 ? cntItem : cntItem % 2 == 0 ? cntItem / 2 : (cntItem + 1) / 2;
                        var chargeAmount = arrDrinkPrice.OrderByDescending(o => o).Take(chargeItem).Sum();
                        order.DiscountAmount = order.OrderTotal - chargeAmount;
                        order.FreeDrinkCount = cntItem / 2;
                    }
                    else
                    {
                        if (checkPromotionResult.Success)
                        {
                            if (checkPromotionResult.ApplyRule == ApplyRule.DiscountOnTotal)
                            {
                                int promotionDiscountValue;
                                if (checkPromotionResult.DiscountType == DiscountType.Percent)
                                {
                                    promotionDiscountValue = order.OrderTotal * checkPromotionResult.DiscountValue / 100;
                                }
                                else
                                {
                                    promotionDiscountValue = checkPromotionResult.DiscountValue;
                                }

                                order.DiscountAmount = promotionDiscountValue;
                                order.OrderTotal -= promotionDiscountValue;
                                if (order.OrderTotal < 0) order.OrderTotal = 0;
                                order.PromotionDiscountType = checkPromotionResult.DiscountType;
                                order.PromotionDiscountValue = promotionDiscountValue;
                            }
                            else if (checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1NewDrink)
                            {
                                var newDrinkCount = orderDetails.Where(d => d.IsNewDrink).Sum(od => od.Quantity);
                                var freeDrinkCount = Math.Min(cntItem / 2, newDrinkCount);
                                var chargeDrinkCount = cntItem - freeDrinkCount;

                                var chargeAmount = arrDrinkPrice.OrderByDescending(o => o).Take(chargeDrinkCount).Sum();
                                order.DiscountAmount = order.OrderTotal - chargeAmount;
                                order.FreeDrinkCount = freeDrinkCount;
                            }
                            else if (checkPromotionResult.ApplyRule == ApplyRule.FreeUpSizeAnd1Topping)
                            {
                                int promotionDiscountValue = 0;
                                foreach (var orderDetail in order.OrderDetails)
                                {
                                    var drinkSizes = _appDbContext.DrinkSizes.Where(dz => dz.DrinkId == orderDetail.DrinkId).OrderBy(dz => dz.Price);
                                    var cheapestSize = drinkSizes.FirstOrDefault();
                                    var mostExpensiveSize = drinkSizes.LastOrDefault();
                                    orderDetail.DrinkSize = mostExpensiveSize.Name;
                                    orderDetail.DrinkSizeId = mostExpensiveSize.Id;
                                    orderDetail.Price = mostExpensiveSize.Price;

                                    promotionDiscountValue += (mostExpensiveSize.Price - cheapestSize.Price) * orderDetail.Quantity;

                                    var cheapestTopping = orderDetail.OrderDetailToppings?.OrderBy(t => t.Price).FirstOrDefault();
                                    if (cheapestTopping != null)
                                    {
                                        promotionDiscountValue += cheapestTopping.Price * orderDetail.Quantity;
                                    }
                                }
                                order.DiscountAmount = promotionDiscountValue;
                                order.DiscountDrinkCount = cntItem;
                            }
                            else if (checkPromotionResult.ApplyRule == ApplyRule.FreeDrink) //tặng món
                            {
                                var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(checkPromotionResult.PromotionId);
                                if (promotionDrinkSetting != null)
                                {
                                    if (promotionDrinkSetting.PromotionGiftType == 1) //kiểu tặng món: tiền phát sinh
                                    {
                                        //kiem tra cac dieu kien
                                        if ((!promotionDrinkSetting.Condition_MinMoney || promotionDrinkSetting.Condition_MinMoneyValue <= order.OrderTotal)
                                            && (!promotionDrinkSetting.Condition_MinDrink || promotionDrinkSetting.Condition_MinDrinkValue <= cntItem))
                                        {
                                            //chỗ này hơi sai sai - khi repeat mà có cả money và số ly
                                            var repeatCount = promotionDrinkSetting.ApplyOneTimeOrRepeat ? 1 : order.OrderTotal / promotionDrinkSetting.Condition_MinMoneyValue;
                                            //tổng số ly được tặng
                                            //var totalFreeCount = repeatCount * promotionDrinkSetting.PromotionDrinkQuantity;

                                            //tìm những món sẽ được chọn để khuyến mãi
                                            var allPromotionDrinks = _appDbContext.PromotionDrinks.Include(pd => pd.Drink)
                                                   .Include(pd => pd.PromotionDrinkToppings).ThenInclude(t => t.Topping)
                                                   .Where(pd => pd.PromotionId == checkPromotionResult.PromotionId).ToList();

                                            if (allPromotionDrinks.Count == 0)
                                                throw new ApplicationException("Không có món khuyến mại");

                                            //số món đã được KM
                                            var existFreeDrinkCount = order.OrderDetails.Where(od => !od.IsDeleted && od.IsFreeDrink).Sum(od => od.Quantity);
                                            //số món sẽ được KM theo tính toán
                                            var compFreeDrinkCount = repeatCount * (promotionDrinkSetting.PromotionForm ? 1 : allPromotionDrinks.Count);
                                            if (existFreeDrinkCount != compFreeDrinkCount)
                                            {
                                                order.OrderDetails.RemoveAll(od => od.IsFreeDrink);
                                                for (var i = 0; i < repeatCount; i++)
                                                {
                                                    //tim mon dc khuyen mai
                                                    var selectedPromotionDrinks = new List<PromotionDrink>();
                                                    if (promotionDrinkSetting.PromotionForm)
                                                    {
                                                        if (promotionDrinkSetting.PromotionDrinkRandom)
                                                        {
                                                            var random = new Random((int)DateTime.Now.Ticks);
                                                            selectedPromotionDrinks.Add(allPromotionDrinks[random.Next(allPromotionDrinks.Count)]);
                                                        }
                                                        else
                                                            selectedPromotionDrinks.Add(allPromotionDrinks[i % allPromotionDrinks.Count]);
                                                    }
                                                    else
                                                        selectedPromotionDrinks.AddRange(allPromotionDrinks);
                                                    foreach (var promotionDrink in selectedPromotionDrinks)
                                                    {
                                                        if (promotionDrink != null)
                                                        {
                                                            var promotionDrinkQuantity = 1;

                                                            var existPromotionDrink = order.OrderDetails.FirstOrDefault(od => od.IsFreeDrink && od.DrinkId == promotionDrink.DrinkId);
                                                            if (existPromotionDrink == null)
                                                            {
                                                                var drink = _appDbContext.Drinks.Include(d => d.IceOptions).Include(d => d.SugarOptions).FirstOrDefault(d => d.Id == promotionDrink.DrinkId);
                                                                if (drink != null)
                                                                    order.OrderDetails.Add(new OrderDetail()
                                                                    {
                                                                        Price = 0,
                                                                        FullPrice = 0,
                                                                        Amount = 0,
                                                                        DrinkId = promotionDrink.DrinkId,
                                                                        DrinkName = promotionDrink.Drink.Name,
                                                                        DrinkSizeId = promotionDrink.DrinkSizeId,
                                                                        DrinkSize = promotionDrink.DrinkSizeName,
                                                                        IsNewDrink = promotionDrink.Drink.IsNew,
                                                                        Quantity = promotionDrinkQuantity,
                                                                        OrderDetailToppings = promotionDrink
                                                                            .PromotionDrinkToppings?.Select(t =>
                                                                                new OrderDetailTopping
                                                                                {
                                                                                    Quantity = t.Quantity,
                                                                                    Amount = 0,
                                                                                    Price = 0,
                                                                                    ToppingId = t.ToppingId,
                                                                                    ToppingName = t.Topping.Name
                                                                                }).ToList(),
                                                                        IsFreeDrink = true,
                                                                        IceOption = drink.IceOptions
                                                                            .FirstOrDefault(o => o.IsPrimary)?.Name,
                                                                        SugarOption = drink.SugarOptions
                                                                            .FirstOrDefault(o => o.IsPrimary)?.Name
                                                                    });
                                                            }
                                                            else
                                                            {
                                                                existPromotionDrink.Quantity += promotionDrinkQuantity;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                order.FreeDrinkCount = order.OrderDetails.Where(od => !od.IsDeleted && od.IsFreeDrink).Sum(od => od.Quantity);
                            }
                            else if (checkPromotionResult.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                            {
                                var conditionQuantity = checkPromotionResult.BuyQuantity + checkPromotionResult.GiveQuantity;
                                var modQuantity = cntItem % conditionQuantity;
                                var divQuantity = (cntItem - modQuantity) / conditionQuantity;

                                var chargeItem = cntItem <= checkPromotionResult.BuyQuantity ? cntItem : modQuantity == 0 ? divQuantity * checkPromotionResult.BuyQuantity : modQuantity <= checkPromotionResult.BuyQuantity ? checkPromotionResult.BuyQuantity * divQuantity + modQuantity : checkPromotionResult.BuyQuantity * divQuantity + checkPromotionResult.BuyQuantity;
                                var chargeAmout = arrDrinkPrice.OrderByDescending(o => o).Take(chargeItem).Sum();
                                order.DiscountAmount = order.OrderTotal - chargeAmout;
                                order.FreeDrinkCount = divQuantity * checkPromotionResult.GiveQuantity;
                            }
                        }
                        else
                            order.DiscountAmount = 0;
                    }
                }

                //tính lại giá + tổng
                orderDetails.ForEach(od =>
                {
                    od.FullPrice = od.Price + (od.OrderDetailToppings?.Sum(t => t.Amount) ?? 0);
                    od.Amount = od.FullPrice * od.Quantity;
                });
                order.OrderTotal = orderDetails.Sum(d => d.Amount);
                order.OrderTotal -= order.DiscountAmount;

                if (order.IsAutoShipFee)
                {
                    //Double-check fee ship
                    double checkFeeShip = 0;
                    if (order.Distance > 1000)
                    {
                        var dDistance = (double)order.Distance / 1000;
                        var multiply = Math.Round(dDistance, 1);
                        checkFeeShip = multiply * 5000;
                    }
                    if (order.IsSpecialPromo || (checkPromotionResult.Success && checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1) || CheckAutoBuy1Get1(order.OrderPlaced))
                    {
                        var chargeItem = cntItem <= 2 ? cntItem : cntItem % 2 == 0 ? cntItem / 2 : (cntItem + 1) / 2;

                        var tempCharge = checkFeeShip - ((chargeItem > 0 ? chargeItem - 1 : 0) * 5000);
                        var tempShip = cntItem > 2 ? tempCharge > 0 ? tempCharge : 0 : checkFeeShip;
                        order.ShipFee = order.OrderTotal < 150000 ? Convert.ToInt32(tempShip) : 0;
                    }
                    else if (order.IsSpecialPromo || (checkPromotionResult.Success && checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1NewDrink))
                    {
                        var newDrinkCount = orderDetails.Where(d => d.IsNewDrink).Sum(od => od.Quantity);
                        var freeDrinkCount = Math.Min(cntItem / 2, newDrinkCount);
                        var chargeDrinkCount = cntItem - freeDrinkCount;

                        var tempCharge = checkFeeShip - ((chargeDrinkCount > 0 ? chargeDrinkCount - 1 : 0) * 5000);
                        var tempShip = cntItem > 2 ? (tempCharge > 0 ? tempCharge : 0) : checkFeeShip;
                        order.ShipFee = order.OrderTotal < 150000 ? Convert.ToInt32(tempShip) : 0;
                    }
                    else if (order.IsSpecialPromo || (checkPromotionResult.Success && checkPromotionResult.ApplyRule == ApplyRule.BuyMGetNLowPrice))
                    {
                        var conditionQuantity = checkPromotionResult.BuyQuantity + checkPromotionResult.GiveQuantity;
                        var modQuantity = cntItem % conditionQuantity;
                        var divQuantity = (cntItem - modQuantity) / conditionQuantity;

                        var chargeItem = cntItem <= checkPromotionResult.BuyQuantity ? cntItem : modQuantity == 0 ? divQuantity * checkPromotionResult.BuyQuantity : modQuantity <= checkPromotionResult.BuyQuantity ? checkPromotionResult.BuyQuantity * divQuantity + modQuantity : checkPromotionResult.BuyQuantity * divQuantity + checkPromotionResult.BuyQuantity;
                        chargeItem = chargeItem >= 2 ? chargeItem - 1 : 0;
                        var tempCharge = checkFeeShip - (chargeItem * 5000);
                        var tempShip = cntItem > 2 ? tempCharge > 0 ? tempCharge : 0 : checkFeeShip;
                        order.ShipFee = order.OrderTotal < 150000 ? Convert.ToInt32(tempShip) : 0;
                    }
                    else
                    {
                        var tempCharge = checkFeeShip - ((cntItem - 1) * 5000);
                        var tempShip = cntItem > 1 ? tempCharge > 0 ? tempCharge : 0 : checkFeeShip;
                        order.ShipFee = order.OrderTotal < 150000 ? Convert.ToInt32(tempShip) : 0;
                    }
                }
                order.OrderTotal += order.ShipFee;

                order.OrderTotal += order.SurchargeAmount;
            }

            return order;
        }

        public List<string> GetHtml(int id)
        {
            if (!Exists(id))
            {
                return null;
            }
            else
            {
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
                var order = GetById(id);
                var result = new List<string>();
                var maxLine = 6;
                var cntItem = 1;

                var headHtml = $"<div style=\"font-size:13px;height:135px;width:100%;margin-bottom:16px;\">Khách đặt: " + String.Join(" ", new string[] { order.LastName, order.MiddleName, order.FirstName }.Where(o => !string.IsNullOrEmpty(o))) + "<br />";
                foreach (var od in order.OrderDetails)
                {
                    for (var qtt = 0; qtt < od.Quantity; qtt++)
                    {
                        var divStr = new List<string>();
                        var subHtml = headHtml;
                        subHtml += $"<span style=\"font-size:12.5px\">" + order.Code + "</span>";
                        subHtml += $"<span style=\"font-size:13px;float:right;margin-right:10px;\">" + cntItem + "/" + order.OrderDetails.Sum(o => o.Quantity) + "</span><br />";
                        subHtml += $"<hr style=\"margin: 1px 0 1px 0;\">";

                        var drink = _appDbContext.Drinks.Find(od.DrinkId);
                        subHtml += $"<b style=\"font-size:12.5px;line-height:15px;\">" + od.DrinkName + "/" + drink.EnglishName + "</b><br />";
                        var listDrinkName = WrapText(od.DrinkName, 32);
                        var remainLine = maxLine - listDrinkName.Count;

                        if (!string.IsNullOrEmpty(od.Note))
                        {
                            var listNote = WrapText(od.Note, 32);
                            if (listNote.Count <= remainLine)
                                subHtml += $"<span style=\"font-size:13px;line-height:15px;\">" + od.Note + "</span><br />";
                            else
                            {
                                for (var i = 0; i < listNote.Count; i++)
                                {
                                    var subNote = listNote[i];
                                    if (i == 0)
                                        subHtml += $"<span style=\"font-size:13px;line-height:15px;\">" + subNote;
                                    else
                                        subHtml += subNote;

                                    if (i == remainLine - 1)
                                    {
                                        subHtml += $"</div>";
                                        divStr.Add(subHtml);
                                        subHtml = $"<div style=\"font-size:13px;height:135px;width:100%;margin-bottom:16px;\"><span style=\"font-size:13px;line-height:15px;\">";
                                    }
                                }
                                subHtml += $"</span><br />";
                            }
                            remainLine = remainLine - listNote.Count;
                        }

                        if (remainLine == 0)
                        {
                            subHtml += $"</div>";
                            divStr.Add(subHtml);
                            subHtml = $"<div style=\"font-size:13px;height:135px;width:100%;margin-bottom:15px;\">";
                            remainLine = maxLine;
                        }
                        else if (remainLine < 0)
                        {
                            remainLine = maxLine + remainLine;
                        }

                        var optionStr = $"Size " + od.DrinkSize + ", Đá " + od.IceOption + ", Đường " + od.SugarOption;
                        if (od.OrderDetailToppings != null)
                        {
                            for (var i = 0; i < od.OrderDetailToppings.Count; i++)
                            {
                                var topping = od.OrderDetailToppings.ElementAt(i);
                                optionStr += $", " + topping.ToppingName + " x" + topping.Quantity;
                            }
                        }

                        var listOption = WrapText(optionStr, 32);
                        if (listOption.Count <= remainLine)
                            subHtml += $"<span style=\"font-size:13px;line-height:15px;\">" + optionStr + "</span><br />";
                        else
                        {
                            for (var i = 0; i < listOption.Count; i++)
                            {
                                var subOption = listOption[i];
                                if (i == 0)
                                    subHtml += $"<span style=\"font-size:13px;line-height:15px;\">" + subOption;
                                else
                                    subHtml += subOption;

                                if (i == remainLine - 1)
                                {
                                    subHtml += $"</div>";
                                    divStr.Add(subHtml);
                                    subHtml = $"<div style=\"font-size:13px;height:135px;width:100%;margin-bottom:16px;\"><span style=\"font-size:13px;line-height:15px;\">";
                                }
                            }
                            subHtml += $"</span><br />";
                        }
                        remainLine = remainLine - listOption.Count;

                        if (remainLine == 0)
                        {
                            subHtml += $"</div>";
                            divStr.Add(subHtml);
                            subHtml = $"<div style=\"font-size:13px;height:135px;width:100%;margin-bottom:16px;\">";
                        }

                        subHtml += $"<b style=\"font-size:13px;float:right;margin-right:15px;\">" + od.FullPrice.ToString("#,###", cul.NumberFormat) + "</b></div>";
                        divStr.Add(subHtml);
                        cntItem++;

                        divStr.Reverse();
                        result.AddRange(divStr);
                    }
                }

                return result;
            }
        }

        public Order UpdatePromotionFreeDrinkCount(Order order)
        {
            if (order.OrderDetails != null)
            {
                var orderDetails = order.OrderDetails.Where(od => od.IsDeleted == false).ToList();

                //apply Promotion Code
                var checkPromotionResult = CheckPromotionCode(order.PromotionCode, order.OrderPlaced, false);
                var cntItem = orderDetails.Sum(od => od.Quantity);

                if (order.IsAutoDiscount)
                {
                    if (checkPromotionResult.Success)
                    {
                        if (checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1)
                        {
                            order.FreeDrinkCount = cntItem / 2;
                        }
                        if (checkPromotionResult.ApplyRule == ApplyRule.Buy1Get1NewDrink)
                        {
                            var newDrinkCount = orderDetails.Where(d => d.IsNewDrink).Sum(od => od.Quantity);
                            var freeDrinkCount = Math.Min(cntItem / 2, newDrinkCount);
                            order.FreeDrinkCount = freeDrinkCount;
                        }
                        else if (checkPromotionResult.ApplyRule == ApplyRule.DiscountOnTotal)
                        {

                        }
                        else if (checkPromotionResult.ApplyRule == ApplyRule.FreeUpSizeAnd1Topping)
                        {
                            order.DiscountDrinkCount = cntItem;
                        }
                        else if (checkPromotionResult.ApplyRule == ApplyRule.FreeDrink) //tặng món
                        {
                            order.FreeDrinkCount = order.OrderDetails.Where(od => od.IsDeleted == false && od.IsFreeDrink).Sum(od => od.Quantity);
                        }
                        else if (checkPromotionResult.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                        {
                            var conditionQuantity = checkPromotionResult.BuyQuantity + checkPromotionResult.GiveQuantity;
                            var divQuantity = cntItem / conditionQuantity;
                            order.FreeDrinkCount = divQuantity * checkPromotionResult.GiveQuantity;
                        }
                    }
                }

                order.BaseDrinkCount = orderDetails.Sum(od => od.Quantity) - order.DiscountDrinkCount - order.FreeDrinkCount;
            }
            _appDbContext.SaveChanges();
            return order;
        }
        IQueryable<Order> IOrderRepository.GetAll => _appDbContext.Orders;
        IQueryable<Order> IOrderRepository.All => _appDbContext.Orders.Where(o => !o.IsDeleted).OrderBy(o => o.OrderPlaced).Include(o => o.OrderDetails);

        IQueryable<Order> IOrderRepository.List => _appDbContext.Orders.Where(o => !o.IsDeleted).OrderByDescending(o => o.OrderId).Include(o => o.OrderDetails);

        public IEnumerable<Order> TodayList
        {
            get
            {
                var today = DateTime.Now.Date;
                return _appDbContext.Orders.Where(o => !o.IsDeleted && o.OrderPlaced.Date == today).OrderByDescending(o => o.OrderId).Include(o => o.OrderDetails);
            }
        }

        public CheckPromotionResult CheckPromotionCode(string code, DateTime orderPlaced, bool isSameCode)
        {
            var promotion = GetPromotionByCode(code);
            var success = true;
            if (promotion != null)
            {
                //check promotion valid
                if (promotion.ApplyTimeType == ApplyTimeType.Hours)
                {
                    if (promotion.ApplyHours == null || promotion.ApplyHours.All(h => h.From > orderPlaced.TimeOfDay || h.To < orderPlaced.TimeOfDay))
                    {
                        success = false;
                    }
                }
                else if (promotion.ApplyTimeType == ApplyTimeType.DayOfWeeks)
                {
                    if (!promotion.ApplyDayOfWeek.HasValue || !promotion.ApplyDayOfWeek.Value.ToString().Contains(orderPlaced.DayOfWeek.ToString()))
                        success = false;
                }
                else //days
                {
                    if (orderPlaced.Date < promotion.ApplyFromDate || orderPlaced.Date > promotion.ApplyToDate)
                    {
                        success = false;
                    }
                }

                if (!success)
                {
                    return new CheckPromotionResult
                    {
                        Success = false
                    };
                }

                var buyQuantity = 0;
                var giveQuantity = 0;
                if (promotion.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                {
                    var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(promotion.Id);
                    if (promotionDrinkSetting != null)
                    {
                        buyQuantity = promotionDrinkSetting.PromotionDrinkBuyQuantity;
                        giveQuantity = promotionDrinkSetting.PromotionDrinkGiveQuantity;
                    }
                    else
                    {
                        return new CheckPromotionResult
                        {
                            Success = false
                        };
                    }
                }

                return new CheckPromotionResult
                {
                    Success = true,
                    DiscountType = promotion.DiscountType,
                    DiscountValue = promotion.DiscountValue,
                    ApplyRule = promotion.ApplyRule,
                    PromotionId = promotion.Id,
                    BuyQuantity = buyQuantity,
                    GiveQuantity = giveQuantity
                };
            }

            //Private Promotion Code
            var privatePromotionCode = GetPrivatePromotionByCode(code);
            if (privatePromotionCode != null && ((privatePromotionCode.Status && !isSameCode) || isSameCode))
            {
                var privatePromotion = _appDbContext.PrivatePromotions.Find(privatePromotionCode.PrivatePromotionId);
                if (privatePromotion != null && ((privatePromotion.IsActive && !isSameCode) || isSameCode))
                {
                    //check promotion valid
                    if (!isSameCode)
                    {
                        if (!privatePromotionCode.IsInfinityTime &&
                            ((privatePromotionCode.StartDate.HasValue && orderPlaced.Date < privatePromotionCode.StartDate)
                                || (privatePromotionCode.EndDate.HasValue && orderPlaced.Date > privatePromotionCode.EndDate)))
                        {
                            success = false;
                        }
                        else if (!privatePromotionCode.IsInfinityUse && privatePromotionCode.CurrentUseCode >= privatePromotionCode.MaxUseCode)
                        {
                            success = false;
                        }
                    }

                    if (!success)
                        return new CheckPromotionResult
                        {
                            Success = false
                        };

                    var promotionDrinkRandom = false;
                    if (privatePromotion.ApplyRule == ApplyRule.FreeDrink)
                    {
                        var promotionDrinkSetting = _appDbContext.PrivatePromotionDrinkSettings.Find(privatePromotion.Id);
                        if (promotionDrinkSetting != null)
                        {
                            promotionDrinkRandom = promotionDrinkSetting.PromotionDrinkRandom;
                        }
                    }
                    var buyQuantity = 0;
                    var giveQuantity = 0;
                    if (privatePromotion.ApplyRule == ApplyRule.BuyMGetNLowPrice)
                    {
                        var promotionDrinkSetting = _appDbContext.PromotionDrinkSettings.Find(privatePromotion.Id);
                        if (promotionDrinkSetting != null)
                        {
                            buyQuantity = promotionDrinkSetting.PromotionDrinkBuyQuantity;
                            giveQuantity = promotionDrinkSetting.PromotionDrinkGiveQuantity;
                        }
                        else
                        {
                            return new CheckPromotionResult
                            {
                                Success = false
                            };
                        }
                    }
                    return new CheckPromotionResult
                    {
                        Success = true,
                        DiscountType = privatePromotion.DiscountType,
                        DiscountValue = privatePromotion.DiscountValue,
                        ApplyRule = privatePromotion.ApplyRule,
                        PromotionId = privatePromotion.Id,
                        PromotionDrinkRandom = promotionDrinkRandom,
                        BuyQuantity = buyQuantity,
                        GiveQuantity = giveQuantity,
                        IsPrivatePromotion = true
                    };
                }
            }

            return new CheckPromotionResult
            {
                Success = false
            };
        }

        private bool CheckAutoBuy1Get1(DateTime orderPlaced)
        {
            var promotion = _appDbContext.Promotions.FirstOrDefault(p => p.ApplyRule == ApplyRule.Buy1Get1 && orderPlaced.Date >= p.ApplyFromDate && orderPlaced.Date <= p.ApplyToDate && p.AutoPromotion);
            return promotion != null;
        }

        private Promotion GetPromotionByCode(string code) => _appDbContext.Promotions.Include(p => p.ApplyHours).FirstOrDefault(p => p.PromotionCode == code);
        private PrivatePromotionCode GetPrivatePromotionByCode(string code) => _appDbContext.PrivatePromotionCodes.FirstOrDefault(p => p.Code == code);

        public IEnumerable<Order> Query(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return _appDbContext.Orders.Where(o => o.Code.Contains(searchBy)).ToList();
        }

        private List<string> WrapText(string text, int partLength = 33)
        {
            string[] words = text.Split(' ');
            var parts = new List<string>();
            string part = string.Empty;

            foreach (var word in words)
            {
                if (part.Length + word.Length < partLength)
                {
                    part += word + " ";
                }
                else
                {
                    parts.Add(part);
                    part = word + " ";
                }
            }
            parts.Add(part);

            return parts;
        }
    }
}
