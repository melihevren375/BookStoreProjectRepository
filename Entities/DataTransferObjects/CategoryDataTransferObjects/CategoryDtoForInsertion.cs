namespace Entities.DataTransferObjects.CategoryDataTransferObjects
{
    public record CategoryDtoForInsertion:CategoryDtoForManipulation
    {
        public CategoryDtoForInsertion()
        {
            Id= Guid.NewGuid();
        }
    }
}
