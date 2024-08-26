﻿using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Concretes.EfCore.Configs
{
    public class CustomersConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Address)
                .HasMaxLength(200);

            builder.
                HasMany(c => c.Orders).
                WithOne(o => o.Customer).
                HasForeignKey(o => o.CustomerId).
                OnDelete(DeleteBehavior.Restrict);

        }
    }
}
