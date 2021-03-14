namespace DatabaseProvider
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string numPhone { get; set; }

        [StringLength(500)]
        public string DeliAddress { get; set; }

        public DateTime? orderDate { get; set; }

        public DateTime? deliveryDate { get; set; }

        [StringLength(50)]
        public string status { get; set; }


    }
}
