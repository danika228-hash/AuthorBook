using AuthorBook.Data;
using AuthorBook.IRepository;
using AuthorBook.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorBook.Repository;

public class AuthorsRepository(AuthorBookDataContext dataContext) : IAuthorRepository
{
    public async Task<ICollection<Author>> GetAllAuthorsAsync()
    {
        var authorsAll = await dataContext.Authors
                            .Include(a => a.Books)
                            .ToListAsync();

        return authorsAll ?? new List<Author>();
    }

    public async Task<Author> GetByIdAuthorAsync(int id)
    {
        var author = await dataContext.Authors
            .AsNoTracking()
            .Include(b => b.Books)
            .FirstOrDefaultAsync(num => num.AuthorId == id);

        if (author == null) 
        { 
            throw new Exception($"Author with ID {id} not found."); 
        }

        return author!;
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        var updateAuthor = await dataContext.Authors
            .FindAsync(author.AuthorId);
        
        if (updateAuthor == null) 
        { 
            throw new Exception($"Author with ID {author.AuthorId} not found."); 
        }

        updateAuthor!.Name = author.Name;

        await dataContext.SaveChangesAsync();
    }

    public async Task<Author> AddAuthorAsync(Author author)
    {
        var authorUpdate = await dataContext.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Name == author.Name);

        if (authorUpdate == null)
        {
            await dataContext.Authors.AddAsync(author);
            await dataContext.SaveChangesAsync();
        }
        else
        {
            foreach (var book in author.Books)
            {
                if (!authorUpdate.Books.Any(b => b.Title == book.Title))
                {
                    authorUpdate.Books.Add(book);
                }
            }

            await dataContext.SaveChangesAsync();
        }

        return authorUpdate!;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await dataContext.Authors.FindAsync(id);

        if (author == null) return false;

        dataContext.Authors.Remove(author);

        await dataContext.SaveChangesAsync();

        return true;
    }
}
