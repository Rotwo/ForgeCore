using ForgeCore.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class AuthProviderConfiguration : IEntityTypeConfiguration<AuthProvider>
    {
        public void Configure(EntityTypeBuilder<AuthProvider> builder)
        {
            builder.ToTable("auth_providers");

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(x => x.ProviderUserId)
                .HasColumnName("provider_user_id")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .IsRequired(false);

            builder.Property(x => x.LinkedAt)
                .HasColumnName("linked_at")
                .IsRequired();
        }
    }
}
