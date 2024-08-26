using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs;

public class EmailCodesConfig : IEntityTypeConfiguration<EmailCode>
{
    public void Configure(EntityTypeBuilder<EmailCode> builder)
    {
        builder.
            HasKey(ec => ec.Id);

        builder.
            Property(ec => ec.CustomerId).
            IsRequired();

        builder.
            Property(ec => ec.Code).
            IsRequired(false);

        builder.
            HasIndex(ec => ec.Code)
            .IsUnique();


        builder.
            HasOne(ec => ec.Customer).
            WithOne(c => c.EmailCode).
            HasForeignKey<EmailCode>(ec => ec.CustomerId).
            OnDelete(DeleteBehavior.Restrict);
    }
}
