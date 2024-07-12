using AuthorBook.DTO;
using AuthorBook.IRepository;
using AuthorBook.Models;
using Microsoft.AspNetCore.Mvc;
using AuthorBook.Filters;
using AutoMapper;

namespace AuthorBook.Controllers;

[Route("api/author")]
[ApiController]
[TypeFilter(typeof(CustomExceptionFilter))]
public class AuthorsControllers(IMapper mapper, IAuthorRepository authorRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthorsAsync()
    {
        var authorsList = await authorRepository.GetAllAuthorsAsync();

        var response = mapper.Map<IEnumerable<AuthorDto>>(authorsList);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetByIdAuthorAsync(int id)
    {
        var author = await authorRepository.GetByIdAuthorAsync(id);

        var response = mapper.Map<AuthorDto>(author);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthorAsync(UpdateAuthorDto updateAuthorDto)
    {
        var author = mapper.Map<Author>(updateAuthorDto);

        await authorRepository.UpdateAuthorAsync(author);

        return Ok(true);
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> AddAuthorAsync(AddAuthorDto authorDto)
    {
        var authorAdd = mapper.Map<Author>(authorDto);
        var author = await authorRepository.AddAuthorAsync(authorAdd);
        var response = mapper.Map<AuthorDto>(author);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorAsync(int id)
    {
        var response = await authorRepository.DeleteAuthorAsync(id);

        return Ok(response);
    }
}
