using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Models
{
    public class CartItem
    {
        public Post post { get; set; }
        public int Quantity { get; set; }
    }
}