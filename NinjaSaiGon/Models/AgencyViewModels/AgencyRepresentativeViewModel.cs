using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.AgencyViewModels
{
    public class AgencyRepresentativeViewModel
    {
        public int Index { get; set; }
        public int PersonId { get; set; }
        public int AgencyId { get; set; }
        public bool IsMainContact { get; set; }
        public bool IsDeleted { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
