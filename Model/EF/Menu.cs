namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string URL { get; set; }

        public int? DispalyOrder { get; set; }

        public int GruopID { get; set; }

        public string Target { get; set; }

        public bool Status { get; set; }

        public virtual MenuGruop MenuGruop { get; set; }
    }
}
