namespace MovieReservation.Data.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        // Seed admin user
        builder.HasData(
            new User
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "Admin",
                Email = "admin@cinema.com",
                Password = "$2a$12$2uQbSl1q3d4Hk7Q6Nq9N2eFyG6PQsb0rHfC3xvLz1hkEeB0Y1ZyJu", // hashed 1111
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}