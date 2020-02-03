using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Models.AgencyViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AgencyController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAgencyRepository _agencyRepository;

        public AgencyController(IPersonRepository personRepository, IAgencyRepository agencyRepository)
        {
            _personRepository = personRepository;
            _agencyRepository = agencyRepository;
        }

        public IActionResult Index()
        {
            return View(_agencyRepository.Agencies);
        }

        public IActionResult Create()
        {
            ViewBag.Nationalities = _personRepository.Nationalities;
            ViewBag.AddressTypes = _personRepository.AddressTypes;
            ViewBag.PhoneTypes = _personRepository.PhoneTypes;
            ViewBag.EmailTypes = _personRepository.EmailTypes;
            ViewBag.SocialTypes = _personRepository.SocialTypes;
            ViewBag.OTTTypes = _personRepository.OTTTypes;
            ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
            ViewBag.BankCardTypes = _personRepository.BankCardTypes;
            ViewBag.Wards = _personRepository.Wards;
            ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
            ViewBag.Provinces = _personRepository.Provinces;
            ViewBag.Representatives = _personRepository.Representatives;
            ViewBag.Employees = _personRepository.Employees;
            ViewBag.AgencyGroups = _agencyRepository.AgencyGroups;
            ViewBag.AgencyBusinesses = _agencyRepository.AgencyBusinesses;
            ViewBag.CurrencyTypes = _agencyRepository.CurrencyTypes;
            ViewBag.PaymentTypes = _agencyRepository.PaymentTypes;
            ViewBag.PaymentTermTypes = _agencyRepository.PaymentTermTypes;
            ViewBag.AgencyDiscountTypes = _agencyRepository.AgencyDiscountTypes;
            ViewBag.PickupTypes = _agencyRepository.PickupTypes;
            ViewBag.AgencyDiscountCustomerTypes = _agencyRepository.AgencyDiscountCustomerTypes;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Agency agency)
        {
            try
            {
                _agencyRepository.Add(agency);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Nationalities = _personRepository.Nationalities;
                ViewBag.AddressTypes = _personRepository.AddressTypes;
                ViewBag.PhoneTypes = _personRepository.PhoneTypes;
                ViewBag.EmailTypes = _personRepository.EmailTypes;
                ViewBag.SocialTypes = _personRepository.SocialTypes;
                ViewBag.OTTTypes = _personRepository.OTTTypes;
                ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
                ViewBag.BankCardTypes = _personRepository.BankCardTypes;
                ViewBag.Wards = _personRepository.Wards;
                ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
                ViewBag.Provinces = _personRepository.Provinces;
                ViewBag.Representatives = _personRepository.Representatives;
                ViewBag.Employees = _personRepository.Employees;
                ViewBag.AgencyGroups = _agencyRepository.AgencyGroups;
                ViewBag.AgencyBusinesses = _agencyRepository.AgencyBusinesses;
                ViewBag.CurrencyTypes = _agencyRepository.CurrencyTypes;
                ViewBag.PaymentTypes = _agencyRepository.PaymentTypes;
                ViewBag.PaymentTermTypes = _agencyRepository.PaymentTermTypes;
                ViewBag.AgencyDiscountTypes = _agencyRepository.AgencyDiscountTypes;
                ViewBag.PickupTypes = _agencyRepository.PickupTypes;
                ViewBag.AgencyDiscountCustomerTypes = _agencyRepository.AgencyDiscountCustomerTypes;

                return View(agency);
            }
        }

        [HttpPost]
        public IActionResult AddAgencyCare(AgencyCareViewModel model)
        {
            return ViewComponent("AgencyCare", new { model });
        }

        [HttpPost]
        public IActionResult AddAgencyRepresentative(AgencyRepresentativeViewModel model)
        {
            return ViewComponent("AgencyRepresentative", new { model });
        }

        [HttpPost]
        public IActionResult AddPayment(AgencyPaymentViewModel model)
        {
            return ViewComponent("AgencyPayment", new { model });
        }

        [HttpPost]
        public IActionResult AddDelivery(AgencyDeliveryViewModel model)
        {
            return ViewComponent("AgencyDelivery", new { model });
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agency = _agencyRepository.GetById(id.Value);
            if (agency == null)
            {
                return NotFound();
            }

            foreach (var aRepresentative in agency.AgencyRepresentatives)
            {
                aRepresentative.Person = _personRepository.GetById(aRepresentative.PersonId);
            }

            ViewBag.Nationalities = _personRepository.Nationalities;
            ViewBag.AddressTypes = _personRepository.AddressTypes;
            ViewBag.PhoneTypes = _personRepository.PhoneTypes;
            ViewBag.EmailTypes = _personRepository.EmailTypes;
            ViewBag.SocialTypes = _personRepository.SocialTypes;
            ViewBag.OTTTypes = _personRepository.OTTTypes;
            ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
            ViewBag.BankCardTypes = _personRepository.BankCardTypes;
            ViewBag.Wards = _personRepository.Wards;
            ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
            ViewBag.Provinces = _personRepository.Provinces;
            ViewBag.Representatives = _personRepository.Representatives;
            ViewBag.Employees = _personRepository.Employees;
            ViewBag.AgencyGroups = _agencyRepository.AgencyGroups;
            ViewBag.AgencyBusinesses = _agencyRepository.AgencyBusinesses;
            ViewBag.CurrencyTypes = _agencyRepository.CurrencyTypes;
            ViewBag.PaymentTypes = _agencyRepository.PaymentTypes;
            ViewBag.PaymentTermTypes = _agencyRepository.PaymentTermTypes;
            ViewBag.AgencyDiscountTypes = _agencyRepository.AgencyDiscountTypes;
            ViewBag.PickupTypes = _agencyRepository.PickupTypes;
            ViewBag.AgencyDiscountCustomerTypes = _agencyRepository.AgencyDiscountCustomerTypes;

            return View(agency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Agency agency)
        {
            if (id != agency.Id)
            {
                return NotFound();
            }

            try
            {
                _agencyRepository.Update(agency);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.Nationalities = _personRepository.Nationalities;
                ViewBag.AddressTypes = _personRepository.AddressTypes;
                ViewBag.PhoneTypes = _personRepository.PhoneTypes;
                ViewBag.EmailTypes = _personRepository.EmailTypes;
                ViewBag.SocialTypes = _personRepository.SocialTypes;
                ViewBag.OTTTypes = _personRepository.OTTTypes;
                ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
                ViewBag.BankCardTypes = _personRepository.BankCardTypes;
                ViewBag.Wards = _personRepository.Wards;
                ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
                ViewBag.Provinces = _personRepository.Provinces;
                ViewBag.Representatives = _personRepository.Representatives;
                ViewBag.Employees = _personRepository.Employees;
                ViewBag.AgencyGroups = _agencyRepository.AgencyGroups;
                ViewBag.AgencyBusinesses = _agencyRepository.AgencyBusinesses;
                ViewBag.CurrencyTypes = _agencyRepository.CurrencyTypes;
                ViewBag.PaymentTypes = _agencyRepository.PaymentTypes;
                ViewBag.PaymentTermTypes = _agencyRepository.PaymentTermTypes;
                ViewBag.AgencyDiscountTypes = _agencyRepository.AgencyDiscountTypes;
                ViewBag.PickupTypes = _agencyRepository.PickupTypes;
                ViewBag.AgencyDiscountCustomerTypes = _agencyRepository.AgencyDiscountCustomerTypes;

                return View(agency);
            }
        }

        public IActionResult FilterAgency(string name, string phone)
        {
            var arrNames = !string.IsNullOrEmpty(name) ? name.Split(' ') : null;
            var agencies = _agencyRepository.Agencies.ToList();

            if (!string.IsNullOrEmpty(phone))
                agencies = agencies.Where(c => c.AgencyContactInfo.Phones.Any(p => !string.IsNullOrEmpty(p.PhoneNumber) && p.PhoneNumber.Contains(phone))).ToList();

            if (arrNames != null)
            {
                var filterName = new List<Agency>();
                foreach (var subName in arrNames)
                {
                    var subAgency = agencies.Where(c => c.Name.ToLower().Contains(subName.ToLower()) || c.TradingName.ToLower().Contains(subName.ToLower())).ToList();
                    if (subAgency.Any())
                        filterName.AddRange(subAgency.Except(filterName));
                }

                if (filterName.Any())
                    agencies = filterName;
            }

            return ViewComponent("AgencyFilter", new { agencies });
        }
    }
}