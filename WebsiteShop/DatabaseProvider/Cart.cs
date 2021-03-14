namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        public int? amount { get; set; }
    }
}
