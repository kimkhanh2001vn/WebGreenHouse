namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Messengers = new HashSet<Messenger>();
            Tags1 = new HashSet<Tag>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Alias { get; set; }

        public int CategoryID { get; set; }

        [StringLength(256)]
        public string Images { get; set; }

        [StringLength(256)]
        public string Images2 { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string Content { get; set; }

        public bool HomeFlag { get; set; }

        public bool HotFlag { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(256)]
        public string Tags { get; set; }

        public DateTime? ActiveDate { get; set; }

        public bool IsSetTime { get; set; }

        [StringLength(256)]
        public string ImageFB { get; set; }

        public int CreatedByID { get; set; }

        public int? StructureID { get; set; }

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

        public string Address { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messenger> Messengers { get; set; }

        public virtual PostCategory PostCategory { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags1 { get; set; }
    }
}
