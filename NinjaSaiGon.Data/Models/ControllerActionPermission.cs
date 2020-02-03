using Microsoft.AspNetCore.Identity;

namespace NinjaSaiGon.Data.Models
{
    public class ControllerActionPermission
    {
        public int ControllerActionId { get; set; }
        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }
        public virtual ControllerAction ControllerAction { get; set; }
    }
}
