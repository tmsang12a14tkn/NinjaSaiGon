using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.ViewModels
{
    public class PagePermissionViewModel
    {
        public int ControllerActionId { get; set; }

        public string RoleId { get; set; }

        public bool CanAccess { get; set; }
    }

    public class PagePermissionSiteViewModel
    {
        public string MenuGroup { get; set; }

        public List<ControllerAction> ControllerActions { get; set; }
    }
}
