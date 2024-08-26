namespace Entities.DataTransferObjects.CustomerDataTransferObjects
{
    public record CustomerDtoForChangePassword
    {
        public Guid Id { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Code { get; set; }
    }
}
