namespace MovieReservation.Data.Context.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SeatRow).IsRequired().HasMaxLength(5);
        builder.Property(x => x.SeatNumber).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.TicketStatus).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        // Unique constraint: one seat per session
        builder.HasIndex(x => new { x.SessionId, x.SeatRow, x.SeatNumber }).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}