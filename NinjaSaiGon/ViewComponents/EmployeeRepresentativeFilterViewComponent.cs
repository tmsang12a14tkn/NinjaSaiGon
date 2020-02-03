using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class EmployeeRepresentativeFilterViewComponent : ViewComponent
    {
        public EmployeeRepresentativeFilterViewComponent()
        {

        }

        public IViewComponentResult Invoke(List<Person> persons)
        {
            return View(persons);
        }
    }
}
