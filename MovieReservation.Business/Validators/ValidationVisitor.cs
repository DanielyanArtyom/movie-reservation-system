namespace MovieReservation.Business.Validators;

public class ValidationVisitor: IVisitor
{
    public void Visit(dynamic request)
    {
        Validate(request);
    }
    
    private void Validate(UserModel request)
    {
        if (request == null)
        {
            throw new ArgumentException("User could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("Login could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Password could not be empty.");
        }
    }
    
    private void Validate(Role request)
    {
        if (request == null)
        {
            throw new ArgumentException("User could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name could not be empty.");
        }
        
        if (request.Permissions == null)
        {
            throw new ArgumentException("Permissions could not be empty.");
        }
    }
}