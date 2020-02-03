using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.PersonViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PersonCustomerCareViewComponent : ViewComponent
    {
        public PersonCustomerCareViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(PersonCustomerCareViewModel model)
        {
            return View(model);
        }
    }
}
