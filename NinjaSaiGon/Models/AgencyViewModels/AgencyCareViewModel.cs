using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.AgencyViewModels
{
    public class AgencyCareViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AgencyId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
