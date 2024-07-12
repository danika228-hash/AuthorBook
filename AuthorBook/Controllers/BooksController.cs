using AuthorBook.DTO;
using AuthorBook.Filters;
using AuthorBook.IRepository;
using AuthorBook.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthorBook.Controllers;

[Route("api/book")]
[ApiController]
[TypeFilter(typeof(CustomExceptionFilter))]
public class BooksController(IMapper mapper, IBookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooksAsync()
    {
        var result = await bookRepository.GetAllBooksAsync();

        var bookResponses = mapper.Map<IEnumerable<BookDto>>(result);

        return Ok(bookResponses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetByIdBookAsync(int id)
    {
        var book = await bookRepository.GetByIdBookAsync(id);

        var bookResponse = mapper.Map<BookDto>(book);

        return Ok(bookResponse);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBookAsync(UpdateBookDto updateBookDto)
    {
        var book = mapper.Map<Book>(updateBookDto);

        var response = await bookRepository.UpdateBookAsync(book);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> AddBookAsync(AddBookDto addBookDto)
    {
        var book = mapper.Map<Book>(addBookDto);

        var bookAdd = await bookRepository.AddBookAsync(book);

        var response = mapper.Map<BookDto>(bookAdd);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var response = await bookRepository.DeleteBookAsync(id);

        return Ok(response);
    }
}
