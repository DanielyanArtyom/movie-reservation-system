namespace MovieReservation.Data.Context.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(x => x.RoleId);

        builder.Property(x => x.CreatedAt).IsRequired();

        // Seed admin user-role mapping
        builder.HasData(
            new UserRole
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee1"),
                UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // admin user
                RoleId = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1"), // Admin role
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}