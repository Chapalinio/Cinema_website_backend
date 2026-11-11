using AutoMapper;
using BLL.DTOs;
using Domain.Entities;

namespace BLL.MapperProfile;

public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<FilmDTO, Film>().ReverseMap();
        CreateMap<CommentDTO, Comment>().ReverseMap();
    }
}
