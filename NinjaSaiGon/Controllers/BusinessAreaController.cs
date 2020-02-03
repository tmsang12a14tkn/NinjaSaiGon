using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Linq;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class BusinessAreaController : Controller
    {
        private readonly IBusinessAreaRepository _businessAreaRepository;

        public BusinessAreaController(IBusinessAreaRepository businessAreaRepository)
        {
            _businessAreaRepository = businessAreaRepository;
        }

        public IActionResult Index()
        {
            return View(_businessAreaRepository.BusinessAreas);
        }

        public IActionResult Create()
        {
            ViewBag.ParentAreas = _businessAreaRepository.BusinessAreas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusinessArea businessArea)
        {
            try
            {
                _businessAreaRepository.Add(businessArea);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ParentAreas = _businessAreaRepository.BusinessAreas;
                return View(businessArea);
            }
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessArea = _businessAreaRepository.GetById(id.Value);
            if (businessArea == null)
            {
                return NotFound();
            }
            ViewBag.ParentAreas = _businessAreaRepository.BusinessAreas.Where(b => b.Id != id && b.ParentId != id);
            return View(businessArea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BusinessArea businessArea)
        {
            if (id != businessArea.Id)
            {
                return NotFound();
            }

            try
            {
                _businessAreaRepository.Update(businessArea);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.ParentAreas = _businessAreaRepository.BusinessAreas.Where(b => b.Id != id && b.ParentId != id);
                return View(businessArea);
            }
        }
    }
}