using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.AgencyViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class AgencyCareViewComponent : ViewComponent
    {
        public AgencyCareViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(AgencyCareViewModel model)
        {
            return View(model);
        }
    }
}
