using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.BookDataTransferObjects
{
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required(ErrorMessage = "AuthorId is a required field and cannot be empty.")]
        public int AuthorId { get; init; }

        [Required(ErrorMessage = "PublisherId is a required field and cannot be empty.")]
        public int PublisherId { get; init; }

        [Required(ErrorMessage = "CategoryId is a required field and cannot be empty.")]
        public int CategoryId { get; init; }
    }
}
