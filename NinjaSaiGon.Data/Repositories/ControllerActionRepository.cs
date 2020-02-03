using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class ControllerActionRepository : IControllerActionRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public ControllerActionRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ControllerActionPermission> ControllerActionPermissions => _appDbContext.ControllerActionPermissions;

        public IEnumerable<PagePermissionSiteViewModel> ControllerActions => _appDbContext.ControllerActions.Include("Permissions.Role")
                                                                                .GroupBy(ca => ca.MenuGroup).Select(g => new PagePermissionSiteViewModel
                                                                                {
                                                                                    MenuGroup = g.Key,
                                                                                    ControllerActions = g.OrderBy(ca => ca.ControllerName).ToList()
                                                                                });

        public void Update(List<PagePermissionViewModel> pagePermissions)
        {
            foreach (var pagePermission in pagePermissions)
            {
                var controllerActionPermission = _appDbContext.ControllerActionPermissions.Find(pagePermission.ControllerActionId, pagePermission.RoleId);

                if (pagePermission.CanAccess)
                {
                    if (controllerActionPermission == null)
                    {
                        //create
                        controllerActionPermission = new ControllerActionPermission
                        {
                            ControllerActionId = pagePermission.ControllerActionId,
                            RoleId = pagePermission.RoleId
                        };
                        _appDbContext.ControllerActionPermissions.Add(controllerActionPermission);
                    }
                }
                else
                {
                    if (controllerActionPermission != null)
                    {
                        //delete
                        _appDbContext.ControllerActionPermissions.Remove(controllerActionPermission);
                    }
                }
            }
            _appDbContext.SaveChanges();
        }
    }
}
