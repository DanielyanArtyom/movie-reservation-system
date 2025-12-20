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
    
    private void Validate(RoleModel request)
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
    
    private void Validate(GenreModel request)
    {
        if (request == null)
        {
            throw new ArgumentException("Genre could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Name could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new ArgumentException("Description could not be empty.");
        }
    }
    
    private void Validate(MovieModel request)
    {
        if (request == null)
        {
            throw new ArgumentException("Movie could not be empty");
        }
        
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title could not be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new ArgumentException("Description could not be empty.");
        }
    }
}