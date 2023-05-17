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
    public class StoreMap : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder, string schema)
        {

            builder.HasKey(e => e.StoreId).HasName("PK__stores__A2F2A30CE04AEE10");

            builder.ToTable("stores", "sales");

            builder.Property(e => e.StoreId).HasColumnName("store_id");
            builder.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            builder.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            builder.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("state");
            builder.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("store_name");
            builder.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            builder.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
            .HasColumnName("zip_code");

        }
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
