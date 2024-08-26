using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;

namespace Entities.DataTransferObjects.OrderDetailDataTransferObjects;

public record OrderDetailDtoForDetails:OrderDetailDtoForManipulation
{
    public string BookName { get; init; }
}
