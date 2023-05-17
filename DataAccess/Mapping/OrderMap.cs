using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;


namespace DataAccess.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder, string schema)
        {


            builder.HasKey(e => e.OrderId).HasName("PK__orders__4659622972C4BF33");

            builder.ToTable("orders", "sales");

            builder.Property(e => e.OrderId).HasColumnName("order_id");
            builder.Property(e => e.CustomerId).HasColumnName("customer_id");
            builder.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            builder.Property(e => e.OrderStatus).HasColumnName("order_status");
            builder.Property(e => e.RequiredDate)
                .HasColumnType("date")
                .HasColumnName("required_date");
            builder.Property(e => e.ShippedDate)
                .HasColumnType("date")
                .HasColumnName("shipped_date");
            builder.Property(e => e.StaffId).HasColumnName("staff_id");
            builder.Property(e => e.StoreId).HasColumnName("store_id");

            builder.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__orders__customer__6D0D32F4");

            builder.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__staff_id__6EF57B66");

            builder.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__orders__store_id__6E01572D");

        }
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
