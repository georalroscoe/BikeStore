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
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder, string schema)
        {


            builder.HasKey(e => new { e.OrderId, e.ItemId });

            builder.ToTable("order_items", "sales");

            builder.Property(e => e.OrderId).HasColumnName("order_id");
            builder.Property(e => e.ItemId).HasColumnName("item_id");
            builder.Property(e => e.Discount)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("discount");
            builder.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            builder.Property(e => e.ProductId).HasColumnName("product_id");
            builder.Property(e => e.Quantity).HasColumnName("quantity");

            builder.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_ite__order__72C60C4A");

            builder.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_ite__produ__73BA3083");

        }
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
