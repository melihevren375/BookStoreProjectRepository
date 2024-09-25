using Entities.DataTransferObjects.OrderDetailsDataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.OrderDetailDataTransferObjects;

public record OrderDetailDtoForDetails:OrderDetailDtoForManipulation
{
    [MaxLength(100, ErrorMessage = "BookName max 100 karakter olmalı.")]
    [MinLength(2, ErrorMessage = "BookName min 2 karakter olmalı.")]
    public string? BookName { get; init; }
}
