using System.ComponentModel.DataAnnotations;

namespace NinjaSaiGon.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email/Tên đăng nhập")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ đăng nhập?")]
        public bool RememberMe { get; set; }
    }
}
