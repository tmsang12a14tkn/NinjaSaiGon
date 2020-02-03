using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class OrderSourceController : Controller
    {
        private readonly IOrderSourceTypeRepository _orderSourceTypeRepository;
        private readonly IOrderSourceRepository _orderSourceRepository;

        public OrderSourceController(IOrderSourceTypeRepository orderSourceTypeRepository, IOrderSourceRepository orderSourceRepository)
        {
            _orderSourceTypeRepository = orderSourceTypeRepository;
            _orderSourceRepository = orderSourceRepository;
        }

        public IActionResult Index()
        {
            return View(_orderSourceRepository.OrderSources);
        }

        public IActionResult Create()
        {
            ViewBag.SourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderSource orderSource)
        {
            if (ModelState.IsValid)
            {
                _orderSourceRepository.Add(orderSource);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            return View(orderSource);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSource = _orderSourceRepository.GetById(id.Value);
            if (orderSource == null)
            {
                return NotFound();
            }

            ViewBag.SourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            return View(orderSource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderSource orderSource)
        {
            if (id != orderSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderSourceRepository.Update(orderSource);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_orderSourceRepository.Exists(orderSource.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.SourceTypes = _orderSourceTypeRepository.OrderSourceTypes.ToList();
            return View(orderSource);
        }
    }
}