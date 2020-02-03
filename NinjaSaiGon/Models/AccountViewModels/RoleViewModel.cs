using System.ComponentModel.DataAnnotations;

namespace NinjaSaiGon.Admin.Models.AccountViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }
}
