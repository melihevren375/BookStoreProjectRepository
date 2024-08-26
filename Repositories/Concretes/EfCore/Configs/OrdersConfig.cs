using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs
{
    public class OrdersConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.
                Property(o => o.OrderDate).
                IsRequired();

            builder.
                Property(o => o.CustomerId).
                IsRequired();

            builder.
                Property(o => o.TotalAmount).
                IsRequired();

            builder.
                HasOne(o => o.Customer).
                WithMany(c => c.Orders).
                HasForeignKey(o => o.CustomerId).
                OnDelete(DeleteBehavior.Restrict);

            builder.
                HasMany(o => o.OrderDetails).
                WithOne(od => od.Order).
                HasForeignKey(od => od.OrderId).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}
