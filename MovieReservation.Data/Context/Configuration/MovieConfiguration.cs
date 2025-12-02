namespace MovieReservation.Data.Context.Configuration;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(x => x.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Sessions)
            .WithOne(s => s.Movie)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}