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

        CreateMap<FilmSession, FilmSessionDTO>()
            .ForMember(
                dest => dest.FilmName,
                opt => opt.MapFrom(src => src.Film != null ? src.Film.Title : string.Empty)
            );

        CreateMap<FilmSessionDTO, FilmSession>()
            .ForMember(dest => dest.Film, opt => opt.Ignore());
    }
}
