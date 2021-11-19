namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Structure")]
    public partial class Structure
    {
        public int ID { get; set; }

        [StringLength(256)]
        public string NameSell { get; set; }
    }
}
