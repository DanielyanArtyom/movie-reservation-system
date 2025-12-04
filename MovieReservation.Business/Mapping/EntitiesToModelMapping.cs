namespace MovieReservation.Business.Mapping;

public sealed class EntitiesToModelMapping : Profile
{
    public EntitiesToModelMapping()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}