using System;

namespace WebGreenhouse.Areas.Admin.Common
{
    [Serializable]
    public class CustomerLogin
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}