using System;

namespace NinjaSaiGon.Admin.Models.PersonViewModels
{
    public class PersonCustomerCareViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PersonId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
