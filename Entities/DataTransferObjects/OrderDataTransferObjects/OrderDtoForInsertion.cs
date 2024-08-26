using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.OrderDataTransferObjects
{
    public record OrderDtoForInsertion : OrderDtoForManipulation
    {
        public OrderDtoForInsertion()
        {
            Id= Guid.NewGuid();
        }
        public string? BookIds { get; init; }
        public string? Quantities { get; init; }
        public override DateTime OrderDate => DateTime.UtcNow;
    }
}
