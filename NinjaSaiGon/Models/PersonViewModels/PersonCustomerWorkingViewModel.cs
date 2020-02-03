using System;

namespace NinjaSaiGon.Admin.Models.PersonViewModels
{
    public class PersonCustomerWorkingViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsMainPosition { get; set; }
        public bool IsWorking { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
