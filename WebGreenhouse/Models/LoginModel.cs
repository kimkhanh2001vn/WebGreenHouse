using System.ComponentModel.DataAnnotations;

namespace WebGreenhouse.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mời bạn nhập Mật Khẩu")]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }
    }
}