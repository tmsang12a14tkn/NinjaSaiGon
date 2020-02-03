using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class QuickCreatePersonViewComponent : ViewComponent
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonLevelRepository _personLevelRepository;
        private readonly IAgencyRepository _agencyRepository;

        public QuickCreatePersonViewComponent(IPersonRepository personRepository, IPersonLevelRepository personLevelRepository, IAgencyRepository agencyRepository)
        {
            _personRepository = personRepository;
            _personLevelRepository = personLevelRepository;
            _agencyRepository = agencyRepository;
        }

        public IViewComponentResult Invoke(PersonType type)
        {
            ViewBag.PersonLevels = _personLevelRepository.PersonLevels;
            ViewBag.PersonCustomerSourceTypes = _personRepository.PersonCustomerSourceTypes;
            ViewBag.Customers = _personRepository.Customers;
            ViewBag.Employees = _personRepository.Employees;
            ViewBag.Agencies = _agencyRepository.Agencies;
            ViewBag.SocialTypes = _personRepository.SocialTypes;
            ViewBag.PersonType = type;

            return View();
        }
    }
}
