namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        [Key]
        [StringLength(50)]
        public string productID { get; set; }

        public int number { get; set; }

        [Column(TypeName = "text")]
        public string image { get; set; }
    }
}
