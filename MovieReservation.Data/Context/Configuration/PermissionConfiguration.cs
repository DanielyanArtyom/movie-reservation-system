namespace MovieReservation.Data.Context.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccessType).IsRequired();
        builder.Property(x => x.Resource).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(r => r.Permissions)
            .HasForeignKey(x => x.RoleId);
    }
}