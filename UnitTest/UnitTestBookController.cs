using AuthorBook.Controllers;
using AuthorBook.DTO;
using AuthorBook.IRepository;
using AuthorBook.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest;

public class UnitTestBookController
{
    [Fact]
    public async Task AddBookAsync_ReturnsBookResponse()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var booksController = new BooksController(mockMapper.Object, mockBookRepository.Object);

        var addBookDto = new AddBookDto
        {
            Title = "Test Book",
            Name = "Test Author"
        };

        var book = new Book
        {
            BookId = 1,
            Title = addBookDto.Title,
            Author = new Author { AuthorId = 1, Name = "Test Author" }
        };

        var bookResponse = new BookDto
        {
            BookId = book.BookId,
            Title = book.Title
        };

        mockMapper.Setup(m => m.Map<Book>(addBookDto)).Returns(book);
        mockBookRepository.Setup(r => r.AddBookAsync(It.IsAny<Book>())).ReturnsAsync(book);
        mockMapper.Setup(m => m.Map<BookDto>(book)).Returns(bookResponse);

        var result = await booksController.AddBookAsync(addBookDto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<BookDto>(okResult.Value);

        Assert.Equal("Test Book", returnValue.Title);
        Assert.Equal(1, returnValue.BookId);

        mockMapper.Verify(m => m.Map<Book>(addBookDto), Times.Once);
        mockBookRepository.Verify(r => r.AddBookAsync(It.IsAny<Book>()), Times.Once);
        mockMapper.Verify(m => m.Map<BookDto>(book), Times.Once);
    }

    [Fact]
    public async Task GetAllBooksAsync_ReturnsListOfBookResponses()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var booksController = new BooksController(mockMapper.Object, mockBookRepository.Object);

        var books = new List<Book>
        {
            new Book { BookId = 1, Title = "Book 1", Author = new Author { AuthorId = 1, Name = "Author 1" } },
            new Book { BookId = 2, Title = "Book 2", Author = new Author { AuthorId = 2, Name = "Author 2" } }
        };

        var bookDtos = new List<BookDto>
        {
            new BookDto { BookId = 1, Title = "Book 1" },
            new BookDto { BookId = 2, Title = "Book 2" }
        };

        mockBookRepository.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(books);
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<BookDto>>(books)).Returns(bookDtos);

        var result = await booksController.GetAllBooksAsync();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<BookDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetByIdBookAsync_ReturnsBookResponse()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var booksController = new BooksController(mockMapper.Object, mockBookRepository.Object);

        var bookId = 1;
        var book = new Book
        {
            BookId = bookId,
            Title = "Test Book",
            Author = new Author { AuthorId = 1, Name = "Test Author" }
        };

        var bookDto = new BookDto
        {
            BookId = bookId,
            Title = "Test Book"
        };

        mockBookRepository.Setup(repo => repo.GetByIdBookAsync(bookId)).ReturnsAsync(book);
        mockMapper.Setup(mapper => mapper.Map<BookDto>(book)).Returns(bookDto);

        var result = await booksController.GetByIdBookAsync(bookId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<BookDto>(okResult.Value);
        Assert.Equal("Test Book", returnValue.Title);
        Assert.Equal(bookId, returnValue.BookId);
    }

    [Fact]
    public async Task UpdateBookAsync_UpdatesBookSuccessfully_ReturnsTrue()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var booksController = new BooksController(mockMapper.Object, mockBookRepository.Object);

        var updateBookDto = new UpdateBookDto
        {
            BookId = 1,
            Title = "Updated Book Title"
        };

        var book = new Book
        {
            BookId = 1,
            Title = "Updated Book Title"
        };

        mockMapper.Setup(mapper => mapper.Map<Book>(updateBookDto)).Returns(book);
        mockBookRepository.Setup(repo => repo.UpdateBookAsync(book)).ReturnsAsync(true);

        var result = await booksController.UpdateBookAsync(updateBookDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value!);
    }

    [Fact]
    public async Task DeleteBookAsync_DeletesBookSuccessfully_ReturnsTrue()
    {
        var mockBookRepository = new Mock<IBookRepository>();
        var mockMapper = new Mock<IMapper>();
        var booksController = new BooksController(mockMapper.Object, mockBookRepository.Object);

        var bookId = 1;
        mockBookRepository.Setup(repo => repo.DeleteBookAsync(bookId)).ReturnsAsync(true);

        var result = await booksController.DeleteBookAsync(bookId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True((bool)okResult.Value!);
    }
}
