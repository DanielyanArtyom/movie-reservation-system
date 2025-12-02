
namespace MovieReservation.Data.Context.Configuration;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasData(
            new MovieGenre
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                Name = "Action",
                Description = "Fast-paced, dynamic movies.",
                CreatedAt = DateTime.UtcNow
            },
            new MovieGenre
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                Name = "Comedy",
                Description = "Humorous, light-hearted movies.",
                CreatedAt = DateTime.UtcNow
            },
            new MovieGenre
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                Name = "Drama",
                Description = "Emotional, narrative-driven stories.",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}