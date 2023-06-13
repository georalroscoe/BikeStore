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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder, string schema)
        {


            builder.HasKey(e => e.CategoryId);

            builder.ToTable("categories", "production");

            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");

        }
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Configure(builder, "dbo");
        }
    }
}
