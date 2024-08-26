using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.PublisherDataTransferObjects
{
    public abstract record PublisherDtoForManipulation
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Publisher is a required field and cannot be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters long.")]
        public string Name { get; init; }
    }
}
