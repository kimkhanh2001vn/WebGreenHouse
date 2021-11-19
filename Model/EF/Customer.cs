namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Messengers = new HashSet<Messenger>();
        }

        public int CustomerID { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string PassWord { get; set; }

        [StringLength(256)]
        public string CustomerName { get; set; }

        [StringLength(256)]
        public string CustomerAddress { get; set; }

        [StringLength(256)]
        public string CustomerWork { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public bool? Status { get; set; }

        [StringLength(256)]
        public string CustomerEmail { get; set; }

        public int? NumberPhone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(256)]
        public string Avata { get; set; }

        [StringLength(256)]
        public string LinkZalo { get; set; }

        [StringLength(256)]
        public string LinkFacebook { get; set; }

        [StringLength(256)]
        public string ResestPasswordCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messenger> Messengers { get; set; }
    }
}
