namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(50)]
        public string email { get; set; }

        [Column("_password")]
        [StringLength(50)]
        public string C_password { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(1000)]
        public string address { get; set; }

        [StringLength(50)]
        public string authenCode { get; set; }
    }
}
