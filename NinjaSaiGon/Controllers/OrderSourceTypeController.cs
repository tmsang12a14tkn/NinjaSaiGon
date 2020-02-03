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
    public class OrderSourceTypeController : Controller
    {
        private readonly IOrderSourceTypeRepository _orderSourceTypeRepository;

        public OrderSourceTypeController(IOrderSourceTypeRepository orderSourceTypeRepository)
        {
            _orderSourceTypeRepository = orderSourceTypeRepository;
        }

        public IActionResult Index()
        {
            return View(_orderSourceTypeRepository.OrderSourceTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderSourceType orderSourceType)
        {
            if (ModelState.IsValid)
            {
                _orderSourceTypeRepository.Add(orderSourceType);
                return RedirectToAction(nameof(Index));
            }
            return View(orderSourceType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderSourceType = _orderSourceTypeRepository.GetById(id.Value);
            if (orderSourceType == null)
            {
                return NotFound();
            }
            return View(orderSourceType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderSourceType orderSourceType)
        {
            if (id != orderSourceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderSourceTypeRepository.Update(orderSourceType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_orderSourceTypeRepository.Exists(orderSourceType.Id))
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
            return View(orderSourceType);
        }
    }
}