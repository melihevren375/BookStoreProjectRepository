namespace Entities.DataTransferObjects.OrderDetailsDataTransferObjects
{
    public record OrderDetailForInsertion:OrderDetailDtoForManipulation
    {
        public OrderDetailForInsertion()
        {
            Id = Guid.NewGuid();
        }
        public string? BookIds { get; init; }
        public string? Quantities { get; init; }
    }
}
