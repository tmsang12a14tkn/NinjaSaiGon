using System;

namespace NinjaSaiGon.Admin.Models.AgencyViewModels
{
    public class AgencyDeliveryViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int? PickupTypeId { get; set; }
        public int AgencyId { get; set; }
        public int? AgencyDiscountCustomerTypeId { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
