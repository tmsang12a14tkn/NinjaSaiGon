using NinjaSaiGon.Data.Models;
using System.Collections.Generic;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> Companies { get; }
        IEnumerable<Department> Departments { get; }
        IEnumerable<DepartmentPosition> DepartmentPositions { get; }

        Department GetDepartmentById(int id);
        void AddDepartment(Department department);
        void UpdateDepartment(Department department);
        bool DepartmentExists(int id);

        DepartmentPosition GetDepartmentPositionById(int id);
        void AddDepartmentPosition(DepartmentPosition departmentPosition);
        void UpdateDepartmentPosition(DepartmentPosition departmentPosition);
        bool DepartmentPositionExists(int id);
    }
}
