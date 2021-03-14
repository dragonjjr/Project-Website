namespace DatabaseProvider
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDb : DbContext
    {
        public MyDb()
            : base("name=MyDb")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountAd> AccountAd { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<DetailOrder> DetailOrder { get; set; }
        public virtual DbSet<ListLikes> ListLikes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.image)
                .IsUnicode(false);
        }
    }
}
