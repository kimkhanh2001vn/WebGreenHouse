using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Models
{
    public class MessengerModel
    {
        public int CustomerID { get; set; }
        public string Content { get; set; }
    }
}