using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaSaiGon.Data.ViewModels
{
    public class PersonUserView
    {
        public int? PersonId { get; set; }
        public string UserId { get; set; }
        public string OldUserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RoleId { get; set; }
        public bool IsMapAccount { get; set; }
    }
}
