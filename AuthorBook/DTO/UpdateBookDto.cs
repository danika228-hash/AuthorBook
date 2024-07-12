using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class UpdateBookDto
{
    [Required]
    public int BookId { get; set; }

    [Required, MinLength(2), MaxLength(32)]
    public string Title { get; set; } = string.Empty;
}
