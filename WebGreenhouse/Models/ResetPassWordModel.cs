using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Models
{
    public class ResetPassWordModel
    {
        [Required(ErrorMessage ="required password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassWord { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassWord",ErrorMessage ="new password and confirm password do not  match")]
        [Required]
        public string ConfirmPassWord { get; set; }
        [Required]
        public string ResetCode { get; set; }
    }
}