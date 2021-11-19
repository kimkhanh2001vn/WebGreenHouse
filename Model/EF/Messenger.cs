namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Messenger")]
    public partial class Messenger
    {
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(256)]
        public string Content { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? PostID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Post Post { get; set; }
    }
}
