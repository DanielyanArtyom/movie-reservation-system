namespace MovieReservation.Data.Context.Entity;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}