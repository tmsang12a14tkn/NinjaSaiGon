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
    public class DeliveryPartnerController : Controller
    {
        private readonly IDeliveryPartnerRepository _deliveryPartnerRepository;

        public DeliveryPartnerController(IDeliveryPartnerRepository deliveryPartnerRepository)
        {
            _deliveryPartnerRepository = deliveryPartnerRepository;
        }

        public IActionResult Index()
        {
            return View(_deliveryPartnerRepository.DeliveryPartners);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeliveryPartner deliveryPartner)
        {
            if (ModelState.IsValid)
            {
                _deliveryPartnerRepository.Add(deliveryPartner);
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryPartner);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPartner = _deliveryPartnerRepository.GetById(id.Value);
            if (deliveryPartner == null)
            {
                return NotFound();
            }
            return View(deliveryPartner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DeliveryPartner deliveryPartner)
        {
            if (id != deliveryPartner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _deliveryPartnerRepository.Update(deliveryPartner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_deliveryPartnerRepository.Exists(deliveryPartner.Id))
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
            return View(deliveryPartner);
        }
    }
}