using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.PersonViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PersonCustomerWorkingViewComponent : ViewComponent
    {
        public PersonCustomerWorkingViewComponent()
        {
            
        }

        public IViewComponentResult Invoke(PersonCustomerWorkingViewModel model)
        {
            return View(model);
        }
    }
}
