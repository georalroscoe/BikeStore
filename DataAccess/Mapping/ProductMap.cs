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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder, string schema)
        {

            builder.HasKey(e => e.ProductId);

            builder.ToTable("products", "production");

            builder.Property(e => e.ProductId).HasColumnName("product_id").ValueGeneratedOnAdd();
            builder.Property(e => e.BrandId).HasColumnName("brand_id");
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            builder.Property(e => e.ModelYear).HasColumnName("model_year");
            builder.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");

            builder.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brand___619B8048");

            builder.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__60A75C0F");

        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
