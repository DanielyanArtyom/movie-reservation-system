using AutoMapper;
using MovieReservation.API.DTO.Request;
using MovieReservation.Business.Model;

namespace MovieReservation.API.Mapping;

public class DTOtoModelMapping : Profile
{
    public DTOtoModelMapping()
    {
        CreateMap<RegisterRequest, UserModel>();
    }
}