namespace Entities.DataTransferObjects.CustomerDataTransferObjects
{
    public record CustomerDtoForInsertion:CustomerDtoForManipulation
    {
        public CustomerDtoForInsertion()
        {
            Id= Guid.NewGuid();
        }
    }
}
