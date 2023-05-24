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
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder, string schema)
        {


            builder.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK__stocks__E68284D382FF5216");

            builder.ToTable("stocks", "production");

            builder.Property(e => e.StoreId).HasColumnName("store_id");
            builder.Property(e => e.ProductId).HasColumnName("product_id");
            builder.Property(e => e.Quantity).HasColumnName("quantity");
            builder.Property(s => s.TimeStamp)
                                            .HasColumnName("Timestamp")
                                            .HasColumnType("rowversion")
                                            .IsConcurrencyToken();

            builder.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__stocks__product___778AC167");

            builder.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__stocks__store_id__76969D2E");

        }
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
