using AuthorBook.Models;

namespace AuthorBook.IRepository;

public interface IBookRepository
{
    Task<ICollection<Book>> GetAllBooksAsync();
    Task<Book> GetByIdBookAsync(int id);
    Task<bool> UpdateBookAsync(Book updateBookDto);
    Task<Book> AddBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);
}
