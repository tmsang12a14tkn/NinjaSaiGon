using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<DepartmentPosition> DepartmentPositions { get; set; }
        public virtual ICollection<PersonEmployeeWorking> Employees { get; set; }
    }

    public class DepartmentPosition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
