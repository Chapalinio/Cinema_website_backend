using AutoMapper;
using BLL.DTOs;
using Domain.Entities;

namespace BLL.MapperProfile;

public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        //USER
        CreateMap<UserDTO, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Id)));

        CreateMap<User, UserDTO>().ForMember(dest => dest.Password, opt => opt.Ignore());

        //FILM
        CreateMap<FilmDTO, Film>().ReverseMap();

        //COMMENT
        CreateMap<CommentDTO, Comment>().ReverseMap();

        //FILM SESSION
        CreateMap<FilmSession, FilmSessionDTO>()
            .ForMember(
                dest => dest.FilmName,
                opt => opt.MapFrom(src => src.Film != null ? src.Film.Title : string.Empty)
            );

        CreateMap<FilmSessionDTO, FilmSession>()
            .ForMember(dest => dest.Film, opt => opt.Ignore());
    }
}
