using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderWriteEntityConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.ToTable("SaleOrders");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Address)
                .HasConversion(address => address.Value, address => SaleOrderAddress.Create(address))
                .IsRequired();

            builder.Property(u => u.Description)
                .HasConversion(description => description == null ? null : description.Value,
                    value => string.IsNullOrWhiteSpace(value) ? null : SaleOrderDescription.Create(value))
                .IsRequired(false);

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
