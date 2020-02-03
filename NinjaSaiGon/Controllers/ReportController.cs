using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.ReportViewModels;
using NinjaSaiGon.Data.Interfaces;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ReportController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public ReportController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult ByWeek(DateTime? toDate, int weektab = 0)
        {
            DateTime date = DateTime.Now;
            if (toDate.HasValue)
                date = toDate.Value;

            var offset = (date.DayOfWeek - DayOfWeek.Monday + 7) % 7;
            var end = date.AddDays(-offset + 7);
            var begin = end.AddDays(-35);

            var weekTabs = new List<OrderTabFilterView>();
            for (var beginWeek = begin; beginWeek < end; beginWeek = beginWeek.AddDays(7))
            {
                weekTabs.Add(new OrderTabFilterView
                {
                    Begin = beginWeek,
                    IsCurrent = date >= beginWeek && date <= beginWeek.AddDays(7)
                });
            }

            var filterView = new OrderFilterView
            {
                DateSelected = date,
                WeekTabs = weekTabs,
                Begin = begin,
                End = end
            };

            ViewBag.weektab = weektab;

            return View(filterView);
        }

        public IActionResult BySearch()
        {
            return View();
        }

        public IActionResult ByDay(DateTime? from, DateTime? to)
        {
            var dayReportFilterInput = new DayReportFilterInput();

            dayReportFilterInput.From = from.HasValue ? from.Value : DateTime.Now.Date;
            dayReportFilterInput.To = to.HasValue ? to.Value : dayReportFilterInput.From.Value.AddDays(1).AddMinutes(-1);
            
            return View(dayReportFilterInput);
        }

        public IActionResult ByProduct()
        {
            return View();
        }

    }
}
