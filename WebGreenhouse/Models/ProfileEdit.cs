using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Models
{
    public class ProfileEdit
    {
       public string CustomerName { get; set; }
       public string CustomerEmail { get; set; }
       public double NumberPhone { get; set; }
       public string CustomerAddress { get; set; }
       public string CustomerWork { get; set; }
       public string Description { get; set; }
       public string Facebook { get; set; }
       public string Zalo { get; set; }
    }
}