using AuthorBook.Models;

namespace AuthorBook.IRepository;

public interface IAuthorRepository
{
    public Task<ICollection<Author>> GetAllAuthorsAsync();
    public Task<Author> GetByIdAuthorAsync(int id);
    public Task UpdateAuthorAsync(Author author);
    public Task<Author> AddAuthorAsync(Author author);
    public Task<bool> DeleteAuthorAsync(int id);
}
