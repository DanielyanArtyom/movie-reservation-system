namespace MovieReservation.Data.Context.Configuration;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.AvailableSeats).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasData(
            new Room
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                Name = "Room A",
                AvailableSeats = 50,
                HasFreeSeats = true,
                CreatedAt = DateTime.UtcNow
            },
            new Room
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                Name = "Room B",
                AvailableSeats = 80,
                HasFreeSeats = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}