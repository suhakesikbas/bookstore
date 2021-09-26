using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Entites;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre,opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author,opt => opt.MapFrom(src => src.Author.Name));
            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre,opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author,opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();

            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>()
                .ForMember(dest => dest.Birthday,opt => opt.MapFrom(src => src.Birthday.ToString("dd/MM/yyyy")));
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(dest => dest.Birthday,opt => opt.MapFrom(src => src.Birthday.ToString("dd/MM/yyyy")));

            CreateMap<CreateUserModel, User>();
        }
    }

}