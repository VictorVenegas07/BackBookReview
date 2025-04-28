using AutoMapper;
using BookReview.Application.UserCase.Authentication.Dtos;
using BookReview.Application.UserCase.Books.Commands.CreateBook;
using BookReview.Application.UserCase.Books.Dtos;
using BookReview.Application.UserCase.Category.Dtos;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;

namespace BookReview.Application.Configurations.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Entities.Book, CreateBookCommand>().ReverseMap();
            CreateMap<Domain.Entities.Book, CreateBookResponse>().ReverseMap();

            CreateMap<PaginatedResponse<Domain.Entities.Book>, PaginatedResponse<GetPaginedBookResponse>>()
                .ReverseMap();

            CreateMap<Domain.Entities.Book, GetPaginedBookResponse>()
                 .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookCategories.Select(x=> x.Category.Name)))
                .ReverseMap();


            CreateMap<Review, ReviewResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
                .ReverseMap();

            CreateMap<Category, CategoryResponse>().ReverseMap();

            CreateMap<User, UserResponse>()
                .ReverseMap();

        }
    }

}
