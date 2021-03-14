namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailOrder")]
    public partial class DetailOrder
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string ProductID { get; set; }

        public int? amount { get; set; }
    }
}
