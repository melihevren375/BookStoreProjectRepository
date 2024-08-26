namespace Entities.DataTransferObjects.EmailCodeDataTransferObjects;

public record EmailCodeFtoForInsertion : EmailCodeDtoForManipulation
{
    public EmailCodeFtoForInsertion()
    {
        Id = Guid.NewGuid();
    }
}
