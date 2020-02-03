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
    public class PersonLevelController : Controller
    {
        private readonly IPersonLevelRepository _personLevelRepository;

        public PersonLevelController(IPersonLevelRepository personLevelRepository)
        {
            _personLevelRepository = personLevelRepository;
        }
        
        public IActionResult Index()
        {
            return View(_personLevelRepository.PersonLevels);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PersonLevel personLevel)
        {
            if (ModelState.IsValid)
            {
                _personLevelRepository.Add(personLevel);
                return RedirectToAction(nameof(Index));
            }
            return View(personLevel);
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personLevel = _personLevelRepository.GetById(id.Value);
            if (personLevel == null)
            {
                return NotFound();
            }
            return View(personLevel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PersonLevel personLevel)
        {
            if (id != personLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _personLevelRepository.Update(personLevel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_personLevelRepository.Exists(personLevel.Id))
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
            return View(personLevel);
        }
    }
}
