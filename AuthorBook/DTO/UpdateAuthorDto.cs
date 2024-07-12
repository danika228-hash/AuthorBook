using System.ComponentModel.DataAnnotations;

namespace AuthorBook.DTO;

public class UpdateAuthorDto
{
    [Required]
    public int AuthorId { get; set; }

    [Required,MinLength(2),MaxLength(32)]
    public string Name { get; set; } = string.Empty;
}
