using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Models;
using System.Linq;
using System.Security.Principal;

namespace NinjaSaiGon.Admin.Helpers
{
    public static class ExtensionAuthorize
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static bool CanAccess(this IPrincipal principal, string actionName, string controllerName, string type, string site)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer("Data Source=27.74.252.26;Initial Catalog=NinjaSaiGon;Persist Security Info=True;User ID=ninjasaigon;Password=Abc@@123;MultipleActiveResultSets=True");
            using (var context = new ApplicationDbContext(options.Options))
            {
                var controllerAction = context.ControllerActions.FirstOrDefault(ca => ca.ActionName == actionName && ca.ControllerName == controllerName && ca.Params == type && ca.Site == site);
                if (controllerAction == null)
                {
                    context.ControllerActions.Add(new ControllerAction
                    {
                        Site = site,
                        ActionName = actionName,
                        ControllerName = controllerName,
                        Params = type,
                        Description = $"{controllerName}/{actionName}"
                    });
                    context.SaveChanges();
                    return false;
                }

                var temp = context.ControllerActionPermissions.Include(c => c.Role).Where(p => p.ControllerActionId == controllerAction.Id).ToList();
                var controllerActionPermission = temp.FirstOrDefault(p => principal.IsInRole(p.Role.Name));
                if (controllerActionPermission != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
