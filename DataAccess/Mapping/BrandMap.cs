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
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder, string schema)
        {

           
                builder.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E27C3072685");

                builder.ToTable("brands", "production");

                builder.Property(e => e.BrandId).HasColumnName("brand_id");
                builder.Property(e => e.BrandName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("brand_name");
           
        }
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
