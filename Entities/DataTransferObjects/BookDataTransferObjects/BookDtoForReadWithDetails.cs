using Entities.Concretes;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.BookDataTransferObjects
{
    public record BookDtoForReadWithDetails : BookDtoForManipulation
    {
        [Required(ErrorMessage = "Id boş olamaz.")]
        public Guid Id { get; init; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "CategoryName must be between 2 and 50 characters long.")]
        public string? CategoryName { get;init; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "PublisherName must be between 2 and 50 characters long.")]
        public string? PublisherName { get;init; }
       
        [StringLength(50, MinimumLength = 2, ErrorMessage = "AuthorFirstName must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string? AuthorFirstName { get;init; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "AuthorLastName must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string? AuthorLastName { get;init; }
    }
}
