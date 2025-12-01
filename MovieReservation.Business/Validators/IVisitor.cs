namespace MovieReservation.Business.Validators;

public interface IVisitor
{
    void Visit(dynamic request);
}