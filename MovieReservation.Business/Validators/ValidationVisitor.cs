namespace MovieReservation.Business.Validators;

public class ValidationVisitor: IVisitor
{
    public void Visit(dynamic request)
    {
        Validate(request);
    }
    
    private void Validate(object request)
    {
        throw new ArgumentException("Parent Id is required");
    }
}