using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.BookDataTransferObjects
{
    public record BookDtoForRead : BookDtoForManipulation
    {
        [Required(ErrorMessage = "AuthorId is a required field and cannot be empty.")]
        public Guid AuthorId { get; init; }

        [Required(ErrorMessage = "PublisherId is a required field and cannot be empty.")]
        public Guid PublisherId { get; init; }

        [Required(ErrorMessage = "CategoryId is a required field and cannot be empty.")]
        public Guid CategoryId { get; init; }
    }
}
