using AutoMapper;
using BLL.DTOs;
using Domain.Entities;
using UI.Contracts.Comment;
using UI.Contracts.Common;
using UI.Contracts.Film;
using UI.Contracts.User;

namespace UI.MapperProfile;

public class ReqResToDtoProfile : Profile
{
    public ReqResToDtoProfile()
    {
        CreateMap<UserRegisterRequest, UserDTO>().ReverseMap();
        CreateMap<UserLoginRequest, UserDTO>().ReverseMap();
        CreateMap<UserUpdateRequest, UserDTO>().ReverseMap();
        CreateMap<UserResponce, UserDTO>().ReverseMap();

        CreateMap<FilmCreateRequest, FilmDTO>().ReverseMap();
        CreateMap<FilmUpdateRequest, FilmDTO>().ReverseMap();
        CreateMap<FilmResponce, FilmDTO>().ReverseMap();

        CreateMap<CommentCreateRequest, CommentDTO>().ReverseMap();
        CreateMap<CommentResponse, CommentDTO>().ReverseMap();
        CreateMap<CommentResponse, Comment>().ReverseMap();

        CreateMap<SetFiltersRequest, SearchFiltersDTO>().ReverseMap();
    }
}
