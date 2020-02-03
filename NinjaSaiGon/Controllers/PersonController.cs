using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Admin.Models.PersonViewModels;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.ViewModels;
using NinjaSaiGon.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPersonLevelRepository _personLevelRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public PersonController(IPersonRepository personRepository, IPersonLevelRepository personLevelRepository, 
                                IAgencyRepository agencyRepository, ICompanyRepository companyRepository,
                                UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _personRepository = personRepository;
            _personLevelRepository = personLevelRepository;
            _agencyRepository = agencyRepository;
            _companyRepository = companyRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public IActionResult Index(PersonType type)
        {
            ViewBag.PersonType = type;

            if (type == PersonType.Customer)
                return View(_personRepository.Customers);
            else if (type == PersonType.Employee)
                return View(_personRepository.Employees);
            else
                return View(_personRepository.Representatives);
        }

        public IActionResult Create(PersonType type)
        {
            ViewBag.PersonLevels = _personLevelRepository.PersonLevels;
            ViewBag.NameTypes = _personRepository.PersonalNameTypes;
            ViewBag.Religions = _personRepository.Religions;
            ViewBag.PersonalDobTypes = _personRepository.PersonalDobTypes;
            ViewBag.MaritalStatusTypes = _personRepository.MaritalStatusTypes;
            ViewBag.Nationalities = _personRepository.Nationalities;
            ViewBag.Ethnics = _personRepository.Ethnics;
            ViewBag.PersonalPhotoTypes = _personRepository.PersonalPhotoTypes;
            ViewBag.AddressTypes = _personRepository.AddressTypes;
            ViewBag.PhoneTypes = _personRepository.PhoneTypes;
            ViewBag.EmailTypes = _personRepository.EmailTypes;
            ViewBag.SocialTypes = _personRepository.SocialTypes;
            ViewBag.OTTTypes = _personRepository.OTTTypes;
            ViewBag.IDDocumentTypes = _personRepository.IDDocumentTypes;
            ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
            ViewBag.BankCardTypes = _personRepository.BankCardTypes;
            ViewBag.PersonCustomerSourceTypes = _personRepository.PersonCustomerSourceTypes;
            ViewBag.Wards = _personRepository.Wards;
            ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
            ViewBag.Provinces = _personRepository.Provinces;
            ViewBag.Customers = _personRepository.Customers;
            ViewBag.Employees = _personRepository.Employees;
            ViewBag.Agencies = _agencyRepository.Agencies;
            ViewBag.Departments = _companyRepository.Departments;
            ViewBag.DepartmentPositions = _companyRepository.DepartmentPositions;
            ViewBag.PersonType = type;
            ViewBag.AvailableUsers = _userManager.Users.Where(u => !u.PersonId.HasValue && u.UserName != "admin");
            ViewBag.RolesList = _roleManager.Roles;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonType type, Person person)
        {
            try
            {
                _personRepository.Add(person);

                if (person.PersonUser != null)
                {
                    if (!person.PersonUser.IsMapAccount)
                    {
                        if (!string.IsNullOrEmpty(person.PersonUser.UserName) && !string.IsNullOrEmpty(person.PersonUser.Password) && !_userManager.Users.Any(u => u.UserName == person.PersonUser.UserName))
                        {
                            var newUser = new ApplicationUser { PersonId = person.Id, UserName = person.PersonUser.UserName, Email = "" };
                            var result = await _userManager.CreateAsync(newUser, person.PersonUser.Password);
                            if (result.Succeeded)
                            {
                                var rs = await _userManager.AddToRolesAsync(newUser, new string[] { person.PersonUser.RoleId });
                                if (!rs.Succeeded)
                                {
                                    ModelState.AddModelError("", rs.Errors.First().Description);
                                }
                            }
                        }
                    }
                    else
                    {
                        var existUser = await _userManager.FindByNameAsync(person.PersonUser.UserName);
                        existUser.PersonId = person.Id;
                        var result = await _userManager.UpdateAsync(existUser);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }

                        var userRoles = await _userManager.GetRolesAsync(existUser);
                        result = await _userManager.AddToRolesAsync(existUser, new string[] { person.PersonUser.RoleId }.Except(userRoles).ToArray<string>());
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }

                        result = await _userManager.RemoveFromRolesAsync(existUser, userRoles.Except(new string[] { person.PersonUser.RoleId }).ToArray<string>());
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }
                    }
                }

                return RedirectToAction("Index", new { type = person.PersonType });
            }
            catch
            {
                ViewBag.PersonLevels = _personLevelRepository.PersonLevels;
                ViewBag.NameTypes = _personRepository.PersonalNameTypes;
                ViewBag.Religions = _personRepository.Religions;
                ViewBag.PersonalDobTypes = _personRepository.PersonalDobTypes;
                ViewBag.MaritalStatusTypes = _personRepository.MaritalStatusTypes;
                ViewBag.Nationalities = _personRepository.Nationalities;
                ViewBag.Ethnics = _personRepository.Ethnics;
                ViewBag.PersonalPhotoTypes = _personRepository.PersonalPhotoTypes;
                ViewBag.AddressTypes = _personRepository.AddressTypes;
                ViewBag.PhoneTypes = _personRepository.PhoneTypes;
                ViewBag.EmailTypes = _personRepository.EmailTypes;
                ViewBag.SocialTypes = _personRepository.SocialTypes;
                ViewBag.OTTTypes = _personRepository.OTTTypes;
                ViewBag.IDDocumentTypes = _personRepository.IDDocumentTypes;
                ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
                ViewBag.BankCardTypes = _personRepository.BankCardTypes;
                ViewBag.PersonCustomerSourceTypes = _personRepository.PersonCustomerSourceTypes;
                ViewBag.Wards = _personRepository.Wards;
                ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
                ViewBag.Provinces = _personRepository.Provinces;
                ViewBag.Customers = _personRepository.Customers;
                ViewBag.Employees = _personRepository.Employees;
                ViewBag.Agencies = _agencyRepository.Agencies;
                ViewBag.Departments = _companyRepository.Departments;
                ViewBag.DepartmentPositions = _companyRepository.DepartmentPositions;
                ViewBag.PersonType = person.PersonType;
                ViewBag.AvailableUsers = _userManager.Users.Where(u => !u.PersonId.HasValue && u.UserName != "admin");
                ViewBag.RolesList = _roleManager.Roles;

                return View(person);
            }
        }

        [HttpPost]
        public IActionResult AddPersonWorking(PersonCustomerWorkingViewModel model)
        {
            return ViewComponent("PersonCustomerWorking", new { model });
        }

        [HttpPost]
        public IActionResult AddEmployeeWorking(PersonEmployeeWorkingViewModel model)
        {
            return ViewComponent("PersonEmployeeWorking", new { model });
        }

        [HttpPost]
        public IActionResult AddPersonCustomerCare(PersonCustomerCareViewModel model)
        {
            return ViewComponent("PersonCustomerCare", new { model });
        }

        public async Task<IActionResult> Edit(int? id, PersonType type)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _personRepository.GetById(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            if (person.PersonType == PersonType.Employee)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.PersonId == person.Id);
                person.PersonUser = new PersonUserView();
                if (user != null)
                {
                    person.IsHaveAccount = true;

                    person.PersonUser.UserId = user.Id;
                    person.PersonUser.OldUserId = user.Id;
                    person.PersonUser.PersonId = user.PersonId;
                    person.PersonUser.UserName = user.UserName;

                    var roles = await _userManager.GetRolesAsync(user);
                    person.PersonUser.RoleId = roles[0];
                }
            }

            ViewBag.PersonLevels = _personLevelRepository.PersonLevels;
            ViewBag.NameTypes = _personRepository.PersonalNameTypes;
            ViewBag.Religions = _personRepository.Religions;
            ViewBag.PersonalDobTypes = _personRepository.PersonalDobTypes;
            ViewBag.MaritalStatusTypes = _personRepository.MaritalStatusTypes;
            ViewBag.Nationalities = _personRepository.Nationalities;
            ViewBag.Ethnics = _personRepository.Ethnics;
            ViewBag.PersonalPhotoTypes = _personRepository.PersonalPhotoTypes;
            ViewBag.AddressTypes = _personRepository.AddressTypes;
            ViewBag.PhoneTypes = _personRepository.PhoneTypes;
            ViewBag.EmailTypes = _personRepository.EmailTypes;
            ViewBag.SocialTypes = _personRepository.SocialTypes;
            ViewBag.OTTTypes = _personRepository.OTTTypes;
            ViewBag.IDDocumentTypes = _personRepository.IDDocumentTypes;
            ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
            ViewBag.BankCardTypes = _personRepository.BankCardTypes;
            ViewBag.PersonCustomerSourceTypes = _personRepository.PersonCustomerSourceTypes;
            ViewBag.Wards = _personRepository.Wards;
            ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
            ViewBag.Provinces = _personRepository.Provinces;
            ViewBag.Customers = _personRepository.Customers;
            ViewBag.Employees = _personRepository.Employees;
            ViewBag.Agencies = _agencyRepository.Agencies;
            ViewBag.Departments = _companyRepository.Departments;
            ViewBag.DepartmentPositions = _companyRepository.DepartmentPositions;
            ViewBag.PersonType = person.PersonType;
            ViewBag.AvailableUsers = _userManager.Users.Where(u => !u.PersonId.HasValue && u.UserName != "admin");
            ViewBag.RolesList = _roleManager.Roles;

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonType type, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            try
            {
                _personRepository.Update(person);

                if (person.PersonUser != null)
                {
                    if (person.PersonUser.OldUserId != person.PersonUser.UserId && person.PersonUser.OldUserId != "")
                    {
                        var oldUser = await _userManager.FindByIdAsync(person.PersonUser.OldUserId);
                        if (oldUser != null)
                        {
                            oldUser.PersonId = null;
                            var result = await _userManager.UpdateAsync(oldUser);
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First().Description);
                            }
                        }
                    }

                    var user = await _userManager.FindByIdAsync(person.PersonUser.UserId);
                    if (user == null)
                    {
                        if (!person.PersonUser.IsMapAccount)
                        {
                            if (!string.IsNullOrEmpty(person.PersonUser.UserName) && !string.IsNullOrEmpty(person.PersonUser.Password) && !_userManager.Users.Any(u => u.UserName == person.PersonUser.UserName))
                            {
                                var newUser = new ApplicationUser { PersonId = person.Id, UserName = person.PersonUser.UserName, Email = "" };
                                var result = await _userManager.CreateAsync(newUser, person.PersonUser.Password);
                                if (result.Succeeded)
                                {
                                    var rs = await _userManager.AddToRolesAsync(newUser, new string[] { person.PersonUser.RoleId });
                                    if (!rs.Succeeded)
                                    {
                                        ModelState.AddModelError("", rs.Errors.First().Description);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var existUser = await _userManager.FindByNameAsync(person.PersonUser.UserName);
                            existUser.PersonId = person.Id;
                            var result = await _userManager.UpdateAsync(existUser);
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First().Description);
                            }

                            var userRoles = await _userManager.GetRolesAsync(existUser);
                            result = await _userManager.AddToRolesAsync(existUser, new string[] { person.PersonUser.RoleId }.Except(userRoles).ToArray<string>());
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First().Description);
                            }

                            result = await _userManager.RemoveFromRolesAsync(existUser, userRoles.Except(new string[] { person.PersonUser.RoleId }).ToArray<string>());
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First().Description);
                            }
                        }
                    }
                    else
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        var result = await _userManager.AddToRolesAsync(user, new string[] { person.PersonUser.RoleId }.Except(userRoles).ToArray<string>());
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }

                        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(new string[] { person.PersonUser.RoleId }).ToArray<string>());
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First().Description);
                        }
                    }
                }

                return RedirectToAction("Index", new { type = person.PersonType });
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.PersonLevels = _personLevelRepository.PersonLevels;
                ViewBag.NameTypes = _personRepository.PersonalNameTypes;
                ViewBag.Religions = _personRepository.Religions;
                ViewBag.PersonalDobTypes = _personRepository.PersonalDobTypes;
                ViewBag.MaritalStatusTypes = _personRepository.MaritalStatusTypes;
                ViewBag.Nationalities = _personRepository.Nationalities;
                ViewBag.Ethnics = _personRepository.Ethnics;
                ViewBag.PersonalPhotoTypes = _personRepository.PersonalPhotoTypes;
                ViewBag.AddressTypes = _personRepository.AddressTypes;
                ViewBag.PhoneTypes = _personRepository.PhoneTypes;
                ViewBag.EmailTypes = _personRepository.EmailTypes;
                ViewBag.SocialTypes = _personRepository.SocialTypes;
                ViewBag.OTTTypes = _personRepository.OTTTypes;
                ViewBag.IDDocumentTypes = _personRepository.IDDocumentTypes;
                ViewBag.BankAccountTypes = _personRepository.BankAccountTypes;
                ViewBag.BankCardTypes = _personRepository.BankCardTypes;
                ViewBag.PersonCustomerSourceTypes = _personRepository.PersonCustomerSourceTypes;
                ViewBag.Wards = _personRepository.Wards;
                ViewBag.DistrictPlaces = _personRepository.DistrictPlaces;
                ViewBag.Provinces = _personRepository.Provinces;
                ViewBag.Customers = _personRepository.Customers;
                ViewBag.Employees = _personRepository.Employees;
                ViewBag.Agencies = _agencyRepository.Agencies;
                ViewBag.Departments = _companyRepository.Departments;
                ViewBag.DepartmentPositions = _companyRepository.DepartmentPositions;
                ViewBag.PersonType = person.PersonType;
                ViewBag.AvailableUsers = _userManager.Users.Where(u => !u.PersonId.HasValue && u.UserName != "admin");
                ViewBag.RolesList = _roleManager.Roles;

                return View(person);
            }
        }

        public IActionResult QuickCreate(PersonType type)
        {
            ViewBag.PersonType = type;
            return View();
        }
        
        public IActionResult FilterPerson(PersonType type, string name, string phone)
        {
            var arrNames = !string.IsNullOrEmpty(name) ? name.Split(' ') : null;
            List<Person> persons;
            if (type == PersonType.Representative)
                persons = _personRepository.Representatives.ToList();
            else if (type == PersonType.Customer)
                persons = _personRepository.Customers.ToList();
            else
                persons = _personRepository.Employees.ToList();

            if (!string.IsNullOrEmpty(phone))
                persons = persons.Where(c => c.ContactInfo.Phones.Any(p => !string.IsNullOrEmpty(p.PhoneNumber) && p.PhoneNumber.Contains(phone))).ToList();

            if (arrNames != null)
            {
                var filterName = new List<Person>();
                foreach (var subName in arrNames)
                {
                    var subPerson = persons.Where(c => c.PrimaryName.ToLower().Contains(subName.ToLower())).ToList();
                    if (subPerson.Any())
                        filterName.AddRange(subPerson.Except(filterName));
                }

                if (filterName.Any())
                    persons = filterName;
            }

            return ViewComponent("EmployeeRepresentativeFilter", new { persons });
        }

        public IActionResult GetWardByDistrict(int districtId)
        {
            var result = _personRepository.Wards.Where(w => w.DistrictPlaceId == districtId)
                                                .Select(w => new SelectListItem()
                                                {
                                                    Text = w.Name,
                                                    Value = w.Id.ToString()
                                                }).ToList();
            return Json(new { result });
        }
    }
}
