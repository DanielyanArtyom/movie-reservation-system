using System.Linq.Expressions;
using System.Security.Claims;

namespace MovieReservation.Business.Service;

public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly IVisitor _visitor;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UserService(
        IUnitOfWork unitOfWork, 
        IJwtService jwtService, 
        IHttpContextAccessor accessor, 
        IVisitor visitor, 
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _httpContextAccessor = accessor;
        _visitor = visitor;
    }
    

    #region Users Interactions

    public async Task UpdateAsync(UserModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var userTask =  _unitOfWork.Users.GetByIdAsync(request.Id, ct);
        
        var duplicateUserTask = _unitOfWork.Users.SearchAsync(new SearchContext<User>
        {
            Filter = x => x.Email == request.Email && x.Id != request.Id
        }, ct);

        await Task.WhenAll(userTask, duplicateUserTask);

        var user = userTask.Result;

        if (user == null)
        {
            throw new NotFoundException("Use is not found");
        }

        if (duplicateUserTask.Result != null)
        {
            throw new DuplicateFoundException("User with this login is already exists");
        }

        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        _unitOfWork.Users.Update(request.Id, _mapper.Map<User>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id, ct);

        return _mapper.Map<UserModel>(user);
    }

    public async Task<List<UserModel>> GetAllAsync()
    {
        var users = await _unitOfWork.Users.SearchAsync(new SearchContext<User>());
        
        return _mapper.Map<List<UserModel>>(users.Results);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id, ct);
        
        if (user == null)
        {
            throw new NotFoundException("User does not exists");
        }

        _unitOfWork.Users.Delete(user);
        await _unitOfWork.CompleteAsync(ct);
    }
    

    #endregion

    #region Auth

    public async Task<AuthorizationModel> Login(UserModel request, CancellationToken ct = default)
    {
        var user = await _unitOfWork.Users.GetUserFullDataAsync(request.Email, ct);

        if (user == null)
        {
            throw new NotFoundException("User is not found");
        }
        
        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!isValid)
        {
            throw new UnauthorizedException("Invalid Password");
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthorizationModel
        {
            AccessToken = token
        };
    }
    
    public async Task CreateAsync(UserModel request, CancellationToken ct)
    {
        _visitor.Visit(request);
        
        var user = (await _unitOfWork.Users.SearchAsync(new SearchContext<User>
        {
            Filter = x => x.Email == request.Email,
        }, ct)).Results.FirstOrDefault();
        
        if (user != null)
        {
            throw new DuplicateFoundException("User is already exists");
        }
        
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        _unitOfWork.Users.Add(_mapper.Map<User>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task CheckAccesses(CheckAccessModel request, CancellationToken ct = default)
    {
        var userRoles = GetUserRolesFromJwt();

        var permissions = await _unitOfWork.Permissions.SearchAsync(new SearchContext<Permission>
        {
            Filter = p =>
                userRoles.Contains(p.Role.Name) &&
                p.Resource == request.TargetPage &&
                p.AccessType == request.Permission,
            Include = new Expression<Func<Permission, object>>[] { x => x.Resource, x => x.Role }
        }, ct);

        if (permissions.TotalCount == 0)
        {
            throw new ForbiddenException("User does not have accesses");
        }
    }

    #endregion
    
    #region privateMethods

    private List<string> GetUserRolesFromJwt()
    {
        var user = _httpContextAccessor.HttpContext.User;

        var roles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        return roles;
    }
    
    #endregion
}