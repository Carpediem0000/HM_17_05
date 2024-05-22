using HM_17_05.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HM_17_05
{
    public partial class Shop1 : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        public Shop1()
            : base("name=Shop1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
               .HasMany(o => o.Products)
               .WithMany()
               .Map(m =>
               {
                   m.ToTable("OrderProducts");
                   m.MapLeftKey("OrderId");
                   m.MapRightKey("ProductId");
               });

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId);
        }
    }
}
