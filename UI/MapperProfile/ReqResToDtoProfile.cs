using AutoMapper;
using BLL.DTOs;
using Domain.Entities;
using UI.Contracts.Comment;
using UI.Contracts.Common;
using UI.Contracts.Film;
using UI.Contracts.FilmSession;
using UI.Contracts.User;

namespace UI.MapperProfile;

public class ReqResToDtoProfile : Profile
{
    public ReqResToDtoProfile()
    {
        //USER
        CreateMap<UserRegisterRequest, UserDTO>().ReverseMap();
        CreateMap<UserResponce, UserDTO>().ReverseMap();
        CreateMap<UserProfileResponce, UserDTO>().ReverseMap();

        //FILM
        CreateMap<FilmCreateRequest, FilmDTO>().ReverseMap();
        CreateMap<FilmUpdateRequest, FilmDTO>().ReverseMap();
        CreateMap<FilmResponce, FilmDTO>().ReverseMap();

        //COMMENT
        CreateMap<CommentCreateRequest, CommentDTO>().ReverseMap();
        CreateMap<CommentResponse, CommentDTO>().ReverseMap();
        CreateMap<CommentResponse, Comment>().ReverseMap();

        //FILTERS
        CreateMap<SetFiltersRequest, SearchFiltersDTO>().ReverseMap();

        //FILM SESSION
        CreateMap<FilmSessionCreateRequest, FilmSessionDTO>().ReverseMap();
        CreateMap<FilmSessionUpdateRequest, FilmSessionDTO>().ReverseMap();
        CreateMap<FilmSessionResponce, FilmSessionDTO>().ReverseMap();
    }
}
