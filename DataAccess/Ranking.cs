namespace VladyslavOrlovPromo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ranking")]
    public partial class Ranking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Ranking")]
        [StringLength(500)]
        public string Ranking1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateInserted { get; set; }
    }
}
