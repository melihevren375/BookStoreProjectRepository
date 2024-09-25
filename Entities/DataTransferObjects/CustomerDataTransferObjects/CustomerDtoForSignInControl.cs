using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.CustomerDataTransferObjects
{
    public record CustomerDtoForSignInControl
    {
        [Required(ErrorMessage ="Email is a required field. It cannot be empty.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is a required field. It cannot be empty.")]
        public string Password { get; init; }
    }
}
