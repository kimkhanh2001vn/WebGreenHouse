namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostCategorys")]
    public partial class PostCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string NameCate { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        public int StoreID { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int ParentID { get; set; }

        public int DisplayOrder { get; set; }

        public int FontSize { get; set; }

        [StringLength(256)]
        public string FontAwesomeClass { get; set; }

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
        public virtual ICollection<Post> Posts { get; set; }
    }
}
