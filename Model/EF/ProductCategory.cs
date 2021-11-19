namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategorys")]
    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public int? HomeOrder { get; set; }

        [StringLength(256)]
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(256)]
        public string UpdatedBy { get; set; }

        [StringLength(256)]
        public string MetaKeyword { get; set; }

        [StringLength(256)]
        public string MetaDescription { get; set; }

        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
