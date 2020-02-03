using System;

namespace NinjaSaiGon.Admin.Models.PersonViewModels
{
    public class PersonEmployeeWorkingViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int DepartmentPositionId { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public DateTime? PositionBeginDate { get; set; }
        public DateTime? PositionEndDate { get; set; }
        public DateTime? TrialWorkBeginDate { get; set; }
        public DateTime? WorkBeginDate { get; set; }
        public bool IsMainPosition { get; set; }
        public bool IsWorking { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
