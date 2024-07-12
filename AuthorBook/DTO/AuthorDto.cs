using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class AuthorDto
{
    [Required]
    public int AuthorId { get; set; }

    [Required, MinLength(3), MaxLength(32)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public List<BookDto> Books { get; set; } = null!;
}
