using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data;
using NinjaSaiGon.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Helpers
{
    public class RoleAuthorizeAttribute : AuthorizationHandler<SiteRequirement>
    {
        private readonly IActionContextAccessor _actionContextAccessor;

        public RoleAuthorizeAttribute(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SiteRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            var actionName = _actionContextAccessor.ActionContext.RouteData.Values["action"].ToString();
            var controllerName = _actionContextAccessor.ActionContext.RouteData.Values["controller"].ToString();
            var typePerson = controllerName.ToLower() == "person" ? _actionContextAccessor.ActionContext.HttpContext.Request.Query["type"].ToString() : null;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer("Data Source=27.74.252.26;Initial Catalog=NinjaSaiGon;Persist Security Info=True;User ID=ninjasaigon;Password=Abc@@123;MultipleActiveResultSets=True");
            using (var dbcontext = new ApplicationDbContext(options.Options))
            {
                var controllerAction = dbcontext.ControllerActions.FirstOrDefault(ca => ca.ActionName == actionName && ca.ControllerName == controllerName && ca.Params == typePerson && ca.Site == requirement.Site);
                if (controllerAction == null)
                {
                    dbcontext.ControllerActions.Add(new ControllerAction
                    {
                        Site = requirement.Site,
                        ActionName = actionName,
                        ControllerName = controllerName,
                        Params = typePerson,
                        Description = $"{controllerName}/{actionName}"
                    });
                    dbcontext.SaveChanges();
                    return Task.CompletedTask;
                }

                var temp = dbcontext.ControllerActionPermissions.Include(c => c.Role).Where(p => p.ControllerActionId == controllerAction.Id).ToList();
                var controllerActionPermission = temp.FirstOrDefault(p => context.User.IsInRole(p.Role.Name));
                if (controllerActionPermission != null)
                    context.Succeed(requirement);
                else
                    return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }

    public class SiteRequirement : IAuthorizationRequirement
    {
        public SiteRequirement(string site)
        {
            Site = site;
        }

        public string Site { get; set; }
    }
}
