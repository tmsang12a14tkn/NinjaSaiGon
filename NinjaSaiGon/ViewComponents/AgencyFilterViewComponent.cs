using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class AgencyFilterViewComponent : ViewComponent
    {
        public AgencyFilterViewComponent()
        {

        }

        public IViewComponentResult Invoke(List<Agency> agencies)
        {
            return View(agencies);
        }
    }
}
