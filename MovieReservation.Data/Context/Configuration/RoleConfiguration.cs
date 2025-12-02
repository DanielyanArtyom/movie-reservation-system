namespace MovieReservation.Data.Context.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CreatedAt).IsRequired();

        // Seed roles
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1"),
                Name = "Admin",
                CreatedAt = DateTime.UtcNow
            },
            new Role
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc2"),
                Name = "User",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}