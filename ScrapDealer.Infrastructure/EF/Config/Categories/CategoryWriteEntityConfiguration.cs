using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Infrastructure.EF.Config.Categories
{
    internal class CategoryWriteEntityConfiguration : IEntityTypeConfiguration<Category>, IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasConversion(category => category.Value, category => CategoryName.Create(category))
                .IsRequired();

            builder.HasMany(u => u.SubCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }

        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasConversion(category => category.Value, category => CategoryName.Create(category))
                .IsRequired();

            builder.Property(u => u.Price)
                .HasConversion(price => price.Value, price => SubCategoryPrice.Create(price))
                .IsRequired();

            builder.HasOne(u => u.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
