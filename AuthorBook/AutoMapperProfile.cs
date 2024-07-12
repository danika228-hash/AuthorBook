using AuthorBook.DTO;
using AutoMapper;
using AuthorBook.Models;

namespace AuthorBook.AutoMapperProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddAuthorDto, Author>()
           .ForMember(dest => dest.Books, opt => opt.MapFrom(src =>
               src.Books.Select(title => new Book { Title = title }).ToList()));

        CreateMap<Author, Author>()
           .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => new BookDto { Title = b.Title }).ToList()));

        CreateMap<Author, Author>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books
            .Select(b => new Book { BookId = b.BookId, Title = b.Title })
            .ToList()));

        CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

        CreateMap<AddBookDto, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => new Author { Name = src.Name }));

        CreateMap<Book, BookDto>();
        CreateMap<BookDto, Book>();
        CreateMap<UpdateAuthorDto, Author>();
        CreateMap<UpdateBookDto, Book>();
    }
}
