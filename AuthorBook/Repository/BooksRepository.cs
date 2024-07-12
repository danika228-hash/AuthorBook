using AuthorBook.Data;
using AuthorBook.IRepository;
using AuthorBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorBook.Repository;
public class BooksRepository(AuthorBookDataContext dataContext) : IBookRepository
{
    public async Task<ICollection<Book>> GetAllBooksAsync()
    {
        var bookAll = await dataContext.Books
            .AsNoTracking()
            .Include(a => a.Author)
            .OrderBy(a => a.BookId)
            .ToListAsync();
        
        return bookAll ?? new List<Book>();
    }

    public async Task<Book> GetByIdBookAsync(int id)
    {
        var resultBook = await dataContext.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.BookId == id);

        if (resultBook == null) 
        { 
            throw new Exception ($"Author with ID {id} not found."); 
        }

        return resultBook!;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var existingBook = await dataContext.Books
            .FindAsync(book.BookId);

        if (existingBook == null)
        {
            return false;
        }

        existingBook.Title = book.Title;

        await dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<Book> AddBookAsync(Book book)
    {
        var author = await dataContext.Authors
            .FirstOrDefaultAsync(a => a.Name == book.Author.Name);

        if (author == null)
        {
            author = new Author
            {
                Name = book.Author.Name,
            };

            await dataContext.Authors.AddAsync(author);
        }

        var existingBook = await dataContext.Books
            .FirstOrDefaultAsync(b => b.AuthorId == author.AuthorId && b.Title == book.Title);

        if (existingBook != null)
        {
            return existingBook;
        }

        var newBook = new Book
        {
            Author = author,
            Title = book.Title,
        };

        await dataContext.Books.AddAsync(newBook);

        await dataContext.SaveChangesAsync();

        return newBook;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var existingBook = await dataContext.Books.FindAsync(id);

        if (existingBook == null)
        {
            return false;
        }

        dataContext.Books.Remove(existingBook);

        await dataContext.SaveChangesAsync();

        return true;
    }
}
