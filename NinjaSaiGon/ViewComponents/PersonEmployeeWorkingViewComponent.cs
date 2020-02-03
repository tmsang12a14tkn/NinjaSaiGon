using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.PersonViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class PersonEmployeeWorkingViewComponent : ViewComponent
    {
        private readonly ICompanyRepository _companyRepository;

        public PersonEmployeeWorkingViewComponent(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IViewComponentResult Invoke(PersonEmployeeWorkingViewModel model)
        {
            var department = _companyRepository.GetDepartmentById(model.DepartmentId);
            model.Department = department.Name;

            var position = _companyRepository.GetDepartmentPositionById(model.DepartmentPositionId);
            model.Position = position.Name;

            return View(model);
        }
    }
}
