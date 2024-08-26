using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.
                HasKey(od => od.Id);

            builder.
                Property(od => od.OrderId).
                IsRequired();

            builder.
               Property(od => od.BookId).
               IsRequired();

            builder.
               Property(od => od.Price).
               IsRequired();

            builder.
               Property(od => od.Quantity).
               IsRequired();

            builder.
                HasOne(od => od.Order).
                WithMany(o => o.OrderDetails).
                HasForeignKey(od => od.OrderId).
                OnDelete(DeleteBehavior.Restrict);

            builder.
                HasOne(od => od.Book).
                WithMany(b => b.OrderDetails).
                HasForeignKey(od => od.BookId).
                OnDelete(DeleteBehavior.Restrict);

        }
    }
}
