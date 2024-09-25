using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.AutherDataTransferObjects
{
    public abstract record AutherDtoForManipulation
    {
        public Guid Id { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string FirstName { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "LastName must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string LastName { get; init; }
    }
}
