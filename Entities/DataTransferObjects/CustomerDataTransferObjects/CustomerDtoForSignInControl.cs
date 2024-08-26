using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.CustomerDataTransferObjects
{
    public record CustomerDtoForSignInControl
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
