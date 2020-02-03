using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Admin.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            return View(_companyRepository.Departments);
        }

        public IActionResult DepartmentPositionIndex()
        {
            return View(_companyRepository.DepartmentPositions);
        }

        public IActionResult CreateDepartment()
        {
            ViewBag.Companies = _companyRepository.Companies;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDepartment(Department department)
        {
            try
            {
                _companyRepository.AddDepartment(department);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Companies = _companyRepository.Companies;
                return View(department);
            }
        }

        public IActionResult EditDepartment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _companyRepository.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.Companies = _companyRepository.Companies;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            try
            {
                _companyRepository.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.Companies = _companyRepository.Companies;
                return View(department);
            }
        }

        public IActionResult CreateDepartmentPosition()
        {
            ViewBag.Departments = _companyRepository.Departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDepartmentPosition(DepartmentPosition departmentPosition)
        {
            try
            {
                _companyRepository.AddDepartmentPosition(departmentPosition);
                return RedirectToAction("DepartmentPositionIndex");
            }
            catch
            {
                ViewBag.Departments = _companyRepository.Departments;
                return View(departmentPosition);
            }
        }

        public IActionResult EditDepartmentPosition(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentPosition = _companyRepository.GetDepartmentPositionById(id.Value);
            if (departmentPosition == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _companyRepository.Departments;
            return View(departmentPosition);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDepartmentPosition(int id, DepartmentPosition departmentPosition)
        {
            if (id != departmentPosition.Id)
            {
                return NotFound();
            }

            try
            {
                _companyRepository.UpdateDepartmentPosition(departmentPosition);
                return RedirectToAction("DepartmentPositionIndex");
            }
            catch (DbUpdateConcurrencyException)
            {
                ViewBag.Departments = _companyRepository.Departments;
                return View(departmentPosition);
            }
        }

        public IActionResult GetListEmployee(int id)
        {
            var department = _companyRepository.GetDepartmentById(id);
            return PartialView("_ListDepartmentEmployee", department);
        }
    }
}