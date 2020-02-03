using Microsoft.AspNetCore.Mvc;
using NinjaSaiGon.Admin.Models.AgencyViewModels;
using NinjaSaiGon.Data.Interfaces;
using System.Linq;

namespace NinjaSaiGon.Admin.ViewComponents
{
    public class AgencyRepresentativeViewComponent : ViewComponent
    {
        private readonly IPersonRepository _personRepository;

        public AgencyRepresentativeViewComponent(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IViewComponentResult Invoke(AgencyRepresentativeViewModel model)
        {
            var person = _personRepository.GetById(model.PersonId);
            model.EmployeeCode = person.Code;
            model.EmployeeName = person.PrimaryName;
            model.EmployeePosition = person.PersonCustomerWorkings.FirstOrDefault(w => w.IsWorking && w.IsMainPosition)?.Position;
            model.EmployeeEmail = person.ContactInfo.Emails.FirstOrDefault(e => e.IsPrimary)?.Email;
            model.EmployeePhone = person.ContactInfo.Phones.FirstOrDefault(p => p.IsPrimary)?.PhoneNumber;
            return View(model);
        }
    }
}
