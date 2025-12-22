namespace MovieReservation.Business.Mapping;

public sealed class EntitiesToModelMapping : Profile
{
    public EntitiesToModelMapping()
    {
        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<Role, RoleModel>().ReverseMap();
        CreateMap<Permission, PermissionModel>().ReverseMap();
        CreateMap<GenreModel, MovieGenre>().ReverseMap();
        CreateMap<MovieModel, Movie>().ReverseMap();
        CreateMap<RoomModel, Room>().ReverseMap();
    }
}