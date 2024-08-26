using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs
{
    public class BooksConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title).
                IsRequired().
                HasColumnType("varchar(100)");

            builder.Property(b => b.Price).
                IsRequired().
                HasColumnType("decimal(18,2)");

            builder.
                Property(b => b.AuthorId).
                IsRequired();

            builder.
                Property(b => b.PublisherId).
                IsRequired();

            builder.
                Property(b => b.CategoryId).
                IsRequired();

            builder.
                HasOne(b => b.Category).
                WithMany(c => c.Books).
                HasForeignKey(b => b.CategoryId).
                OnDelete(DeleteBehavior.Restrict);

            builder.
                HasOne(b => b.Author).
                WithMany(a => a.Books).
                HasForeignKey(b => b.AuthorId).
                OnDelete(DeleteBehavior.Restrict);

            builder.
                HasOne(b => b.Publisher).
                WithMany(p => p.Books).
                HasForeignKey(b => b.PublisherId).
                OnDelete(DeleteBehavior.Restrict);

            builder.
                HasMany(b => b.OrderDetails).
                WithOne(od => od.Book).
                HasForeignKey(od => od.BookId).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}
