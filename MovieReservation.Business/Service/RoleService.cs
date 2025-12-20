namespace MovieReservation.Business.Service;

public class RoleService: IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IVisitor _visitor;
    
    public RoleService(IUnitOfWork unitOfWork, IVisitor visitor ,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }
    
    public async Task CreateAsync(RoleModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var role = (await _unitOfWork.Roles.SearchAsync(new SearchContext<Role>
        {
            Filter = x => x.Name == request.Name
        }, ct)).Results.FirstOrDefault();
        
        if (role != null)
        {
            throw new DuplicateFoundException("Role is already exists");
        }
        
        _unitOfWork.Roles.Add(_mapper.Map<Role>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(RoleModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var role = (await _unitOfWork.Roles.SearchAsync(new SearchContext<Role>
        {
            Filter = x => x.Id == request.Id || x.Name == request.Name
        }, ct)).Results.FirstOrDefault();
        
        if (role == null)
        {
            throw new NotFoundException("Role is not found");
        }
        
        _unitOfWork.Roles.Add(_mapper.Map<Role>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<RoleModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id, ct);

        return _mapper.Map<RoleModel>(role);
    }

    public async Task<List<RoleModel>> GetAllAsync()
    {
        var roles = await _unitOfWork.Roles.SearchAsync(new SearchContext<Role>());
        
        return _mapper.Map<List<RoleModel>>(roles.Results);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id, ct);
        
        if (role == null)
        {
            throw new NotFoundException("Role does not exists");
        }

        _unitOfWork.Roles.Delete(role);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<List<RoleModel>> GetRolesByUser(Guid id, CancellationToken ct = default)
    {
        var roles = await _unitOfWork.UserRoles.SearchAsync(new SearchContext<UserRole>
        {
            Filter = x => x.UserId == id,
            Include = new Expression<Func<UserRole, object>>[] { x => x.Role, x => x.Role.Permissions }
        }, ct);

        return _mapper.Map<List<RoleModel>>(roles.Results);

    }

    public async Task<List<PermissionModel>> GetPermissionsByRoleId(Guid id, CancellationToken ct = default)
    {
        var role = (await _unitOfWork.Roles.SearchAsync(new SearchContext<Role>
        {
            Filter = x => x.Id == id,
            Include = new Expression<Func<Role, object>>[] { x => x.Permissions }
        }, ct)).Results.FirstOrDefault();

        if (role == null)
        {
            throw new NotFoundException("Role is not found");
        }

        return _mapper.Map<List<PermissionModel>>(role.Permissions);
    }
}