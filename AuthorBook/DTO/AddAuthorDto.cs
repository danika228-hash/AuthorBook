using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class AddAuthorDto
{
    [Required, MinLength(3), MaxLength(32)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ICollection<string> Books { get; set; } = null!;
}
