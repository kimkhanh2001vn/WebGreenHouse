namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Footer
    {
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
