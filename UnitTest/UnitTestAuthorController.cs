using AuthorBook.Models;
using AuthorBook.DTO;
using AuthorBook.IRepository;
using AutoMapper;
using Moq;
using AuthorBook.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest;

public class UnitTestAuthorController
{
    [Fact]
    public async Task AddAuthorAsync_ReturnsAuthorResponse()
    {
        var mockAuthorRepository = new Mock<IAuthorRepository>();
        var mockMapper = new Mock<IMapper>();
        var authorsController = new AuthorsControllers(mockMapper.Object, mockAuthorRepository.Object);

        var addAuthorDto = new AddAuthorDto { Name = "New Author" };
        var author = new Author { AuthorId = 1, Name = "New Author" };
        var authorResponse = new AuthorDto { AuthorId = 1, Name = "New Author", Books = new List<BookDto>() };

        mockMapper.Setup(mapper => mapper.Map<Author>(addAuthorDto)).Returns(author);
        mockAuthorRepository.Setup(repo => repo.AddAuthorAsync(author)).ReturnsAsync(author);
        mockMapper.Setup(mapper => mapper.Map<AuthorDto>(author)).Returns(authorResponse);

        var result = await authorsController.AddAuthorAsync(addAuthorDto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AuthorDto>(okResult.Value);
        Assert.Equal("New Author", returnValue.Name);
    }

    [Fact]
    public async Task GetAllAuthorsAsync_ReturnsListOfAuthorResponses()
    {
        var mockAuthorRepository = new Mock<IAuthorRepository>();
        var mockMapper = new Mock<IMapper>();
        var authorsController = new AuthorsControllers(mockMapper.Object, mockAuthorRepository.Object);

        var authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Author 1", Books = new List<Book> { new Book { BookId = 1, Title = "Book 1" } } },
                new Author { AuthorId = 2, Name = "Author 2", Books = new List<Book> { new Book { BookId = 2, Title = "Book 2" } } }
            };

        var authorDtos = new List<AuthorDto>
            {
                new AuthorDto { AuthorId = 1, Name = "Author 1", Books = new List<BookDto> { new BookDto { BookId = 1, Title = "Book 1" } } },
                new AuthorDto { AuthorId = 2, Name = "Author 2", Books = new List<BookDto> { new BookDto { BookId = 2, Title = "Book 2" } } }
            };

        mockAuthorRepository.Setup(repo => repo.GetAllAuthorsAsync()).ReturnsAsync(authors);
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<AuthorDto>>(authors)).Returns(authorDtos);

        var result = await authorsController.GetAllAuthorsAsync();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<AuthorDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetByIdAuthorAsync_ReturnsAuthorResponse()
    {
        var mockAuthorRepository = new Mock<IAuthorRepository>();
        var mockMapper = new Mock<IMapper>();
        var authorsController = new AuthorsControllers(mockMapper.Object, mockAuthorRepository.Object);

        var authorId = 1;
        var author = new Author
        {
            AuthorId = authorId,
            Name = "Test Author",
            Books = new List<Book> { new Book { BookId = 1, Title = "Book 1" } }
        };

        var authorDto = new AuthorDto
        {
            AuthorId = authorId,
            Name = "Test Author",
            Books = new List<BookDto> { new BookDto { BookId = 1, Title = "Book 1" } }
        };

        mockAuthorRepository.Setup(repo => repo.GetByIdAuthorAsync(authorId)).ReturnsAsync(author);
        mockMapper.Setup(mapper => mapper.Map<AuthorDto>(author)).Returns(authorDto);

        var result = await authorsController.GetByIdAuthorAsync(authorId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AuthorDto>(okResult.Value);
        Assert.Equal("Test Author", returnValue.Name);
        Assert.Equal(1, returnValue.AuthorId);
    }

    [Fact]
    public async Task UpdateAuthorAsync_UpdatesAuthorSuccessfully()
    {
        var mockAuthorRepository = new Mock<IAuthorRepository>();
        var mockMapper = new Mock<IMapper>();
        var authorsController = new AuthorsControllers(mockMapper.Object, mockAuthorRepository.Object);

        var updateAuthorDto = new UpdateAuthorDto { AuthorId = 1, Name = "Updated Author Name" };
        var author = new Author { AuthorId = 1, Name = "Updated Author Name" };

        mockMapper.Setup(mapper => mapper.Map<Author>(updateAuthorDto)).Returns(author);
        mockAuthorRepository.Setup(repo => repo.UpdateAuthorAsync(author)).Returns(Task.CompletedTask);

        var result = await authorsController.UpdateAuthorAsync(updateAuthorDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value!);

    }

    [Fact]
    public async Task DeleteAuthorAsync_DeletesAuthorSuccessfully_ReturnsTrue()
    {
        var mockAuthorRepository = new Mock<IAuthorRepository>();
        var mockMapper = new Mock<IMapper>();
        var authorsController = new AuthorsControllers(mockMapper.Object, mockAuthorRepository.Object);

        var authorId = 1;
        mockAuthorRepository.Setup(repo => repo.DeleteAuthorAsync(authorId)).ReturnsAsync(true);

        var result = await authorsController.DeleteAuthorAsync(authorId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value!);
    }
}