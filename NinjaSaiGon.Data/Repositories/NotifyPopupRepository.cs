using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class NotifyPopupRepository : INotifyPopupRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public NotifyPopupRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<NotifyPopup> NotifyPopups => _appDbContext.NotifyPopups;

        public NotifyPopup GetNotifyPopupById(int notifyPopupId) => _appDbContext.NotifyPopups.FirstOrDefault(p => p.Id == notifyPopupId);
        public NotifyPopup GetActiveNotifyPopup() => _appDbContext.NotifyPopups.FirstOrDefault(p => p.IsActive && p.StartDate <= DateTime.Now.Date && p.EndDate >= DateTime.Now.Date);

        public void AddNotifyPopup(NotifyPopup notifyPopup)
        {
            _appDbContext.NotifyPopups.Add(notifyPopup);
            if (notifyPopup.IsActive)
            {
                var notifyPopups = _appDbContext.NotifyPopups.Where(s => s.Id != notifyPopup.Id);
                foreach (var n in notifyPopups)
                {
                    n.IsActive = false;
                }
            }
            _appDbContext.SaveChanges();
        }

        public void UpdateNotifyPopup(NotifyPopup notifyPopup)
        {
            _appDbContext.Entry(notifyPopup).State = EntityState.Modified;
            _appDbContext.Update(notifyPopup);
            if (notifyPopup.IsActive)
            {
                var notifyPopups = _appDbContext.NotifyPopups.Where(s => s.Id != notifyPopup.Id);
                foreach (var n in notifyPopups)
                {
                    n.IsActive = false;
                }
            }
            _appDbContext.SaveChanges();
        }

        public void DeleteNotifyPopup(int notifyPopupId)
        {
            var notifyPopup = GetNotifyPopupById(notifyPopupId);
            _appDbContext.NotifyPopups.Remove(notifyPopup);
            _appDbContext.SaveChanges();
        }

        public void UpdateAllowOrder(int notifyPopupId)
        {
            var notifyPopup = GetNotifyPopupById(notifyPopupId);
            notifyPopup.IsAllowOrder = true;

            _appDbContext.Entry(notifyPopup).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void UpdateActiveNotify(int notifyPopupId)
        {
            var notifyPopup = GetNotifyPopupById(notifyPopupId);
            foreach (var n in NotifyPopups)
            {
                n.IsActive = false;
            }
            notifyPopup.IsActive = true;
            _appDbContext.Entry(notifyPopup).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public bool NotifyPopupExists(int notifyPopupId)
        {
            return _appDbContext.NotifyPopups.Any(e => e.Id == notifyPopupId);
        }
    }
}
