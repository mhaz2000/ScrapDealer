using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderReadEntityConfiguration : IEntityTypeConfiguration<SaleOrderReadModel>
    {
        public void Configure(EntityTypeBuilder<SaleOrderReadModel> builder)
        {
            builder.ToTable("SaleOrders");
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.Seller)
                .WithMany()
                .HasForeignKey(u => u.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.SubCategory)
                .WithMany()
                .HasForeignKey(u => u.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Images)
                .HasConversion(
                    v => string.Join(",", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? new List<Guid>()
                        : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
                )
                .HasColumnType("nvarchar(max)");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
