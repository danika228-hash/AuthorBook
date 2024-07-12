using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class AddBookDto
{
    [Required, MinLength(3), MaxLength(32)]
    public string Title { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(32)]
    public string Name { get; set; } = string.Empty;
}
