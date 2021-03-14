namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string productID { get; set; }

        [Column("_content")]
        [StringLength(50)]
        public string C_content { get; set; }

        public DateTime? datePost { get; set; }
    }
}
