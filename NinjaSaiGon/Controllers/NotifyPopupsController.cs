using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class NotifyPopupsController : Controller
    {
        private readonly INotifyPopupRepository _notifyPopupRepository;

        public NotifyPopupsController(INotifyPopupRepository notifyPopupRepository)
        {
            _notifyPopupRepository = notifyPopupRepository;
        }

        public IActionResult Index()
        {
            return View(_notifyPopupRepository.NotifyPopups);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyPopup = _notifyPopupRepository.GetNotifyPopupById(id.Value);
            if (notifyPopup == null)
            {
                return NotFound();
            }

            return View(notifyPopup);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NotifyPopup notifyPopup)
        {
            if (ModelState.IsValid)
            {
                _notifyPopupRepository.AddNotifyPopup(notifyPopup);
                return RedirectToAction(nameof(Index));
            }
            return View(notifyPopup);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyPopup = _notifyPopupRepository.GetNotifyPopupById(id.Value);
            if (notifyPopup == null)
            {
                return NotFound();
            }
            return View(notifyPopup);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NotifyPopup notifyPopup)
        {
            if (id != notifyPopup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _notifyPopupRepository.UpdateNotifyPopup(notifyPopup);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_notifyPopupRepository.NotifyPopupExists(notifyPopup.Id))
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
            return View(notifyPopup);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifyPopup = _notifyPopupRepository.GetNotifyPopupById(id.Value);
            if (notifyPopup == null)
            {
                return NotFound();
            }
            return View(notifyPopup);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var notifyPopup = _notifyPopupRepository.GetNotifyPopupById(id);
            _notifyPopupRepository.DeleteNotifyPopup(notifyPopup.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateAllowOrder(int id)
        {
            try
            {
                _notifyPopupRepository.UpdateAllowOrder(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult UpdateActiveNotify(int id)
        {
            try
            {
                _notifyPopupRepository.UpdateActiveNotify(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }
    }
}
