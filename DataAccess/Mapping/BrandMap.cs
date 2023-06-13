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


            builder.HasKey(e => e.BrandId);

                builder.ToTable("brands", "production");

                builder.Property(e => e.BrandId).HasColumnName("brand_id").ValueGeneratedOnAdd(); 
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
