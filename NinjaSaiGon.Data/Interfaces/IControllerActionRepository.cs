using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IControllerActionRepository
    {
        IEnumerable<ControllerActionPermission> ControllerActionPermissions { get; }
        IEnumerable<PagePermissionSiteViewModel> ControllerActions { get; }
        void Update(List<PagePermissionViewModel> pagePermissions);
    }
}
