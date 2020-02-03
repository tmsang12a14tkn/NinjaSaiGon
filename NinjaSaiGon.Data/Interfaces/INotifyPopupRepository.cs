using NinjaSaiGon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface INotifyPopupRepository
    {
        IEnumerable<NotifyPopup> NotifyPopups { get; }
        NotifyPopup GetNotifyPopupById(int notifyPopupId);
        NotifyPopup GetActiveNotifyPopup();
        void AddNotifyPopup(NotifyPopup notifyPopup);
        void UpdateNotifyPopup(NotifyPopup notifyPopup);
        void DeleteNotifyPopup(int notifyPopupId);
        void UpdateAllowOrder(int notifyPopupId);
        void UpdateActiveNotify(int notifyPopupId);
        bool NotifyPopupExists(int notifyPopupId);
    }
}
