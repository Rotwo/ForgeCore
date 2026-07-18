using ForgeCore.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForgeCore.Infrastructure.Persistence.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("sessions");

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.AccountId).HasColumnName("account_id").IsRequired();
            builder.Property(x => x.RefreshToken).HasColumnName("refresh_token").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
            builder.Property(x => x.DeviceInfo).HasColumnName("device_info");
            builder.Property(x => x.IpAddress).HasColumnName("ip_address");

            builder.HasIndex(s => s.RefreshToken)
                .IsUnique()
                .HasDatabaseName("IX_Session_RefreshToken_Unique");

            builder.Property(x => x.RowVersion)
                .IsRowVersion()
                .HasColumnName("row_version");
        }
    }
}
