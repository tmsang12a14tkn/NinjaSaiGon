using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NinjaSaiGon.Data.Models
{
    public class VehicleInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string Manufacturer { get; set; }
        public string CylinderCapacity { get; set; } //dung tich xi lanh
        public string FrameCode { get; set; } //so khung
        public string MachineCode { get; set; } //so may

        public string Status { get; set; } //tinh trang phuong tien
        public string Owner { get; set; }  //chinh chu
        public bool IsPrimary { get; set; } //phuong tien chinh
        public string Note { get; set; } //ghi chu

        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<VehiclePhoto> Photos { get; set; }

    }

    public class VehiclePhoto
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int VehicleInfoId { get; set; }

        public virtual VehicleInfo VehicleInfo { get; set; }
    }
}
