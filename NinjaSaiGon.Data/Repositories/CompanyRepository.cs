using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace NinjaSaiGon.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public CompanyRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Company> Companies => _appDbContext.Companies;
        public IEnumerable<Department> Departments => _appDbContext.Departments;
        public IEnumerable<DepartmentPosition> DepartmentPositions => _appDbContext.DepartmentPositions.Include(d => d.Department);

        public Department GetDepartmentById(int id) => _appDbContext.Departments.Include(d => d.DepartmentPositions).Include(d => d.Employees)
                                                                            .Include("Employees.Person").Include("Employees.DepartmentPosition")
                                                                            .SingleOrDefault(m => m.Id == id);
        public void AddDepartment(Department department)
        {
            department.Code = "D" + HiResDateTime.UtcNowTicks;
            _appDbContext.Departments.Add(department);
            _appDbContext.SaveChanges();
        }
        public void UpdateDepartment(Department department)
        {
            _appDbContext.Update(department);
            _appDbContext.SaveChanges();
        }
        public bool DepartmentExists(int id) => _appDbContext.Departments.Any(e => e.Id == id);

        public DepartmentPosition GetDepartmentPositionById(int id) => _appDbContext.DepartmentPositions.SingleOrDefault(m => m.Id == id);
        public void AddDepartmentPosition(DepartmentPosition departmentPosition)
        {
            departmentPosition.Code = "DP" + HiResDateTime.UtcNowTicks;
            _appDbContext.DepartmentPositions.Add(departmentPosition);
            _appDbContext.SaveChanges();
        }
        public void UpdateDepartmentPosition(DepartmentPosition departmentPosition)
        {
            _appDbContext.Update(departmentPosition);
            _appDbContext.SaveChanges();
        }
        public bool DepartmentPositionExists(int id) => _appDbContext.DepartmentPositions.Any(e => e.Id == id);
    }
}
