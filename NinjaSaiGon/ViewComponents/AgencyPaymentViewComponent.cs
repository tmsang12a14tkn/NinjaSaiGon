using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.AgencyViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class AgencyPaymentViewComponent : ViewComponent
    {
        public AgencyPaymentViewComponent()
        {

        }

        public IViewComponentResult Invoke(AgencyPaymentViewModel model)
        {
            return View(model);
        }
    }
}
