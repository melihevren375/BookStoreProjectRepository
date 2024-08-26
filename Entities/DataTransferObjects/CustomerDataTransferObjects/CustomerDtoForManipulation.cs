namespace Entities.DataTransferObjects.CustomerDataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public abstract record CustomerDtoForManipulation
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Firstname is a required field and cannot be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Firstname must be between 2 and 50 characters long.")]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "Lastname is a required field and cannot be empty.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname must be between 2 and 50 characters long.")]
        public string LastName { get; init; }

        [Required(ErrorMessage = "Email is a required field and cannot be empty.")]
        [EmailAddress(ErrorMessage = "Email is not in a valid format.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is a required field and cannot be empty.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long and cannot be longer than 100 characters.")]
        public string Password { get; init; }

        [Required(ErrorMessage = "Address is a required field and cannot be empty.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; init; }

    }

}
