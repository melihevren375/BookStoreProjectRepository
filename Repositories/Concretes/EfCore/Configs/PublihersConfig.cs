using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs
{
    public class PublihersConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.
                HasKey(p => p.Id);

            builder.
                Property(p => p.Name).
                HasMaxLength(50).
                IsRequired();

            builder.
                HasMany(p => p.Books).
                WithOne(b => b.Publisher).
                HasForeignKey(o => o.PublisherId).
                OnDelete(DeleteBehavior.Restrict);
        }
    }
}
