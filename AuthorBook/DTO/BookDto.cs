using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class BookDto
{
    [Required, MinLength(3), MaxLength(32)]
    public string Title { get; set; }  = string.Empty;

    [Required]
    public int BookId { get; set; }
}
