namespace MovieReservation.Data.Interface;

public interface IUnitOfWork
{
    //IRepository<User> Users { get; }
    
    Task CompleteAsync(CancellationToken ct = default );
    void Dispose();
}