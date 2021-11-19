namespace WebGreenhouse.Models
{
    public class RegistrationModel
    {
        public string UserName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerWork { get; set; }

        public bool? Status { get; set; }

        public string CustomerEmail { get; set; }
        public string Password { get; set; }
        public int? NumberPhone { get; set; }
        public string ConfirmPassWord { get; set; }
    }
}