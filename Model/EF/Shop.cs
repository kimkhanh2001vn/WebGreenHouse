namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shop
    {
        [Key]
        public int StoreID { get; set; }

        [Required]
        [StringLength(256)]
        public string ShopName { get; set; }
    }
}
