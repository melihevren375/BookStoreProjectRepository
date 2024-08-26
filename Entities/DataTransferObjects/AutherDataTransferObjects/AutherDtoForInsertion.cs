namespace Entities.DataTransferObjects.AutherDataTransferObjects
{
    public record AutherDtoForInsertion:AutherDtoForManipulation
    {
        public AutherDtoForInsertion()
        {
            Id = Guid.NewGuid();
        }
    }
}
