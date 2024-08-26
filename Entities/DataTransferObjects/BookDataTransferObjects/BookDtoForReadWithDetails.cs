using Entities.Concretes;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.BookDataTransferObjects
{
    public record BookDtoForReadWithDetails : BookDtoForManipulation
    {
        [Required(ErrorMessage = "Id boş olamaz.")]
        public Guid Id { get; init; }

        public string? CategoryName { get;init; }
        public string? PublisherName { get;init; }
        public string? AuthorFirstName { get;init; }
        public string? AuthorLastName { get;init; }
    }
}
