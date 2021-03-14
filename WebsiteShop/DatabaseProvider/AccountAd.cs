namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountAd")]
    public partial class AccountAd
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        [Column("_password")]
        [StringLength(50)]
        public string C_password { get; set; }
    }
}
