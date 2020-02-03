using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaSaiGon.Admin.Models.AccountViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        
        [Display(Name = "Điện thoại")]
        public string PhoneNumber { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
