namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderID { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerEmail { get; set; }

        [StringLength(256)]
        public string CustomerMobile { get; set; }

        [StringLength(256)]
        public string PaymentMethod { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        [StringLength(256)]
        public string PaymentStatus { get; set; }

        public int Status { get; set; }

        public int? CustomerId { get; set; }
    }
}
