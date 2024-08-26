using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.OrderDetailsDataTransferObjects
{
    public abstract record OrderDetailDtoForManipulation
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "OrderId is a required field.")]
        public Guid OrderId { get; init; }

        [Required(ErrorMessage = "BookId is a required field.")]
        public Guid BookId { get; init; }

        [Required(ErrorMessage = "Price is a required field.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; init; }
    }
}
