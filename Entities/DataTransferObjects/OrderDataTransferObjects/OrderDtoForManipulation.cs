using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.OrderDataTransferObjects
{
    public abstract record OrderDtoForManipulation
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "OrderDate is a required field.")]
        public virtual DateTime OrderDate { get; init; }

        [Required(ErrorMessage = "CustomerId is a required field.")]
        public Guid CustomerId { get; init; }

        [Required(ErrorMessage = "TotalAmount is a required field.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than zero.")]
        public decimal TotalAmount { get; set; }
    }
}
