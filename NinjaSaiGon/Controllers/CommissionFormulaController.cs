using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Linq;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CommissionFormulaController : Controller
    {
        private readonly ICommissionFormulaRepository _commissionFormulaRepository;

        public CommissionFormulaController(ICommissionFormulaRepository commissionFormulaRepository)
        {
            _commissionFormulaRepository = commissionFormulaRepository;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            var formulas = _commissionFormulaRepository.CommissionFormulas;
            if (startDate.HasValue && endDate.HasValue)
                formulas = formulas.Where(f => f.StartDate.Value.Date >= startDate && f.EndDate.Value.Date <= endDate);

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(formulas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommissionFormula commissionFormula)
        {
            try
            {
                _commissionFormulaRepository.Add(commissionFormula);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(commissionFormula);
            }
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commissionFormula = _commissionFormulaRepository.GetById(id.Value);
            if (commissionFormula == null)
            {
                return NotFound();
            }
            
            return View(commissionFormula);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CommissionFormula commissionFormula)
        {
            if (id != commissionFormula.Id)
            {
                return NotFound();
            }

            try
            {
                _commissionFormulaRepository.Update(commissionFormula);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(commissionFormula);
            }
        }
    }
}