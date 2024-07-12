using Microsoft.EntityFrameworkCore;

namespace AuthorBook.Models;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}
