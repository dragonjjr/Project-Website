namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        [StringLength(50)]
        public string productName { get; set; }

        public double? price { get; set; }

        public int? likes { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        public DateTime? postingDate { get; set; }

        [StringLength(50)]
        public string kindProduct { get; set; }

        [StringLength(250)]
        public string image { get; set; }
    }
}
