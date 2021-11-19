using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Models
{
    public class ListAllOrderModel
    {
        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public Post post { get; set; }
    }
}