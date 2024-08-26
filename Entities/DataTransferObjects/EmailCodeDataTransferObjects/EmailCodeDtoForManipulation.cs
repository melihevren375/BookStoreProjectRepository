using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.EmailCodeDataTransferObjects;

public abstract record EmailCodeDtoForManipulation
{
    public Guid Id { get; init; }

    [Required]
    public Guid CustomerId { get; init; }

    [Required]
    public int Code { get; init; }
}
