using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.AgencyViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class AgencyDeliveryViewComponent : ViewComponent
    {
        public AgencyDeliveryViewComponent()
        {

        }

        public IViewComponentResult Invoke(AgencyDeliveryViewModel model)
        {
            return View(model);
        }
    }
}
