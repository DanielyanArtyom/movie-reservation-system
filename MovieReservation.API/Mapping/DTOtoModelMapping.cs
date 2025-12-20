namespace MovieReservation.API.Mapping;

public class DTOtoModelMapping : Profile
{
    public DTOtoModelMapping()
    {
            CreateMap<LoginRequest, UserModel>();
            CreateMap<AuthorizationModel, AuthorizationDto>();
            CreateMap<RegisterRequest, UserModel>();
            CreateMap<CheckAccessRequest, CheckAccessModel>();
            CreateMap<UserModel, UserDto>();

            CreateMap<PermissionModel, PermissionDto>().ReverseMap();
            CreateMap<RoleCreateRequest, RoleModel>();
            
            CreateMap<UserUpdateRequest, UserModel>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.Roles.Select(genreId => new RoleModel { Id = genreId })));

            CreateMap<RoleModel, RoleDto>();
            CreateMap<GenreRequest, GenreModel>();
            CreateMap<GenreModel, GenreResponse>();

            CreateMap<MovieCreateRequest, MovieModel>();
    }
}